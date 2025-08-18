# BitButton

The `BitButton` component represents a [button using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/buttons/).

## Namespace

```csharp
BitBlazor.Components
```

## Description

The Button component provides interactive buttons with support for icons, different styles, sizes, and states.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `ChildContent` | `RenderFragment` | ✓ | - | The content of the button |
| `Color` | `Color` | ✓ | - | The color of the button |
| `Variant` | `Variant` | ✗ | `Variant.Solid` | The variant of the button (solid or outline) |
| `Type` | `ButtonType` | ✗ | `ButtonType.Button` | The type of button (Button, Submit, Reset) |
| `Size` | `Size` | ✗ | `Size.Default` | The size of the button |
| `Disabled` | `bool` | ✗ | `false` | Indicates if the button is disabled |
| `Icon` | `string?` | ✗ | `null` | The name of the icon to display |
| `IconRounded` | `bool` | ✗ | `false` | Indicates if the icon should be rounded |
| `IconPosition` | `IconPosition` | ✗ | `IconPosition.Start` | The position of the icon in the button |
| `OnClick` | `EventCallback` | ✗ | - | Callback invoked when the button is clicked |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes to apply |

## Used Enumerations

### ButtonType
| Value | Description |
|-------|-------------|
| `Button` | Generic button (default) |
| `Submit` | Form submit button |
| `Reset` | Form reset button |

### IconPosition
| Value | Description |
|-------|-------------|
| `Start` | Icon at the beginning of the button |
| `End` | Icon at the end of the button |

### Size
| Value | Description |
|-------|-------------|
| `Default` | Standard size |
| `Small` | Small size |
| `Large` | Large size |
| `Mini` | Very small size |

### Color
| Value | Description |
|-------|-------------|
| `Primary` | Primary color |
| `Secondary` | Secondary color |
| `Success` | Success color |
| `Warning` | Warning color |
| `Danger` | Danger color |

### Variant
| Value | Description |
|-------|-------------|
| `Solid` | Button with full background |
| `Outline` | Button with only colored border |

## Usage Examples

### Simple button

```razor
<BitButton Color="Color.Primary">
    Click here
</BitButton>
```

### Button with icon

```razor
<BitButton Color="Color.Success" 
           Icon="@Icons.ItCheck">
    Confirm
</BitButton>
```

### Button with icon at the end

```razor
<BitButton Color="Color.Primary" 
           Icon="@Icons.ItArrowRight" 
           IconPosition="IconPosition.End">
    Next
</BitButton>
```

### Outline button

```razor
<BitButton Color="Color.Secondary" 
           Variant="Variant.Outline">
    Cancel
</BitButton>
```

### Buttons of different sizes

```razor
<BitButton Color="Color.Primary" Size="Size.Small">Small</BitButton>
<BitButton Color="Color.Primary">Standard</BitButton>
<BitButton Color="Color.Primary" Size="Size.Large">Large</BitButton>
```

### Disabled button

```razor
<BitButton Color="Color.Primary" Disabled="true">
    Not available
</BitButton>
```

### Button with click handling

```razor
<BitButton Color="Color.Success" 
           OnClick="HandleClick">
    Save
</BitButton>

@code {
    private async Task HandleClick()
    {
        // Click handling logic
        Console.WriteLine("Button clicked!");
    }
}
```

### Submit button for forms

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <!-- Other form fields -->
    
    <BitButton Type="ButtonType.Submit" 
               Color="Color.Primary"
               Icon="@Icons.ItSave">
        Save
    </BitButton>
</EditForm>
```

### Button with rounded icon

```razor
<BitButton Color="Color.Info" 
           Icon="@Icons.ItUser" 
           IconRounded="true">
    Profile
</BitButton>
```

## Generated CSS Classes

The component generates the following CSS classes based on parameters:

### Base class
- `btn`: Base button class

### Color classes (Variant.Solid)
- `btn-primary`: Primary button
- `btn-secondary`: Secondary button
- `btn-success`: Success button
- `btn-warning`: Warning button
- `btn-danger`: Danger button

### Color classes (Variant.Outline)
- `btn-outline-primary`: Primary outline button
- `btn-outline-secondary`: Secondary outline button
- `btn-outline-success`: Success outline button
- `btn-outline-warning`: Warning outline button
- `btn-outline-danger`: Danger outline button

### Size classes
- `btn-xs`: Very small size
- `btn-sm`: Small size
- `btn-lg`: Large size

### State classes
- `disabled`: Added when `Disabled` is `true`
- `btn-icon`: Added when an icon is present

## Accessibility

- The component automatically adds the `aria-disabled="true"` attribute when the button is disabled
- Supports keyboard navigation
- The `type` attribute is set correctly based on the `Type` parameter

## Notes

- The component uses `CascadingValue` to provide context to child components
- Icons are automatically positioned with appropriate margins
- Rounded icons are displayed within a container with the `rounded-icon` class
- Icon color is automatically calculated based on the button's color and variant
