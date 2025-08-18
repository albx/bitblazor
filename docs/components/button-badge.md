# ButtonBadge

The `ButtonBadge` component represents a badge designed to be used within a `BitButton`.

## Namespace

```csharp
BitBlazor.Components
```

## Description

The ButtonBadge component displays a badge with customizable text that automatically integrates with the parent button, adapting its appearance based on the button's properties.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Text` | `string` | ✓ | `string.Empty` | The text to display in the badge |
| `AdditionalText` | `string?` | ✗ | `null` | Additional text for context, accessible only to assistive technologies |

## Features

### Automatic integration
The ButtonBadge automatically integrates with the parent button through `CascadingParameter`, inheriting:
- **Color**: Uses the same color as the parent button
- **Variant**: Automatically inverts the variant (if the button is Solid, the badge will be Outline and vice versa)

### Accessibility
- The `AdditionalText` parameter provides additional context for screen readers
- Additional text is visually hidden but accessible to assistive technologies

## Usage Examples

### Simple badge in a button

```razor
<BitButton Color="Color.Primary">
    Messages
    <ButtonBadge Text="3" />
</BitButton>
```

### Badge with additional text for accessibility

```razor
<BitButton Color="Color.Warning">
    Notifications
    <ButtonBadge Text="12" 
                 AdditionalText="unread notifications" />
</BitButton>
```

### Badge in buttons of different variants

```razor
<!-- Solid button with Outline badge -->
<BitButton Color="Color.Success" Variant="Variant.Solid">
    Completed
    <ButtonBadge Text="5" />
</BitButton>

<!-- Outline button with Solid badge -->
<BitButton Color="Color.Danger" Variant="Variant.Outline">
    Errors
    <ButtonBadge Text="2" />
</BitButton>
```

### Badge in buttons of different colors

```razor
<BitButton Color="Color.Primary">
    Primary <ButtonBadge Text="1" />
</BitButton>

<BitButton Color="Color.Success">
    Success <ButtonBadge Text="10" />
</BitButton>

<BitButton Color="Color.Warning">
    Warning <ButtonBadge Text="!" />
</BitButton>

<BitButton Color="Color.Danger">
    Danger <ButtonBadge Text="99+" />
</BitButton>
```

### Badge with icon in button

```razor
<BitButton Color="Color.Info" Icon="@Icons.ItMail">
    Email
    <ButtonBadge Text="5" 
                 AdditionalText="new emails" />
</BitButton>
```

### Badge in buttons of different sizes

```razor
<BitButton Color="Color.Primary" Size="Size.Small">
    Small <ButtonBadge Text="2" />
</BitButton>

<BitButton Color="Color.Primary">
    Standard <ButtonBadge Text="15" />
</BitButton>

<BitButton Color="Color.Primary" Size="Size.Large">
    Large <ButtonBadge Text="100" />
</BitButton>
```

## Automatic Behavior

### Variant inversion
The badge automatically inverts the variant compared to the parent button:

| Button | Badge |
|--------|-------|
| `Variant.Solid` | `Variant.Outline` |
| `Variant.Outline` | `Variant.Solid` |

This ensures optimal contrast and clear visual distinction.

### Color inheritance
The badge automatically inherits the color from the parent button, ensuring visual consistency.

## Generated HTML

The component generates the following HTML:

```html
<!-- Visible badge -->
<span class="badge bg-white text-primary">3</span>

<!-- Additional text (if provided) -->
<span class="visually-hidden">unread notifications</span>
```

## Accessibility

### Additional text
The `AdditionalText` parameter is crucial for accessibility:

```razor
<!-- Good: provides context -->
<ButtonBadge Text="5" AdditionalText="unread messages" />

<!-- Less accessible: without context -->
<ButtonBadge Text="5" />
```

### Guidelines
1. **Always provide context**: Use `AdditionalText` to explain what the number represents
2. **Concise text**: Additional text should be brief but descriptive
3. **Units of measurement**: Include units when appropriate (e.g., "items", "notifications", "errors")

## Implementation Notes

- Requires a `BitButton` as parent (via `CascadingParameter`)
- Cannot be used as a standalone component
- Appearance is automatically determined by parent button properties
- Uses the `BitBadge` component internally for rendering
- The `visually-hidden` class hides additional text while keeping it accessible to screen readers

## Common Use Cases

1. **Notification counters**: Show the number of unread notifications
2. **Status indicators**: Highlight items that require attention
3. **Item counters**: Display the number of items in a collection
4. **Error indicators**: Show the number of errors or warnings
5. **Informational badges**: Add additional information to an action
