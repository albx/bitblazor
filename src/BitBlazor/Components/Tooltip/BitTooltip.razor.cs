using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BitBlazor.Components;

/// <summary>
/// Wraps its child content and attaches a Bootstrap Italia tooltip
/// directly to the first child element.
/// </summary>
public partial class BitTooltip : BitComponentBase, IAsyncDisposable
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    private IJSObjectReference? _jsModule;
    private ElementReference _spanRef;

    /// <summary>
    /// The text displayed inside the tooltip (maps to the <c>title</c> attribute).
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tooltip placement. Default: <see cref="TooltipPlacement.Top"/>.
    /// </summary>
    [Parameter]
    public TooltipPlacement Placement { get; set; } = TooltipPlacement.Top;

    /// <summary>
    /// Gets or sets the event that shows the tooltip. Default: <see cref="TooltipTrigger.Hover"/>.
    /// </summary>
    [Parameter]
    public TooltipTrigger Trigger { get; set; } = TooltipTrigger.Hover;

    /// <summary>
    /// Gets or sets whether HTML is allowed in the tooltip text.
    /// <para><b>Warning:</b> only use with trusted / sanitised content.</para>
    /// </summary>
    [Parameter]
    public bool HtmlEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether a CSS fade transition is applied. Default: <c>true</c>.
    /// </summary>
    [Parameter]
    public bool Animation { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the tooltip text is HTML-sanitised. Default: <c>true</c>.
    /// </summary>
    [Parameter]
    public bool Sanitize { get; set; } = true;

    /// <summary>
    /// Gets or sets the delay, in milliseconds, before showing the tooltip. Default: <c>0</c>.
    /// </summary>
    [Parameter]
    public int DelayShow { get; set; }

    /// <summary>
    /// Gets or sets the delay, in milliseconds, before hiding the tooltip. Default: <c>0</c>.
    /// </summary>
    [Parameter]
    public int DelayHide { get; set; }

    /// <summary>
    /// Gets or sets the offset of the tooltip along the target edge (skidding), in pixels. Default: <c>0</c>.
    /// </summary>
    [Parameter]
    public int OffsetSkidding { get; set; }

    /// <summary>
    /// Gets or sets the distance of the tooltip from the target, in pixels. Default: <c>0</c>.
    /// </summary>
    [Parameter]
    public int OffsetDistance { get; set; }

    /// <summary>
    /// Gets or sets the placements tried, in order, when the preferred one overflows.
    /// Default: Top, Right, Bottom, Left.
    /// </summary>
    [Parameter]
    public IReadOnlyList<TooltipPlacement> FallbackPlacements { get; set; } =
        [TooltipPlacement.Top, TooltipPlacement.Right, TooltipPlacement.Bottom, TooltipPlacement.Left];

    /// <summary>The element that triggers the tooltip on hover/focus.</summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BitBlazor/js/bitblazor-interop.js");

        await _jsModule.InvokeVoidAsync(
            "initTooltipOnFirstChild", _spanRef, Text, BuildOptions());
    }

    private TooltipOptions BuildOptions() => new()
    {
        Animation = Animation,
        CustomClass = string.IsNullOrWhiteSpace(CssClass) ? null : CssClass,
        Delay = new TooltipDelay { Show = DelayShow, Hide = DelayHide },
        FallbackPlacements = FallbackPlacements.Select(p => p.ToString().ToLowerInvariant()).ToList(),
        Html = HtmlEnabled,
        Offset = $"{OffsetSkidding},{OffsetDistance}",
        Placement = Placement.ToString().ToLowerInvariant(),
        Sanitize = Sanitize,
        Trigger = Trigger.ToString().ToLowerInvariant()
    };

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (_jsModule is not null)
        {
            try
            {
                await _jsModule.InvokeVoidAsync("disposeTooltipOnFirstChild", _spanRef);
                await _jsModule.DisposeAsync();
            }
            catch (JSDisconnectedException) { }
        }
    }
}
