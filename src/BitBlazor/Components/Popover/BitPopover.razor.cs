using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BitBlazor.Components;

/// <summary>
/// Wraps its child content and attaches a Bootstrap Italia popover
/// directly to the first child element.
/// </summary>
public partial class BitPopover : BitComponentBase, IAsyncDisposable
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    private const string ModulePath = "./_content/BitBlazor/Components/Popover/BitPopover.razor.js";

    private IJSObjectReference? _module;
    private IJSObjectReference? _instance;
    private ElementReference _wrapperRef;
    private bool _disposed;

    /// <summary>
    /// The text displayed in the popover header.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// The body content of the popover.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the popover placement. Default: <see cref="PopoverPlacement.Top"/>.
    /// </summary>
    [Parameter]
    public PopoverPlacement Placement { get; set; } = PopoverPlacement.Top;

    /// <summary>
    /// Gets or sets the event that opens the popover. Default: <see cref="PopoverTrigger.Click"/>.
    /// </summary>
    [Parameter]
    public PopoverTrigger Trigger { get; set; } = PopoverTrigger.Click;

    /// <summary>
    /// Gets or sets whether HTML is allowed in the popover title and content.
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
    /// Gets or sets whether the title and content are HTML-sanitised. Default: <c>true</c>.
    /// </summary>
    [Parameter]
    public bool Sanitize { get; set; } = true;

    /// <summary>
    /// Gets or sets the delay, in milliseconds, before showing the popover. Default: <c>0</c>.
    /// </summary>
    [Parameter]
    public int DelayShow { get; set; }

    /// <summary>
    /// Gets or sets the delay, in milliseconds, before hiding the popover. Default: <c>0</c>.
    /// </summary>
    [Parameter]
    public int DelayHide { get; set; }

    /// <summary>
    /// Gets or sets the offset of the popover along the target edge (skidding), in pixels. Default: <c>0</c>.
    /// </summary>
    [Parameter]
    public int OffsetSkidding { get; set; }

    /// <summary>
    /// Gets or sets the distance of the popover from the target, in pixels. Default: <c>0</c>.
    /// </summary>
    [Parameter]
    public int OffsetDistance { get; set; }

    /// <summary>
    /// Gets or sets the placements tried, in order, when the preferred one overflows.
    /// Default: Top, Right, Bottom, Left.
    /// </summary>
    [Parameter]
    public IReadOnlyList<PopoverPlacement> FallbackPlacements { get; set; } =
        [PopoverPlacement.Top, PopoverPlacement.Right, PopoverPlacement.Bottom, PopoverPlacement.Left];

    /// <summary>The element that triggers the popover.</summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", ModulePath);
        var instance = await _module.InvokeAsync<IJSObjectReference?>(
            "BitPopover.create", _wrapperRef, Title, Content, BuildOptions());

        if (_disposed)
        {
            // Disposed while the import or init was pending: release immediately.
            await ReleaseAsync(instance);
            return;
        }

        _instance = instance;
    }

    private PopoverOptions BuildOptions() => new()
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
        _disposed = true;

        try
        {
            await ReleaseAsync(_instance);
            _instance = null;

            if (_module is not null)
            {
                await _module.DisposeAsync();
                _module = null;
            }
        }
        catch (JSDisconnectedException) { }
        catch (TaskCanceledException) { }
    }

    private static async Task ReleaseAsync(IJSObjectReference? instance)
    {
        if (instance is null) return;

        await instance.InvokeVoidAsync("dispose");
        await instance.DisposeAsync();
    }
}
