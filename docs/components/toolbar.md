# BitToolbar

The `BitToolbar` component represents a [toolbar using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/menu-di-navigazione/toolbar/).

## Namespace

```csharp
BitBlazor.Components
```

## Description

The Toolbar component provides a navigation bar for grouping and displaying icon-based action items. It supports horizontal and vertical orientations, three size variants, optional badge counts, and a divider sub-component. Items support an active and a disabled state, and are compatible with both static (SSR) and interactive rendering modes.

Each `BitToolbarItem` can be used as a navigation link (via `Href`), a click handler (via `OnClick`), or both at the same time. When both are provided, `OnClick` takes precedence in interactive rendering while `Href` remains available for browser-native behaviors such as right-click → Open in new tab. In static SSR rendering, `Href` is the only navigation mechanism.

## Components

| Component | Description |
|-----------|-------------|
| `BitToolbar` | Root container that renders a `<nav>` with an inner `<ul>` and cascades itself to child items |
| `BitToolbarItem` | Individual action item rendered as an `<li>` with an icon and a label |
| `BitToolbarDivider` | Visual and semantic separator between items |

## BitToolbar Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `ChildContent` | `RenderFragment` | ✓ | - | One or more `BitToolbarItem` or `BitToolbarDivider` components to render inside the toolbar |
| `Size` | `ToolbarSize` | ✗ | `ToolbarSize.Default` | Controls the visual size of the toolbar |
| `Orientation` | `Orientation` | ✗ | `Orientation.Horizontal` | Determines whether the toolbar is laid out horizontally or vertically |
| `Id` | `string?` | ✗ | `null` | Sets the `id` HTML attribute on the root element |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes to apply to the root `<nav>` element |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes forwarded to the root element |

## BitToolbarItem Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `Label` | `string` | ✓ | - | The visible label text for the item |
| `IconName` | `string` | ✓ | - | The Bootstrap Italia icon name to display |
| `Href` | `string?` | ✗ | `null` | The URL the item links to. In SSR rendering the browser follows this URL on click. In interactive rendering it is a navigation fallback when `OnClick` has no delegate, and a secondary browser target (right-click, Ctrl+Click) when `OnClick` is set. |
| `Active` | `bool` | ✗ | `false` | When `true`, applies the active style to the item |
| `Disabled` | `bool` | ✗ | `false` | When `true`, disables the item and adds `aria-disabled="true"` |
| `BadgeCount` | `int?` | ✗ | `null` | A numeric badge count shown on the item; hidden when `null` or `0` |
| `BadgeLabel` | `string?` | ✗ | `null` | A text label shown next to the badge; displayed in different positions depending on `Size` |
| `OnClick` | `EventCallback` | ✗ | - | Primary interactive callback invoked when the item is clicked. Takes precedence over `Href` navigation in interactive rendering. Not invoked during static (SSR) rendering — provide `Href` as a navigation fallback for SSR contexts. |
| `Id` | `string?` | ✗ | `null` | Sets the `id` HTML attribute on the root element |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes to apply to the item |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes forwarded to the root element |

## Used Enumerations

### ToolbarSize

| Value | Description |
|-------|-------------|
| `Default` | Standard size; badge count and badge label are shown inline next to the label |
| `Medium` | Reduced size; badge label is shown in the badge wrapper area instead of next to the label |
| `Small` | Smallest size; badge count is hidden and only the badge label is shown |

### Orientation

| Value | Description |
|-------|-------------|
| `Horizontal` | Items are arranged from left to right (default) |
| `Vertical` | Items are stacked from top to bottom |

## Usage Examples

### Basic horizontal toolbar

```razor
<BitToolbar>
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" />
    <BitToolbarItem IconName="it-search" Label="Search" Href="/search" />
    <BitToolbarItem IconName="it-settings" Label="Settings" Href="/settings" />
</BitToolbar>
```

### With active and disabled items

```razor
<BitToolbar>
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" Active="true" />
    <BitToolbarItem IconName="it-search" Label="Search" Href="/search" />
    <BitToolbarItem IconName="it-lock" Label="Locked" Disabled="true" />
</BitToolbar>
```

### With divider

```razor
<BitToolbar>
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" />
    <BitToolbarItem IconName="it-search" Label="Search" Href="/search" />
    <BitToolbarDivider />
    <BitToolbarItem IconName="it-settings" Label="Settings" Href="/settings" />
</BitToolbar>
```

### With badge count and label

```razor
<BitToolbar>
    <BitToolbarItem IconName="it-mail" Label="Messages" BadgeCount="5" BadgeLabel="new" />
    <BitToolbarItem IconName="it-bell" Label="Notifications" BadgeCount="12" />
</BitToolbar>
```

### Medium size

```razor
<BitToolbar Size="ToolbarSize.Medium">
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" />
    <BitToolbarItem IconName="it-search" Label="Search" Href="/search" />
    <BitToolbarItem IconName="it-settings" Label="Settings" Href="/settings" />
</BitToolbar>
```

### Small size

```razor
<BitToolbar Size="ToolbarSize.Small">
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" />
    <BitToolbarItem IconName="it-search" Label="Search" Href="/search" />
    <BitToolbarItem IconName="it-settings" Label="Settings" Href="/settings" />
</BitToolbar>
```

### Vertical orientation

```razor
<BitToolbar Orientation="Orientation.Vertical">
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" />
    <BitToolbarItem IconName="it-search" Label="Search" Href="/search" />
    <BitToolbarItem IconName="it-settings" Label="Settings" Href="/settings" />
</BitToolbar>
```

### With click callback

```razor
<BitToolbar>
    <BitToolbarItem IconName="it-download" Label="Download" OnClick="HandleDownload" />
    <BitToolbarItem IconName="it-delete" Label="Delete" OnClick="HandleDelete" />
</BitToolbar>

@code {
    private void HandleDownload() { /* ... */ }
    private void HandleDelete() { /* ... */ }
}
```

### Navigation items (SSR-compatible)

When only `Href` is provided the item works as a plain navigation link in both SSR and interactive rendering.

```razor
<BitToolbar>
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" />
    <BitToolbarItem IconName="it-search" Label="Search" Href="/search" />
    <BitToolbarDivider />
    <BitToolbarItem IconName="it-settings" Label="Settings" Href="/settings" />
</BitToolbar>
```

### Mixed mode (OnClick takes precedence)

When both `Href` and `OnClick` are provided, `OnClick` handles the primary click in interactive rendering. `Href` is still rendered on the `<a>` element so that right-click → Open in new tab continues to work.

```razor
<BitToolbar>
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" OnClick="HandleHome" />
    <BitToolbarItem IconName="it-search" Label="Search" Href="/search" OnClick="HandleSearch" />
</BitToolbar>

@code {
    private void HandleHome() { /* custom SPA navigation or analytics */ }
    private void HandleSearch() { /* open search panel */ }
}
```

### With additional attributes

```razor
<BitToolbar data-testid="main-toolbar" aria-label="Main actions">
    <BitToolbarItem IconName="it-home" Label="Home" Href="/" />
</BitToolbar>
```
