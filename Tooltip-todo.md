## BitTooltip — Tooltip on Child Content

### 1. Razor markup

```razor
@namespace BitBlazor.Components

@inherits BitComponentBase
@implements IAsyncDisposable

<bit-tooltip @ref="_spanRef" style="display:contents" @attributes="AdditionalAttributes">
    @ChildContent
</bit-tooltip>
```

---

### 2. Code-behind (`BitTooltip.razor.cs`)

```csharp
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
```

---

### 3. Options model (`TooltipOptions.cs`)

```csharp
using System.Text.Json.Serialization;

namespace BitBlazor.Components;

/// <summary>
/// Strongly-typed options for the Bootstrap Italia Tooltip component.
/// Property names are serialized to camelCase to match the Bootstrap JS API.
/// </summary>
public class TooltipOptions
{
    /// <summary>Applies a CSS fade transition. Default: <c>true</c>.</summary>
    [JsonPropertyName("animation")]
    public bool Animation { get; set; } = true;

    /// <summary>
    /// Overflow constraint boundary of the tooltip.
    /// Default: <c>"clippingParents"</c>.
    /// </summary>
    [JsonPropertyName("boundary")]
    public string Boundary { get; set; } = "clippingParents";

    /// <summary>
    /// Appends the tooltip to a specific element, e.g. <c>"body"</c>.
    /// Default: <c>null</c> (appended after the trigger element).
    /// </summary>
    [JsonPropertyName("container")]
    public string? Container { get; set; }

    /// <summary>Extra CSS classes added to the tooltip when shown.</summary>
    [JsonPropertyName("customClass")]
    public string? CustomClass { get; set; }

    /// <summary>Show/hide delay in milliseconds.</summary>
    [JsonPropertyName("delay")]
    public TooltipDelay Delay { get; set; } = new();

    /// <summary>
    /// Fallback placements tried when the preferred one overflows.
    /// Default: <c>["top","right","bottom","left"]</c>.
    /// </summary>
    [JsonPropertyName("fallbackPlacements")]
    public IList<string> FallbackPlacements { get; set; } =
        ["top", "right", "bottom", "left"];

    /// <summary>
    /// Allow HTML in the tooltip title.
    /// <para><b>Warning:</b> only use with trusted / sanitised content.</para>
    /// </summary>
    [JsonPropertyName("html")]
    public bool Html { get; set; }

    /// <summary>
    /// Offset of the tooltip relative to its target, as <c>"skidding,distance"</c>.
    /// Default: <c>"0,0"</c>.
    /// </summary>
    [JsonPropertyName("offset")]
    public string Offset { get; set; } = "0,0";

    /// <summary>
    /// Tooltip placement: <c>auto</c>, <c>top</c>, <c>bottom</c>,
    /// <c>left</c>, <c>right</c>. Default: <c>"top"</c>.
    /// </summary>
    [JsonPropertyName("placement")]
    public string Placement { get; set; } = "top";

    /// <summary>Enable HTML sanitisation of title/template. Default: <c>true</c>.</summary>
    [JsonPropertyName("sanitize")]
    public bool Sanitize { get; set; } = true;

    /// <summary>
    /// CSS selector used for tooltip delegation on dynamically added elements.
    /// </summary>
    [JsonPropertyName("selector")]
    public string? Selector { get; set; }

    /// <summary>Custom HTML template used to create the tooltip.</summary>
    [JsonPropertyName("template")]
    public string? Template { get; set; }

    /// <summary>
    /// Events that trigger the tooltip: <c>click</c>, <c>hover</c>, <c>focus</c>,
    /// <c>manual</c> (space-separated). Default: <c>"hover focus"</c>.
    /// </summary>
    [JsonPropertyName("trigger")]
    public string Trigger { get; set; } = "hover focus";
}

/// <summary>Show/hide delay configuration for a tooltip.</summary>
public class TooltipDelay
{
    /// <summary>Delay before showing the tooltip (ms). Default: <c>0</c>.</summary>
    [JsonPropertyName("show")]
    public int Show { get; set; } = 0;

    /// <summary>Delay before hiding the tooltip (ms). Default: <c>0</c>.</summary>
    [JsonPropertyName("hide")]
    public int Hide { get; set; } = 0;
}
```

---

### 4. JS interop (`bitblazor-interop.js`)

```javascript
/**
 * Initialises a Bootstrap Italia Tooltip on the first child element
 * of the given wrapper element.
 *
 * @param {HTMLElement} wrapper  - The <bit-tooltip> wrapper element.
 * @param {string}      title    - The tooltip text.
 * @param {object}      options  - Tooltip options (matches Bootstrap JS API).
 */
export async function initTooltipOnFirstChild(wrapper, title, options) {
    const target = wrapper.firstElementChild;
    if (!target) {
        console.warn('[BitTooltip] No child element found for tooltip.');
        return;
    }

    // Set the title on the real target, not the wrapper.
    target.setAttribute('title', title);

    const { Tooltip } = await import('bootstrap-italia');
    const cleaned = Object.fromEntries(
        Object.entries(options).filter(([, v]) => v !== null && v !== undefined)
    );
    new Tooltip(target, cleaned);
}

/**
 * Disposes the Bootstrap Italia Tooltip instance from the first child
 * element of the given wrapper.
 *
 * @param {HTMLElement} wrapper
 */
export async function disposeTooltipOnFirstChild(wrapper) {
    const target = wrapper.firstElementChild;
    if (!target) return;

    const { Tooltip } = await import('bootstrap-italia');
    Tooltip.getInstance(target)?.dispose();
}
```

---

### 5. Usage example

```razor
<BitTooltip Text="Hello from tooltip!"
            Options="@(new TooltipOptions
            {
                Placement = "bottom",
                Trigger   = "hover",
                Delay     = new() { Show = 200, Hide = 100 }
            })">
    <button class="btn btn-primary">Hover me</button>
</BitTooltip>
```

---

### How it works

```
<bit-tooltip style="display:contents">   ← wrapper, layout-invisible
    <button ...>                          ← firstElementChild ← tooltip lives here
        Click me
    </button>
</bit-tooltip>
```

| Step | What happens |
|---|---|
| Blazor renders | `<bit-tooltip>` with `display:contents` wraps `ChildContent` — no visual impact |
| `OnAfterRenderAsync` | JS receives the wrapper ref and walks to `firstElementChild` |
| `setAttribute('title', ...)` | The title is placed on the real trigger element |
| `new Tooltip(target, options)` | Bootstrap Italia binds to the child, not the wrapper |
| `DisposeAsync` | `getInstance` is called on the same child to clean up |