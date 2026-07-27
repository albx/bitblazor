# BitDropdown

The `BitDropdown` component provides a [dropdown menu using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/dropdown/).

## Namespace

```csharp
BitBlazor.Components
```

## Description

`BitDropdown` renders a collapsible menu anchored to an activator button. Opening, closing, and full keyboard navigation are managed entirely in Blazor — no JavaScript interop is required. The menu can be positioned in four directions, styled with a dark color theme, and expanded to full container width. Individual `BitDropdownItem` components support navigation links, click callbacks, active highlighting, and disabled states.

A custom `ActivatorTemplate` can replace the built-in toggle button with any element while retaining the complete keyboard interaction model.

## Components

| Component | Description |
|-----------|-------------|
| `BitDropdown` | Root container. Renders the activator and the collapsible menu; orchestrates open/close state and keyboard focus |
| `BitDropdownItem` | A single menu entry rendered as a `<li><a>` pair inside the menu |

## BitDropdown Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `ChildContent` | `RenderFragment` | ✓ | - | One or more `BitDropdownItem` components rendered inside the dropdown menu |
| `ActivatorId` | `string` | ✓ | - | Unique `id` for the activator element. Used to link the button to the menu via `aria-labelledby` |
| `ActivatorLabel` | `string` | ✗ | `""` | Text label shown on the default activator button |
| `ActivatorTemplate` | `RenderFragment<ActivatorContext>?` | ✗ | `null` | Custom render fragment that replaces the default activator button. Receives an `ActivatorContext` context object |
| `Position` | `DropdownPosition` | ✗ | `DropdownPosition.Down` | Direction in which the menu opens relative to the activator |
| `MenuColor` | `DropdownMenuColor` | ✗ | `DropdownMenuColor.Default` | Color theme applied to the dropdown menu |
| `MenuWidth` | `DropdownMenuWidth` | ✗ | `DropdownMenuWidth.Default` | Width behaviour of the dropdown menu |
| `ItemSize` | `DropdownItemSize` | ✗ | `DropdownItemSize.Default` | Visual size applied to all child `BitDropdownItem` components |
| `MenuHeaderTemplate` | `RenderFragment?` | ✗ | `null` | Optional content rendered as a heading above the item list |
| `Id` | `string?` | ✗ | `null` | Sets the `id` HTML attribute on the root container element |
| `CssClass` | `string?` | ✗ | `null` | Additional CSS classes applied to the root container element |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes forwarded to the root container element |

## BitDropdownItem Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `ChildContent` | `RenderFragment` | ✓ | - | The content rendered inside the item link |
| `Href` | `string?` | ✗ | `null` | URL the item navigates to when clicked. In SSR rendering, the browser follows this URL. In interactive rendering, used as a navigation fallback when `OnClick` has no delegate |
| `OnClick` | `EventCallback` | ✗ | - | Callback invoked when the item is clicked. Takes precedence over `Href` navigation in interactive rendering |
| `Active` | `bool` | ✗ | `false` | When `true`, applies the active style to the item |
| `Disabled` | `bool` | ✗ | `false` | When `true`, disables the item and adds `aria-disabled="true"` |
| `AdditionalAttributes` | `IDictionary<string, object>?` | ✗ | - | Additional HTML attributes forwarded to the item anchor element |

## ActivatorContext

When you provide a custom `ActivatorTemplate`, Blazor passes an `ActivatorContext` object as the template context. Use its members to wire the correct behaviour and ARIA attributes onto your activator element.

| Member | Type | Description |
|--------|------|-------------|
| `ActivatorId` | `string` | The `id` value from `BitDropdown.ActivatorId`. Bind to your element's `id` attribute |
| `ActivatorLabel` | `string` | The label text from `BitDropdown.ActivatorLabel` |
| `ActivatorRef` | `ElementReference` | Optional element reference. Bind via `@ref` to enable focus restoration when the menu closes via Escape |
| `Attributes` | `IDictionary<string, object>` | Pre-populated with `aria-haspopup="true"` and a live `aria-expanded` value. Spread onto your activator via `@attributes` |
| `ToggleDropdown()` | `void` | Call on `@onclick` to open or close the menu |
| `HandleKeyDownAsync(KeyboardEventArgs)` | `Task` | Call on `@onkeydown` to enable full keyboard navigation (Enter, Space, ArrowDown, ArrowUp) |

## Used Enumerations

### DropdownPosition

| Value | Description |
|-------|-------------|
| `Down` | Menu opens below the activator (default) |
| `Up` | Menu opens above the activator |
| `End` | Menu opens to the right of the activator |
| `Start` | Menu opens to the left of the activator |

### DropdownMenuColor

| Value | Description |
|-------|-------------|
| `Default` | Standard light menu background |
| `Dark` | Dark-themed menu background |

### DropdownMenuWidth

| Value | Description |
|-------|-------------|
| `Default` | Width determined by the longest item |
| `Full` | Menu expands to the full width of the parent container |

### DropdownItemSize

| Value | Description |
|-------|-------------|
| `Default` | Standard item height |
| `Large` | Increased item height |

## Accessibility

`BitDropdown` implements the WAI-ARIA [Disclosure Navigation Menu pattern](https://www.w3.org/WAI/ARIA/apg/patterns/disclosure/examples/disclosure-navigation/) and follows WCAG 2.2 AA.

### Automatic ARIA wiring

The default activator button always carries:

| Attribute | Value | Effect |
|-----------|-------|--------|
| `aria-haspopup` | `"true"` | Announces the presence of a popup to screen readers |
| `aria-expanded` | `"false"` / `"true"` | Updated automatically on every open/close cycle |
| `id` | value of `ActivatorId` | Linked to the menu via `aria-labelledby` |

Disabled `BitDropdownItem` components receive `aria-disabled="true"` automatically and are skipped by all keyboard-focus algorithms.

### Keyboard interaction

| Key | Behaviour |
|-----|-----------|
| `Enter` / `Space` | Toggle the menu open or closed; focus stays on the activator |
| `ArrowDown` | Open the menu and move focus to the **first** enabled item |
| `ArrowUp` | Open the menu and move focus to the **last** enabled item |
| `ArrowDown` (when open, focus inside menu) | Move focus to the **next** enabled item (wraps to first) |
| `ArrowUp` (when open, focus inside menu) | Move focus to the **previous** enabled item (wraps to last) |
| `Enter` / `Space` (focus on item) | Activate the item (invoke `OnClick` or navigate to `Href`) |
| `Escape` (focus inside menu) | Close the menu and return focus to the activator |

### Custom activator template

When replacing the default button, you must wire all five directives to preserve full keyboard accessibility and correct ARIA state:

```razor
<BitDropdown ActivatorId="custom-activator" ActivatorLabel="Actions">
    <ActivatorTemplate Context="ctx">
        <button id="@ctx.ActivatorId"
                @ref="ctx.ActivatorRef"
                @onclick="ctx.ToggleDropdown"
                @onkeydown="ctx.HandleKeyDownAsync"
                @attributes="ctx.Attributes">
            @ctx.ActivatorLabel
        </button>
    </ActivatorTemplate>
    <BitDropdownItem Href="/profile">Profile</BitDropdownItem>
    <BitDropdownItem Href="/logout">Log out</BitDropdownItem>
</BitDropdown>
```

| Directive | Purpose |
|-----------|---------|
| `id="@ctx.ActivatorId"` | Links the button to the menu via `aria-labelledby` |
| `@ref="ctx.ActivatorRef"` | Optional. Enables focus restoration to the activator when the menu closes via Escape |
| `@onclick="ctx.ToggleDropdown"` | Handles mouse and touch toggle |
| `@onkeydown="ctx.HandleKeyDownAsync"` | Handles keyboard toggle and arrow-key navigation |
| `@onkeydown:preventDefault="true"` | Prevents the browser from scrolling on arrow keys |
| `@attributes="ctx.Attributes"` | Spreads `aria-haspopup` and live `aria-expanded` |

> **Note:** Omitting `@ref="ctx.ActivatorRef"` is safe — `CloseAsync` degrades gracefully and simply will not restore focus to the activator on Escape.

## Usage Examples

### Basic dropdown

```razor
<BitDropdown ActivatorId="basic-dd" ActivatorLabel="Actions">
    <BitDropdownItem Href="/new">New item</BitDropdownItem>
    <BitDropdownItem Href="/edit">Edit</BitDropdownItem>
    <BitDropdownItem Href="/delete">Delete</BitDropdownItem>
</BitDropdown>
```

### With click callbacks

```razor
<BitDropdown ActivatorId="actions-dd" ActivatorLabel="Actions">
    <BitDropdownItem OnClick="HandleNew">New</BitDropdownItem>
    <BitDropdownItem OnClick="HandleEdit">Edit</BitDropdownItem>
    <BitDropdownItem OnClick="HandleDelete">Delete</BitDropdownItem>
</BitDropdown>

@code {
    private void HandleNew() { /* ... */ }
    private void HandleEdit() { /* ... */ }
    private void HandleDelete() { /* ... */ }
}
```

### With active and disabled items

```razor
<BitDropdown ActivatorId="state-dd" ActivatorLabel="Options">
    <BitDropdownItem Href="/dashboard" Active="true">Dashboard</BitDropdownItem>
    <BitDropdownItem Href="/reports">Reports</BitDropdownItem>
    <BitDropdownItem Href="/admin" Disabled="true">Admin (locked)</BitDropdownItem>
</BitDropdown>
```

### With menu header

```razor
<BitDropdown ActivatorId="header-dd" ActivatorLabel="Account">
    <MenuHeaderTemplate>
        My Account
    </MenuHeaderTemplate>
    <BitDropdownItem Href="/profile">Profile</BitDropdownItem>
    <BitDropdownItem Href="/settings">Settings</BitDropdownItem>
    <BitDropdownItem OnClick="HandleLogout">Log out</BitDropdownItem>
</BitDropdown>

@code {
    private void HandleLogout() { /* ... */ }
}
```

### Position variants

```razor
@* Menu opens upward *@
<BitDropdown ActivatorId="up-dd" ActivatorLabel="Up" Position="DropdownPosition.Up">
    <BitDropdownItem Href="/option-1">Option 1</BitDropdownItem>
    <BitDropdownItem Href="/option-2">Option 2</BitDropdownItem>
</BitDropdown>

@* Menu opens to the right *@
<BitDropdown ActivatorId="end-dd" ActivatorLabel="End" Position="DropdownPosition.End">
    <BitDropdownItem Href="/option-1">Option 1</BitDropdownItem>
    <BitDropdownItem Href="/option-2">Option 2</BitDropdownItem>
</BitDropdown>

@* Menu opens to the left *@
<BitDropdown ActivatorId="start-dd" ActivatorLabel="Start" Position="DropdownPosition.Start">
    <BitDropdownItem Href="/option-1">Option 1</BitDropdownItem>
    <BitDropdownItem Href="/option-2">Option 2</BitDropdownItem>
</BitDropdown>
```

### Dark menu theme

```razor
<BitDropdown ActivatorId="dark-dd" ActivatorLabel="Actions" MenuColor="DropdownMenuColor.Dark">
    <BitDropdownItem Href="/new">New</BitDropdownItem>
    <BitDropdownItem Href="/edit">Edit</BitDropdownItem>
    <BitDropdownItem Href="/delete">Delete</BitDropdownItem>
</BitDropdown>
```

### Full-width menu

```razor
<BitDropdown ActivatorId="full-dd" ActivatorLabel="Select action" MenuWidth="DropdownMenuWidth.Full">
    <BitDropdownItem Href="/option-a">Option A — longer label</BitDropdownItem>
    <BitDropdownItem Href="/option-b">Option B</BitDropdownItem>
</BitDropdown>
```

### Large item size

```razor
<BitDropdown ActivatorId="large-dd" ActivatorLabel="Navigate" ItemSize="DropdownItemSize.Large">
    <BitDropdownItem Href="/home">Home</BitDropdownItem>
    <BitDropdownItem Href="/about">About</BitDropdownItem>
    <BitDropdownItem Href="/contact">Contact</BitDropdownItem>
</BitDropdown>
```

### Custom activator template

Replace the default button with any element while preserving full accessibility and keyboard behaviour:

```razor
<BitDropdown ActivatorId="custom-dd" ActivatorLabel="User menu">
    <ActivatorTemplate Context="ctx">
        <button id="@ctx.ActivatorId"
                class="btn btn-outline-primary"
                @ref="ctx.ActivatorRef"
                @onclick="ctx.ToggleDropdown"
                @onkeydown="ctx.HandleKeyDownAsync"
                @attributes="ctx.Attributes">
            <BitIcon IconName="@Icons.ItUser" />
            @ctx.ActivatorLabel
        </button>
    </ActivatorTemplate>
    <BitDropdownItem Href="/profile">Profile</BitDropdownItem>
    <BitDropdownItem Href="/settings">Settings</BitDropdownItem>
    <BitDropdownItem OnClick="HandleLogout">Log out</BitDropdownItem>
</BitDropdown>

@code {
    private void HandleLogout() { /* ... */ }
}
```

### Custom activator with additional attributes

```razor
<BitDropdown ActivatorId="attr-dd" ActivatorLabel="File" CssClass="d-inline-block">
    <ActivatorTemplate Context="ctx">
        <button id="@ctx.ActivatorId"
                class="btn btn-dropdown dropdown-toggle"
                data-testid="file-menu-trigger"
                @ref="ctx.ActivatorRef"
                @onclick="ctx.ToggleDropdown"
                @onkeydown="ctx.HandleKeyDownAsync"
                @onkeydown:preventDefault="true"
                @attributes="ctx.Attributes">
            @ctx.ActivatorLabel
        </button>
    </ActivatorTemplate>
    <BitDropdownItem OnClick="HandleNew">New file</BitDropdownItem>
    <BitDropdownItem OnClick="HandleOpen">Open file</BitDropdownItem>
    <BitDropdownItem OnClick="HandleSave">Save</BitDropdownItem>
</BitDropdown>

@code {
    private void HandleNew() { /* ... */ }
    private void HandleOpen() { /* ... */ }
    private void HandleSave() { /* ... */ }
}
```

### With additional HTML attributes on the container

```razor
<BitDropdown ActivatorId="attrs-dd"
             ActivatorLabel="More"
             data-testid="more-menu">
    <BitDropdownItem Href="/option-1">Option 1</BitDropdownItem>
    <BitDropdownItem Href="/option-2">Option 2</BitDropdownItem>
</BitDropdown>
```
