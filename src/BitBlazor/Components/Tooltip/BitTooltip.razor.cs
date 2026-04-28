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
    /// Bootstrap Italia tooltip options passed to the <c>Tooltip</c> constructor.
    /// When <c>null</c>, Bootstrap defaults apply.
    /// </summary>
    [Parameter]
    public TooltipOptions? Options { get; set; }

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
            "initTooltipOnFirstChild", _spanRef, Text, Options ?? new TooltipOptions());
    }

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
