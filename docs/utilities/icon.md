# BitIcon

The `BitIcon` component represents an [icon using Bootstrap Italia icons](https://italia.github.io/bootstrap-italia/docs/utilities/icone/).

## Namespace

```csharp
BitBlazor.Utilities
```

## Description

The Icon component displays SVG icons from the Bootstrap Italia icon set using the SVG sprites system.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `IconName` | `string` | ✓ | `string.Empty` | The name of the icon to display |
| `Size` | `IconSize` | ✗ | `IconSize.Default` | The size of the icon |
| `Padded` | `bool` | ✗ | `false` | Indicates if the icon should have padding |
| `Color` | `IconColor` | ✗ | `IconColor.Default` | The color of the icon |
| `Align` | `IconAlignment` | ✗ | `IconAlignment.Default` | The alignment of the icon |
| `Role` | `string?` | ✗ | `null` | The role of the icon for accessibility |
| `Title` | `string?` | ✗ | `null` | The title of the icon for accessibility |
| `AriaHidden` | `bool` | ✗ | `false` | Indicates if the icon should be hidden from assistive technologies |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes to apply |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes |

## Used Enumerations

### IconSize
| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Default` | Standard size | - |
| `ExtraSmall` | Very small size | `icon-xs` |
| `Small` | Small size | `icon-sm` |
| `Large` | Large size | `icon-lg` |
| `ExtraLarge` | Very large size | `icon-xl` |

### IconColor
| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Default` | Default color | - |
| `Primary` | Primary color | `icon-primary` |
| `Secondary` | Secondary color | `icon-secondary` |
| `Success` | Success color | `icon-success` |
| `Warning` | Warning color | `icon-warning` |
| `Danger` | Danger color | `icon-danger` |
| `Light` | Light color | `icon-light` |
| `White` | White color | `icon-white` |

### IconAlignment
| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Default` | Default alignment | - |
| `Bottom` | Bottom alignment | `align-bottom` |
| `Middle` | Center alignment | `align-middle` |
| `Top` | Top alignment | `align-top` |

## Available Icons

The component uses icons from the Bootstrap Italia system. Icon names are available through the static `Icons` class.

The full list of available icons can be found in the Bootstrap Italia website:

https://italia.github.io/bootstrap-italia/docs/utilities/icone/#lista-delle-icone-disponibili

### Common icons (examples)
- `Icons.ItClose`: Close icon
- `Icons.ItCheck`: Confirmation icon
- `Icons.ItArrowRight`: Right arrow
- `Icons.ItArrowLeft`: Left arrow
- `Icons.ItUser`: User icon
- `Icons.ItSave`: Save icon
- `Icons.ItInfo`: Information icon
- `Icons.ItWarning`: Warning icon
- `Icons.ItDanger`: Danger icon

## Usage Examples

### Simple icon

```razor
<BitIcon IconName="@Icons.ItCheck" />
```

### Icon with custom size

```razor
<BitIcon IconName="@Icons.ItUser" Size="IconSize.Large" />
```

### Colored icon

```razor
<BitIcon IconName="@Icons.ItWarning" Color="IconColor.Warning" />
```

### Icon with padding

```razor
<BitIcon IconName="@Icons.ItInfo" 
         Color="IconColor.Primary" 
         Padded="true" />
```

### Icon with alignment

```razor
<span>
    Text with icon 
    <BitIcon IconName="@Icons.ItCheck" 
             Align="IconAlignment.Middle" 
             Color="IconColor.Success" />
    aligned to center
</span>
```

### Accessible icon

```razor
<BitIcon IconName="@Icons.ItSave" 
         Role="img" 
         Title="Save document" 
         Color="IconColor.Primary" />
```

### Decorative icon (hidden from assistive technologies)

```razor
<BitIcon IconName="@Icons.ItStar" 
         AriaHidden="true" 
         Color="IconColor.Warning" />
```

### Icon with custom CSS classes

```razor
<BitIcon IconName="@Icons.ItUser" 
         CssClass="my-custom-icon" 
         Size="IconSize.ExtraLarge" />
```

### Usage in a button

```razor
<button class="btn btn-primary">
    <BitIcon IconName="@Icons.ItSave" Color="IconColor.White" />
    Save
</button>
```

### Icons of different sizes

```razor
<BitIcon IconName="@Icons.ItStar" Size="IconSize.ExtraSmall" />
<BitIcon IconName="@Icons.ItStar" Size="IconSize.Small" />
<BitIcon IconName="@Icons.ItStar" Size="IconSize.Default" />
<BitIcon IconName="@Icons.ItStar" Size="IconSize.Large" />
<BitIcon IconName="@Icons.ItStar" Size="IconSize.ExtraLarge" />
```

### Colored icons

```razor
<BitIcon IconName="@Icons.ItCircle" Color="IconColor.Primary" />
<BitIcon IconName="@Icons.ItCircle" Color="IconColor.Secondary" />
<BitIcon IconName="@Icons.ItCircle" Color="IconColor.Success" />
<BitIcon IconName="@Icons.ItCircle" Color="IconColor.Warning" />
<BitIcon IconName="@Icons.ItCircle" Color="IconColor.Danger" />
```

## Generated HTML Structure

The component generates the following HTML structure:

```html
<svg class="icon icon-{size} icon-{color} align-{alignment}">
    <!-- Title (if provided) -->
    <title>Icon description</title>
    
    <!-- Icon reference -->
    <use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#{IconName}"></use>
</svg>
```

## Generated CSS Classes

The component generates the following CSS classes based on parameters:

### Base class
- `icon`: Base icon class

### Size classes
- `icon-xs`: Very small size
- `icon-sm`: Small size
- `icon-lg`: Large size
- `icon-xl`: Very large size

### Color classes
- `icon-primary`: Primary color
- `icon-secondary`: Secondary color
- `icon-success`: Success color
- `icon-warning`: Warning color
- `icon-danger`: Danger color
- `icon-light`: Light color
- `icon-white`: White color

### Alignment classes
- `align-bottom`: Bottom alignment
- `align-middle`: Center alignment
- `align-top`: Top alignment

### Additional classes
- `icon-padded`: Added when `Padded` is `true`

## Accessibility

The component supports various accessibility options:

- **Role**: Specifies the role of the icon (e.g., "img" for decorative icons)
- **Title**: Provides a textual description of the icon
- **AriaHidden**: Hides the icon from assistive technologies when it's purely decorative

### Accessibility Guidelines

1. **Decorative icons**: Use `AriaHidden="true"` for purely decorative icons
2. **Informative icons**: Always provide a `Title` and `Role="img"`
3. **Interactive icons**: Ensure the parent element has an appropriate label

## Technical Implementation

- Uses the Bootstrap Italia SVG sprites system
- Icon path is `/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#{IconName}`
- Renders as an `<svg>` element with internal `<use>` element
- Supports custom HTML attributes through the attribute splatting system (`AdditionalAttributes`)
- The `role` attribute is automatically managed based on the `Role` parameter
- When a `Title` is provided, it's rendered as a `<title>` element inside the SVG for accessibility

## Notes

- Icons are loaded from the Bootstrap Italia sprites SVG file
- Custom CSS classes can be added through the `CssClass` parameter
- The component automatically manages `role` and `aria-hidden` attributes based on parameters
- For optimal performance, the sprites file is loaded only once for the entire application
