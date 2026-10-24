# BitTooltip

The `BitTooltip` component attaches a [tooltip using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/tooltip/) to its child element.

## Namespace

```csharp
BitBlazor.Components
```

## Description

`BitTooltip` wraps its child content in a layout-neutral `<bit-tooltip>` element (`display: contents`) and initializes the Bootstrap Italia tooltip on the **first child element**. Use it to show a short, non-interactive hint when the user hovers or focuses an element.

The component requires JavaScript interop: the tooltip is created after the first render and disposed together with the component. No script setup is needed: the Bootstrap Italia JavaScript is loaded on demand. See the [JavaScript section of the README](../../README.md#javascript) for the supported version, bring-your-own rules and CSP requirements.
## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Text` | `string` | ✓ | `""` | The text displayed inside the tooltip |
| `ChildContent` | `RenderFragment?` | ✓ | - | The element the tooltip is attached to (its first child element) |
| `Placement` | `TooltipPlacement` | ✗ | `TooltipPlacement.Top` | Where the tooltip is placed relative to the element |
| `Trigger` | `TooltipTrigger` | ✗ | `TooltipTrigger.Hover \| TooltipTrigger.Focus` | The event(s) that show the tooltip; flags can be combined |
| `HtmlEnabled` | `bool` | ✗ | `false` | Allows HTML in `Text`. Only use with trusted content |
| `Animation` | `bool` | ✗ | `true` | Applies a CSS fade transition |
| `Sanitize` | `bool` | ✗ | `true` | Sanitizes the tooltip text when HTML is enabled |
| `DelayShow` | `int` | ✗ | `0` | Delay in milliseconds before showing the tooltip |
| `DelayHide` | `int` | ✗ | `0` | Delay in milliseconds before hiding the tooltip |
| `OffsetSkidding` | `int` | ✗ | `0` | Offset in pixels along the edge of the element |
| `OffsetDistance` | `int` | ✗ | `0` | Distance in pixels from the element |
| `FallbackPlacements` | `IReadOnlyList<TooltipPlacement>` | ✗ | `Top, Right, Bottom, Left` | Placements tried, in order, when the preferred one does not fit |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes applied to the generated tooltip element |
| `Id` | `string?` | ✗ | `null` | The `id` of the `<bit-tooltip>` wrapper element |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes forwarded to the `<bit-tooltip>` wrapper element |

> **Note:** `CssClass` is applied to the tooltip that Bootstrap Italia creates, not to the wrapper element.

## Used Enumerations

### TooltipPlacement

| Value | Description |
|-------|-------------|
| `Auto` | Placed automatically based on the available space |
| `Top` | Placed above the element |
| `Bottom` | Placed below the element |
| `Left` | Placed to the left of the element |
| `Right` | Placed to the right of the element |

### TooltipTrigger

`TooltipTrigger` is a `[Flags]` enum: values can be combined with `|` (for example `TooltipTrigger.Click | TooltipTrigger.Focus`). `Manual` cannot be combined; when set, it takes precedence over the other flags.

| Value | Description |
|-------|-------------|
| `Hover` | Shown when the pointer hovers over the element |
| `Focus` | Shown when the element receives focus |
| `Click` | Shown when the element is clicked |
| `Manual` | Controlled programmatically via JavaScript |

## Accessibility

- The default trigger is `Hover | Focus`, so keyboard users can reach the tooltip on focusable elements (buttons, links). Avoid restricting `Trigger` to `Hover` alone.
- Do not put essential information or interactive content in a tooltip; use [`BitPopover`](popover.md) instead.
- Bootstrap Italia sets `aria-describedby` on the target element while the tooltip is visible.

## Usage Examples

### Basic usage

```razor
<BitTooltip Text="Tooltip text">
    <button type="button" class="btn btn-secondary">Hover me</button>
</BitTooltip>
```

### Placement

```razor
<BitTooltip Text="Shown on the right" Placement="TooltipPlacement.Right">
    <button type="button" class="btn btn-secondary">Right</button>
</BitTooltip>
```

### On keyboard focus

```razor
<BitTooltip Text="Shown when focused" Trigger="TooltipTrigger.Focus">
    <button type="button" class="btn btn-secondary">Focus me</button>
</BitTooltip>
```

### With delay and offset

```razor
<BitTooltip Text="Delayed tooltip"
            DelayShow="300"
            DelayHide="100"
            OffsetDistance="8">
    <button type="button" class="btn btn-secondary">Hover me</button>
</BitTooltip>
```

### With HTML content

```razor
<BitTooltip Text="<em>Tooltip</em> with <strong>HTML</strong>" HtmlEnabled="true">
    <button type="button" class="btn btn-secondary">Tooltip with HTML</button>
</BitTooltip>
```

### On a link

```razor
<p>
    Hover over
    <BitTooltip Text="This is a tooltip on a link" Placement="TooltipPlacement.Top">
        <a href="#">this link</a>
    </BitTooltip>
    to see the tooltip.
</p>
```
