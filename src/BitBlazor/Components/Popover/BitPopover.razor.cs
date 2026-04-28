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

    private IJSObjectReference? _jsModule;
    private ElementReference _spanRef;

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
    /// Bootstrap Italia popover options passed to the <c>Popover</c> constructor.
    /// When <c>null</c>, Bootstrap defaults apply.
    /// </summary>
    [Parameter]
    public PopoverOptions? Options { get; set; }

    /// <summary>The element that triggers the popover.</summary>
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
            "initPopoverOnFirstChild", _spanRef, Title, Content, Options ?? new PopoverOptions());
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (_jsModule is not null)
        {
            try
            {
                await _jsModule.InvokeVoidAsync("disposePopoverOnFirstChild", _spanRef);
                await _jsModule.DisposeAsync();
            }
            catch (JSDisconnectedException) { }
        }
    }
}
