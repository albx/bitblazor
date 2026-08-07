using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BitBlazor.Components;

/// <summary>
/// Represents a popover component using Bootstrap Italia styles.
/// </summary>
/// <remarks>
/// Wraps the trigger element in a <c>&lt;span&gt;</c> with the appropriate
/// Bootstrap Italia <c>data-bs-*</c> attributes. The Bootstrap Italia JS is
/// initialised automatically via JS interop after every first render, loading
/// the bundle on-demand when not already present on the page.
/// </remarks>
public partial class BitPopover : BitComponentBase, IAsyncDisposable
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    private IJSObjectReference? _jsModule;
    private ElementReference _spanRef;

    /// <summary>
    /// Gets or sets the title displayed in the popover header.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the body content of the popover.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the placement of the popover relative to its trigger element.
    /// Defaults to <see cref="PopoverPlacement.Top"/>.
    /// </summary>
    [Parameter]
    public PopoverPlacement Placement { get; set; } = PopoverPlacement.Top;

    /// <summary>
    /// Gets or sets how the popover is triggered.
    /// Defaults to <see cref="PopoverTrigger.Click"/>.
    /// </summary>
    [Parameter]
    public PopoverTrigger Trigger { get; set; } = PopoverTrigger.Click;

    /// <summary>
    /// Gets or sets a value indicating whether HTML markup is allowed in the popover title and content.
    /// When <c>true</c>, the <c>data-bs-html</c> attribute is set to <c>"true"</c>.
    /// </summary>
    /// <remarks>
    /// Enabling HTML in popover content may expose your application to XSS attacks.
    /// Only use this option with trusted, sanitised content.
    /// </remarks>
    [Parameter]
    public bool HtmlEnabled { get; set; }

    /// <summary>
    /// Gets or sets the content that acts as the popover trigger.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    private string PlacementValue => Placement switch
    {
        PopoverPlacement.Auto   => "auto",
        PopoverPlacement.Top    => "top",
        PopoverPlacement.Bottom => "bottom",
        PopoverPlacement.Left   => "left",
        PopoverPlacement.Right  => "right",
        _                       => "top"
    };

    private string TriggerValue => Trigger switch
    {
        PopoverTrigger.Click  => "click",
        PopoverTrigger.Hover  => "hover",
        PopoverTrigger.Focus  => "focus",
        PopoverTrigger.Manual => "manual",
        _                     => "click"
    };

    private string HtmlEnabledValue => HtmlEnabled ? "true" : "false";

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BitBlazor/js/bitblazor-interop.js");

        await _jsModule.InvokeVoidAsync("initPopover", _spanRef);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (_jsModule is not null)
        {
            try
            {
                await _jsModule.InvokeVoidAsync("disposePopover", _spanRef);
                await _jsModule.DisposeAsync();
            }
            catch (JSDisconnectedException) { }
        }
    }
}
