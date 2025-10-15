# Enumerations and Types - BitBlazor

This section documents all enumerations and types used in BitBlazor components.

## General Enumerations

### Color

**Namespace**: `BitBlazor`

Defines color options for BitBlazor components.

| Value | Description | Usage |
|-------|-------------|-------|
| `Default` | Deafult color | Default component color, basic semantic content |
| `Primary` | Primary color | Main actions, important elements |
| `Secondary` | Secondary color | Secondary actions, support elements |
| `Success` | Success color | Confirmations, successful operations |
| `Danger` | Danger color | Errors, destructive actions |
| `Warning` | Warning color | Warnings, caution |

**Components using Color**:
- `BitAlert`
- `BitBadge`
- `BitButton`
- `BitCard` (for BorderTopColor)

### Variant

**Namespace**: `BitBlazor`

Defines style variants for BitBlazor components.

| Value | Description | Appearance |
|-------|-------------|------------|
| `Solid` | Solid variant | Full color fill |
| `Outline` | Outline variant | Colored border only, transparent background |

**Components using Variant**:
- `BitBadge`
- `BitButton`
- `ButtonBadge` (automatic)

### Size

**Namespace**: `BitBlazor`

Defines size options for BitBlazor components.

| Value | Description | Typical CSS Class |
|-------|-------------|-------------------|
| `Default` | Default size | -, `*-md` |
| `ExtraExtraLarge` | Extra extra large size | `*-xxl` |
| `ExtraLarge` | Extra large size | `*-xl` |
| `Large` | Large size | `*-lg` |
| `Small` | Small size | `*-sm` |
| `Mini` | Very small size | `*-xs` |

**Components using Size**:
- `BitButton`
- `BitTextField`
- `BitPasswordField`
- `BitTextAreaField`

### Ratio

**Namespace**: `BitBlazor`

Represents common aspect ratios used in media and display settings.

| Value | Description | Aspect Ratio |
|-------|-------------|--------------|
| `Ratio1x1` | Square ratio | 1:1 |
| `Ratio4x3` | Traditional ratio | 4:3 |
| `Ratio16x9` | Widescreen ratio | 16:9 |
| `Ratio21x9` | Ultra-wide ratio | 21:9 |

**Components using Ratio**:
- `CardImage`

### Typography

**Namespace**: `BitBlazor`

Represents typography levels used in structured text.

| Value | HTML Element | Usage |
|-------|--------------|-------|
| `H1` | `<h1>` | Main page title |
| `H2` | `<h2>` | Main section titles |
| `H3` | `<h3>` | Subsection titles (default for CardTitle) |
| `H4` | `<h4>` | Paragraph titles |
| `H5` | `<h5>` | Sub-titles |
| `H6` | `<h6>` | Detail titles |

**Components using Typography**:
- `CardTitle`
- `CardProfileHeader`

## Alert-specific Enumerations

### AlertType

**Namespace**: `BitBlazor.Components`

Specifies the type of an alert component.

| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Primary` | Primary alert | `alert-primary` |
| `Info` | Informational alert | `alert-info` |
| `Success` | Success alert | `alert-success` |
| `Warning` | Warning alert | `alert-warning` |
| `Danger` | Danger alert | `alert-danger` |

## Button-specific Enumerations

### ButtonType

**Namespace**: `BitBlazor.Components`

Specifies the type of a button component.

| Value | Description | HTML type attribute |
|-------|-------------|---------------------|
| `Button` | Generic button | `button` |
| `Submit` | Form submit button | `submit` |
| `Reset` | Form reset button | `reset` |

### IconPosition

**Namespace**: `BitBlazor.Components`

Defines the position of an icon in a button.

| Value | Description |
|-------|-------------|
| `Start` | Icon at the beginning (left) |
| `End` | Icon at the end (right) |

## Card-specific Enumerations

### CardType

**Namespace**: `BitBlazor.Components`

Represents the different types of cards available.

| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Default` | Standard card | - |
| `Profile` | Profile card | `it-card-profile` |
| `Banner` | Card with banner layout | `it-card-banner` |

### CardShadow

**Namespace**: `BitBlazor.Components`

Specifies the shadow size for a card element.

| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Small` | Small shadow | `shadow-sm` |
| `Medium` | Medium shadow | `shadow` |
| `Large` | Large shadow | `shadow-lg` |

## Form-specific Enumerations

### TextFieldType

**Namespace**: `BitBlazor.Form`

Specifies the type of input field for a text-based form element.

| Value | Description | HTML Input Type |
|-------|-------------|-----------------|
| `Text` | Plain text input | `text` |
| `Email` | Email address input | `email` |
| `Tel` | Telephone number input | `tel` |
| `Url` | URL input | `url` |

**Components using TextFieldType**:
- `BitTextField`

## Icon-specific Enumerations

### IconSize

**Namespace**: `BitBlazor.Utilities`

Defines size options for icons.

| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Default` | Default size | - |
| `ExtraSmall` | Very small size | `icon-xs` |
| `Small` | Small size | `icon-sm` |
| `Large` | Large size | `icon-lg` |
| `ExtraLarge` | Very large size | `icon-xl` |

### IconColor

**Namespace**: `BitBlazor.Utilities`

Defines color options for icons.

| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Default` | Default color | - |
| `Primary` | Primary color | `icon-primary` |
| `Secondary` | Secondary color | `icon-secondary` |
| `Success` | Success color | `icon-success` |
| `Danger` | Danger color | `icon-danger` |
| `Warning` | Warning color | `icon-warning` |
| `Light` | Light color | `icon-light` |
| `White` | White color | `icon-white` |

### IconAlignment

**Namespace**: `BitBlazor.Utilities`

Defines alignment options for icons.

| Value | Description | CSS Class |
|-------|-------------|-----------|
| `Default` | Default alignment | - |
| `Bottom` | Bottom alignment | `align-bottom` |
| `Middle` | Center alignment | `align-middle` |
| `Top` | Top alignment | `align-top` |

## Enumeration Mappings

### Color vs IconColor

When using icons in colored components, mapping between colors is often necessary:

```csharp
var iconColor = buttonColor switch
{
    Color.Primary => IconColor.Primary,
    Color.Secondary => IconColor.Secondary,
    Color.Success => IconColor.Success,
    Color.Warning => IconColor.Warning,
    Color.Danger => IconColor.Danger,
    _ => IconColor.Default
};
```

### Size vs IconSize

Component and icon sizes don't always correspond directly:

```csharp
var iconSize = buttonSize switch
{
    Size.Mini => IconSize.ExtraSmall,
    Size.Small => IconSize.Small,
    Size.Default => IconSize.Default,
    Size.Large => IconSize.Large,
    _ => IconSize.Default
};
```

## Combined Usage Examples

### Button with colored badge

```razor
<BitButton Color="Color.Primary" 
           Variant="Variant.Solid"
           Size="Size.Large">
    Notifications
    <ButtonBadge Text="5" />
</BitButton>
```

### Card with icon in title

```razor
<BitCard Type="CardType.Profile" 
         Shadow="CardShadow.Medium"
         BorderTopColor="Color.Primary">
    <CardBody>
        <CardTitle Typography="Typography.H2" HasIcon="true">
            <a href="#">
                User Profile
                <BitIcon IconName="@Icons.ItUser" 
                         Color="IconColor.Primary" />
            </a>
        </CardTitle>
    </CardBody>
</BitCard>
```

## Notes on Default Values

- Most enumerations have a `Default` value representing the standard style
- Default values are chosen to ensure the best accessibility and usability
- When no value is specified, the enumeration's default is used
- Some parameters have component-specific default values that may differ from the enumeration's `Default` value
