# BitBottomNav

The `BitBottomNav` component renders a [mobile-optimized bottom navigation bar](https://italia.github.io/bootstrap-italia/docs/menu-di-navigazione/bottomnav/) to show the current location or quick actions. It supports icons, badges, alert indicators and accessibility-friendly labels for each item.

## Namespace

```csharp
BitBlazor.Components
```

## Description

BitBottomNav displays a horizontal list of navigation entries optimized for bottom placement (common on mobile apps). Each entry is a BitBottomNavItem that can render:
- a text,
- an icon (via BitIcon),
- an optional badge (text/number),
- an optional alert indicator (dot) when no badge is present,
- an active state applied via the IsActive flag.

Items are rendered as anchor elements (`<a>`) so they are keyboard-focusable and navigable. Visual styling is provided by the consuming app's stylesheet; the component emits semantic classes the CSS targets.

## Parameters

| Name      | Type | Required | Default | Description |
|-----------|--------------------------------------|----------|---------|-----------------------------------------------------------------------------|
| Items	    | IReadOnlyList<BitBottomNavItem> | ✗ | `Enumerable.Empty<BitBottomNavItem>().ToList()` |	 The collection of items to render in the bottom navigation. |

## BitBottomNavItem

Each item in Items is a `BitBottomNavItem`. Main properties:

| Property   | Type     | Default             | Description                                             |
|------------|----------|---------------------|---------------------------------------------------------|
| `Text`	 | `string`   | `string.Empty`        | Visible label for the item.                         |
| `Icon`     | `string?`  | `null`                |	Optional icon name (rendered via `BitIcon`).        |
| `Link`     | `string?`  | `null`                | Optional URL for the item (<a href="...">).         |
| `BadgeText` | `string?` | `null`                | Optional badge text; when present renders a badge element. |
| `BadgeAriaLabel` | `string?` | `null`           | Optional ARIA label for the badge/alert; rendered in a visually-hidden span. |
| `IsActive` | `bool`     | `false`               | If `true`, the item gets an active class (indicates current/selected item). |
| `IsAlertActive` | `bool` | `false`              | When `true` and no `BadgeText` is set, a small alert indicator (dot) is rendered. |

Helper methods on `BitBottomNavItem`:
- `HasBadge()` — `true` when `BadgeText` is non-empty.
- `HasAriaLabel()` — `true` when (badge or alert) and `BadgeAriaLabel` is non-empty.
- `HasIcon()` — `true` when `Icon` is non-empty.

## Usage Examples

### Default

C# (prepare items):

```csharp
var items = new List<BitBottomNavItem>
    {
        new BitBottomNavItem { Text = "Messages", Link = "#", Icon = BitBlazor.Utilities.Icons.ItComment, BadgeText = "1", BadgeAriaLabel = "to read" },
        new BitBottomNavItem { Text = "Images", Link = "#", Icon = BitBlazor.Utilities.Icons.ItCamera, BadgeText = "2", BadgeAriaLabel = "to view" },
        new BitBottomNavItem { Text = "Documents", Link = "#", Icon = BitBlazor.Utilities.Icons.ItFile, BadgeText = "42", BadgeAriaLabel = "to examine" },
        new BitBottomNavItem { Text = "Favorites", Link = "#", Icon = BitBlazor.Utilities.Icons.ItStarOutline, IsActive = true },
        new BitBottomNavItem { Text = "Settings", Link = "#", Icon = BitBlazor.Utilities.Icons.ItSettings, IsAlertActive = true }
    };
```

Razor (render component):

```razor
<BitBottomNav Items="@items" />
```

## Common scenarios:
- Numeric/text badge: set `BadgeText` = "7" and optionally `BadgeAriaLabel` = "7 unread messages".
- Alert indicator (dot): set `IsAlertActive` = true and leave `BadgeText` empty.
- Icon-only visual with accessible status: supply Icon, Text, and a `BadgeAriaLabel` if there is a badge or alert.

## Accessibility
- When a badge or alert is present and BadgeAriaLabel is provided, the label is rendered inside a visually-hidden span next to the visible label so assistive technologies receive the context (for example, "3 unread notifications").
- Items render as anchors (<a>), providing keyboard focus and native navigation behavior. Supply meaningful Link values.
- Provide concise and descriptive `Text` and `BadgeAriaLabel` strings to maximize clarity for screen reader users.

## Generated CSS Classes

- `bottom-nav` — root container on the <nav> element.
- `active` — applied to an item's <a> when IsActive is true.
- `badge-wrapper` — wrapper for badge or alert element.
- `bottom-nav-badge` — class for the badge text element.
- `bottom-nav-alert` — class for the small alert indicator (dot).
- `bottom-nav-label` — wrapper for the visible item text.
- `visually-hidden` — used for accessible text that should be hidden visually.

## Generated HTML Structure (approximate)

```html
<nav class="bottom-nav">
    <ul>
        <li><a href="#" class=""><div class="badge-wrapper"><span class="bottom-nav-badge">1</span></div><svg class="icon"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-comment"></use></svg><span class="bottom-nav-label">Messages <span class="visually-hidden">to read</span></span></a></li>
        <li><a href="#" class=""><div class="badge-wrapper"><span class="bottom-nav-badge">2</span></div><svg class="icon"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-camera"></use></svg><span class="bottom-nav-label">Images <span class="visually-hidden">to view</span></span></a></li>
        <li><a href="#" class=""><div class="badge-wrapper"><span class="bottom-nav-badge">42</span></div><svg class="icon"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-file"></use></svg><span class="bottom-nav-label">Documents <span class="visually-hidden">to examine</span></span></a></li>
        <li><a href="#" class=""><svg class="icon"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-star-outline"></use></svg><span class="bottom-nav-label">Favorites</span></a></li>
        <li><a href="#" class=""><svg class="icon"><use href="/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#it-settings"></use></svg><span class="bottom-nav-label">Settings</span></a></li>
    </ul>
</nav>
```

## Notes
- If Items is empty the component renders an empty list by default.
- The component does not enforce positional semantics for first/last items; the ordering and the active item are controlled by the provided Items and each item's `IsActive` flag.
- Visual appearance depends on the app's stylesheet. Ensure your project includes the BitBlazor CSS or defines styles for the classes listed above.