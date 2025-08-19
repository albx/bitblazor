# BitBadge

The `BitBadge` component represents a [badge using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/badge/).

## Namespace

```csharp
BitBlazor.Components
```

## Description

The Badge component provides small and adaptable labels for adding information to any content.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Text` | `string` | ✓ | `string.Empty` | The text to display in the badge |
| `BackgroundColor` | `Color` | ✓ | - | The background color of the badge |
| `Variant` | `Variant` | ✗ | `Variant.Solid` | The variant of the badge (solid or outline) |
| `Rounded` | `bool` | ✗ | `false` | Indicates if the badge should be rounded |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes to apply |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes |

## Used Enumerations

### Color
- `Primary`: Primary color
- `Secondary`: Secondary color  
- `Success`: Success color
- `Warning`: Warning color
- `Danger`: Danger color

### Variant
- `Solid`: Badge with full background
- `Outline`: Badge with only colored border

## Usage Examples

### Simple badge

```razor
<BitBadge Text="New" BackgroundColor="Color.Primary" />
```

### Outline badge

```razor
<BitBadge Text="Pending" 
          BackgroundColor="Color.Warning" 
          Variant="Variant.Outline" />
```

### Rounded badge

```razor
<BitBadge Text="42" 
          BackgroundColor="Color.Success" 
          Rounded="true" />
```

### Usage in a component

```razor
<h3>
    Notifications 
    <BitBadge Text="5" 
              BackgroundColor="Color.Danger" 
              Rounded="true" />
</h3>
```

### Badges with different variants

```razor
<!-- Solid badges -->
<BitBadge Text="Primary" BackgroundColor="Color.Primary" />
<BitBadge Text="Success" BackgroundColor="Color.Success" />
<BitBadge Text="Danger" BackgroundColor="Color.Danger" />

<!-- Outline badges -->
<BitBadge Text="Primary" BackgroundColor="Color.Primary" Variant="Variant.Outline" />
<BitBadge Text="Success" BackgroundColor="Color.Success" Variant="Variant.Outline" />
<BitBadge Text="Danger" BackgroundColor="Color.Danger" Variant="Variant.Outline" />
```

## Generated HTML Structure

The component generates the following HTML structure:

```html
<span class="badge bg-{color} rounded-pill">Badge text</span>
<!-- OR for outline variant -->
<span class="badge bg-white text-{color} rounded-pill">Badge text</span>
```

## Generated CSS Classes

The component generates the following CSS classes based on parameters:

### Base class
- `badge`: Base badge class

### Color classes (Variant.Solid)
- `bg-primary`: Primary background
- `bg-secondary`: Secondary background
- `bg-success`: Success background
- `bg-warning`: Warning background
- `bg-danger`: Danger background

### Color classes (Variant.Outline)
- `bg-white text-primary`: White background with primary text
- `bg-white text-secondary`: White background with secondary text
- `bg-white text-success`: White background with success text
- `bg-white text-warning`: White background with warning text
- `bg-white text-danger`: White background with danger text

### Shape class
- `rounded-pill`: Added when `Rounded` is `true`

## Notes

- The badge is an inline element that automatically adapts to surrounding content
- The outline variant is useful when you want a more subtle appearance
- Color classes are dynamically generated based on the combination of `BackgroundColor` and `Variant` properties
