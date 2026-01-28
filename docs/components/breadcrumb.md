# BitBreadcrumb

The `BitBreadcrumb` component renders a [breadcrumb navigation](https://getbootstrap.com/docs/5.3/components/breadcrumb/) to display the user's current location within a hierarchy, supporting customization of items, separators, and accessibility features.

## Namespace

```csharp
BitBlazor.Components
```

## Description

The Breadcrumb component provides a navigational aid that helps users understand and navigate the hierarchy of a website or application. It displays the current page's location within a navigational hierarchy and allows users to quickly move to previous levels.

## Parameters

| Name      | Type                                 | Required | Default | Description                                                                 |
|-----------|--------------------------------------|----------|---------|-----------------------------------------------------------------------------|
| `Label`   | `string?`                            | ✗       | `null`  | The ARIA label for the breadcrumb component, improving accessibility.       |
| `Items`   | `IReadOnlyList<BitBreadcrumbItem>`   | ✓        | empty list       | The collection of breadcrumb items to display.                              |
| `Separator` | `string?`                          | ✗       | `"/"`   | The separator string shown between breadcrumb items.                        |
| `Dark`    | `bool`                               | ✗       | `false` | Indicates if the breadcrumb is rendered on a dark background.               |

## BitBreadcrumbItem

Each breadcrumb item is represented by a `BitBreadcrumbItem` object. Typically, this includes properties such as:

| Property   | Type     | Description                                 |
|------------|----------|---------------------------------------------|
| `Text`     | `string` | The display text for the breadcrumb item. (Default empty string)   |
| `Href`     | `string` | The URL to navigate to when the item is clicked. (Default `"#"`, optional for the last item) |
| `Icon`     | `string?` | The icon to show before the item (Optional) |

> **Note:** The last item in the `Items` list is considered the current page and is not rendered as a link.

## Usage Examples

### Basic breadcrumb

C#:
```csharp
    List<BitBreadcrumbItem> items =
        [
            new BitBreadcrumbItem { Text = "Home", Link = "#" },
            new BitBreadcrumbItem { Text = "Category", Link = "#" },
            new BitBreadcrumbItem { Text = "Subcategory", Link = "#" },
            new BitBreadcrumbItem { Text = "Item" }
        ];
```

Razor:
```razor
<BitBreadcrumb Items="@items" />
```

### Breadcrumb with custom separator

C#:
```csharp
    List<BitBreadcrumbItem> items =
        [
            new BitBreadcrumbItem { Text = "Home", Link = "#" },
            new BitBreadcrumbItem { Text = "Category", Link = "#" },
            new BitBreadcrumbItem { Text = "Subcategory", Link = "#" },
            new BitBreadcrumbItem { Text = "Item" }
        ];
```

Razor:
```razor
<BitBreadcrumb Items="@items" Separator=">" />
```

### Breadcrumb with icons

C#:
```csharp
    List<BitBreadcrumbItem> items =
        [
            new BitBreadcrumbItem { Text = "Home", Link = "#", Icon= BitBlazor.Utilities.Icons.ItLink },
            new BitBreadcrumbItem { Text = "Category", Link = "#", Icon= BitBlazor.Utilities.Icons.ItLink },
            new BitBreadcrumbItem { Text = "Subcategory", Link = "#", Icon= BitBlazor.Utilities.Icons.ItLink },
            new BitBreadcrumbItem { Text = "Item", Icon= BitBlazor.Utilities.Icons.ItLink }
        ];
```

Razor:
```razor
<BitBreadcrumb Items="@items" />
```

### Breadcrumb with dark background

C#
```csharp
    List<BitBreadcrumbItem> items =
        [
            new BitBreadcrumbItem { Text = "Home", Link = "#" },
            new BitBreadcrumbItem { Text = "Category", Link = "#" },
            new BitBreadcrumbItem { Text = "Subcategory", Link = "#" },
            new BitBreadcrumbItem { Text = "Item" }
        ];
```

Razor:
```razor
<BitBreadcrumb Items="@items" Dark="true" />
```


## Accessibility

- The `Label` parameter sets the `aria-label` attribute for the breadcrumb navigation, enhancing accessibility for assistive technologies.

## Generated CSS Classes

The component generates the following CSS classes based on parameters:

- `breadcrumb`: Base class for the breadcrumb container.
- `dark`: Added when `Dark` is `true`.
- `px-3`: Added when `Dark` is `true` for horizontal padding.

## Generated HTML Structure

```html
<nav class="breadcrumb-container">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">Home</a>
                <span class="separator">/</span>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Category</a>
                <span class="separator">/</span>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Subcategory</a>
                <span class="separator">/</span>
            </li>
            <li class="breadcrumb-item active" aria-current="page">
                Item
            </li>
        </ol>
    </nav>
```


## Notes

- The `Separator` parameter allows customization of the character or string used between breadcrumb items.
- When `Dark` is set to `true`, additional CSS classes are applied for better contrast on dark backgrounds.
- The last item in the `Items` list is rendered as plain text to indicate the current page.
- If `Items` is `null` or empty, the component renders nothing.