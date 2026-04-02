using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BitBlazor.Components;

/// <summary>
/// Represents a tooltip component using Bootstrap Italia styles.
/// </summary>
/// <remarks>
/// Wraps the trigger element in a <c>&lt;span&gt;</c> with the appropriate
/// Bootstrap Italia <c>data-bs-*</c> attributes. The Bootstrap Italia JS is
/// initialised automatically via JS interop after every first render, loading
/// the bundle on-demand when not already present on the page.
/// </remarks>
public partial class BitTooltip : BitComponentBase, IAsyncDisposable
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    private IJSObjectReference? _jsModule;
    private ElementReference _spanRef;

    /// <summary>
    /// Gets or sets the text displayed inside the tooltip.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the placement of the tooltip relative to its trigger element.
    /// Defaults to <see cref="TooltipPlacement.Top"/>.
    /// </summary>
    [Parameter]
    public TooltipPlacement Placement { get; set; } = TooltipPlacement.Top;

    /// <summary>
    /// Gets or sets a value indicating whether HTML markup is allowed in the tooltip text.
    /// When <c>true</c>, the <c>data-bs-html</c> attribute is set to <c>"true"</c>.
    /// </summary>
    /// <remarks>
    /// Enabling HTML in tooltips may expose your application to XSS attacks.
    /// Only use this option with trusted, sanitised content.
    /// </remarks>
    [Parameter]
    public bool HtmlEnabled { get; set; }

    /// <summary>
    /// Gets or sets the content that acts as the tooltip trigger.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    private string PlacementValue => Placement switch
    {
        TooltipPlacement.Auto   => "auto",
        TooltipPlacement.Top    => "top",
        TooltipPlacement.Bottom => "bottom",
        TooltipPlacement.Left   => "left",
        TooltipPlacement.Right  => "right",
        _                       => "top"
    };

    private string HtmlEnabledValue => HtmlEnabled ? "true" : "false";

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BitBlazor/js/bitblazor-interop.js");

        await _jsModule.InvokeVoidAsync("initTooltip", _spanRef);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (_jsModule is not null)
        {
            try
            {
                await _jsModule.InvokeVoidAsync("disposeTooltip", _spanRef);
                await _jsModule.DisposeAsync();
            }
            catch (JSDisconnectedException) { }
        }
    }
}
