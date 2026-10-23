# BitPopover

The `BitPopover` component attaches a [popover using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/popover/) to its child element.

## Namespace

```csharp
BitBlazor.Components
```

## Description

`BitPopover` wraps its child content in a layout-neutral `<bit-popover>` element (`display: contents`) and initializes the Bootstrap Italia popover on the **first child element**. Use it to show a small overlay with an optional title and a body text when the user interacts with an element.

The component requires JavaScript interop: the popover is created after the first render and disposed together with the component.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Content` | `string` | ✓ | `""` | The body of the popover |
| `ChildContent` | `RenderFragment?` | ✓ | - | The element the popover is attached to (its first child element) |
| `Title` | `string?` | ✗ | `null` | The text displayed in the popover header. No header is shown when empty |
| `Placement` | `PopoverPlacement` | ✗ | `PopoverPlacement.Top` | Where the popover is placed relative to the element |
| `Trigger` | `PopoverTrigger` | ✗ | `PopoverTrigger.Click` | The event that opens the popover |
| `HtmlEnabled` | `bool` | ✗ | `false` | Allows HTML in `Title` and `Content`. Only use with trusted content |
| `Animation` | `bool` | ✗ | `true` | Applies a CSS fade transition |
| `Sanitize` | `bool` | ✗ | `true` | Sanitizes the title and content when HTML is enabled |
| `DelayShow` | `int` | ✗ | `0` | Delay in milliseconds before showing the popover |
| `DelayHide` | `int` | ✗ | `0` | Delay in milliseconds before hiding the popover |
| `OffsetSkidding` | `int` | ✗ | `0` | Offset in pixels along the edge of the element |
| `OffsetDistance` | `int` | ✗ | `0` | Distance in pixels from the element |
| `FallbackPlacements` | `IReadOnlyList<PopoverPlacement>` | ✗ | `Top, Right, Bottom, Left` | Placements tried, in order, when the preferred one does not fit |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes applied to the generated popover element |
| `Id` | `string?` | ✗ | `null` | The `id` of the `<bit-popover>` wrapper element |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes forwarded to the `<bit-popover>` wrapper element |

> **Note:** `CssClass` is applied to the popover that Bootstrap Italia creates, not to the wrapper element.

## Used Enumerations

### PopoverPlacement

| Value | Description |
|-------|-------------|
| `Auto` | Placed automatically based on the available space |
| `Top` | Placed above the element |
| `Bottom` | Placed below the element |
| `Left` | Placed to the left of the element |
| `Right` | Placed to the right of the element |

### PopoverTrigger

| Value | Description |
|-------|-------------|
| `Click` | Opened when the element is clicked |
| `Hover` | Opened when the pointer hovers over the element |
| `Focus` | Opened when the element receives focus |
| `Manual` | Controlled programmatically via JavaScript |

## Accessibility

- The target element should be focusable and operable with the keyboard (for example a `<button>`), so the popover can be opened without a pointer. Avoid attaching a popover to a plain `<span>` unless it is made focusable.
- Do not rely on `Hover` alone: keyboard and touch users cannot trigger it.
- Keep popover content short and non-essential; do not place interactive controls inside it.

## Usage Examples

### Basic usage

```razor
<BitPopover Title="Popover title" Content="Popover body text.">
    <button type="button" class="btn btn-danger">Click to open</button>
</BitPopover>
```

### Placement

```razor
<BitPopover Title="On the right" Content="Popover content." Placement="PopoverPlacement.Right">
    <button type="button" class="btn btn-secondary">Right</button>
</BitPopover>
```

### Hover trigger

```razor
<BitPopover Title="Hover trigger"
            Content="This popover opens on mouse hover."
            Trigger="PopoverTrigger.Hover">
    <button type="button" class="btn btn-secondary">Hover me</button>
</BitPopover>
```

### Without title

```razor
<BitPopover Content="Popover with text content only.">
    <button type="button" class="btn btn-secondary">No title</button>
</BitPopover>
```

### With delay, offset and fallback placements

```razor
<BitPopover Content="Positioned popover"
            Placement="PopoverPlacement.Bottom"
            FallbackPlacements="@(new[] { PopoverPlacement.Top, PopoverPlacement.Right })"
            OffsetDistance="8"
            DelayShow="200">
    <button type="button" class="btn btn-secondary">Open</button>
</BitPopover>
```

### With HTML content

```razor
<BitPopover Title="<strong>Title</strong> with HTML"
            Content="Content with <em>HTML</em> enabled."
            HtmlEnabled="true">
    <button type="button" class="btn btn-secondary">Popover with HTML</button>
</BitPopover>
```
