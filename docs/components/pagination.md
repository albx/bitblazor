# BitPagination

The `BitPagination` component provides a [pagination control using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/paginazione/).

## Namespace

```csharp
BitBlazor.Components
```

## Description

The Pagination component enables users to navigate through large data sets split across multiple pages. It renders previous/next buttons, individual page links with optional ellipsis truncation, and optional extras such as a jump-to-page input and a total-items summary. Two view modes are available: the default full control and a compact simple mode.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `NumberOfPages` | `int` | ✓ | `1` | Total number of pages in the data set. |
| `Description` | `string` | ✓ | `""` | Text placed in the `aria-label` of the wrapping `<nav>` element. |
| `Page` | `int` | ✗ | `1` | The currently selected page. Use `@bind-Page` for two-way binding. |
| `PageChanged` | `EventCallback<int>` | ✗ | - | Callback invoked when the active page changes. Receives the new page number. |
| `PreviousPageLabel` | `string` | ✗ | `"previous page"` | Visually-hidden label for the previous-page button (screen readers). |
| `PreviousPageTemplate` | `RenderFragment?` | ✗ | `null` | Custom content for the previous-page button. Replaces the default chevron icon. |
| `NextPageLabel` | `string` | ✗ | `"next page"` | Visually-hidden label for the next-page button (screen readers). |
| `NextPageTemplate` | `RenderFragment?` | ✗ | `null` | Custom content for the next-page button. Replaces the default chevron icon. |
| `Alignment` | `PaginationAlignment` | ✗ | `Left` | Horizontal alignment of the pagination controls within their container. |
| `Disabled` | `bool` | ✗ | `false` | Disables all interaction with the pagination controls. |
| `PageRangeSize` | `int?` | ✗ | `null` | Number of page buttons to show on each side of the current page before inserting an ellipsis. When `null`, all pages are always shown. |
| `TotalItemsTemplate` | `RenderFragment?` | ✗ | `null` | Custom content rendered below the page list to display a total-items summary. |
| `ShowJumpToPage` | `bool` | ✗ | `false` | When `true`, renders a text input that lets users jump directly to a specific page. |
| `JumpToPageLabelTemplate` | `RenderFragment?` | ✗ | `null` | Custom label for the jump-to-page input. Defaults to `"go to..."` with an accessible description. |
| `ViewMode` | `PaginationViewMode` | ✗ | `Default` | Display mode for the pagination controls (see `PaginationViewMode`). |
| `SimpleModeVisuallyHiddenTemplate` | `RenderFragment<PaginationState>?` | ✗ | `null` | Accessible content for Simple mode, provided to screen readers. Receives `PaginationState` as context. Defaults to `"page N of M"`. |

## Enumerations

### PaginationViewMode

| Value | Description |
|-------|-------------|
| `Default` | Full pagination control: previous button, all page buttons (with optional ellipsis), next button. |
| `Simple` | Compact control showing only the current page and total pages (e.g. `3 / 10`), plus a visually-hidden screen-reader link. |

### PaginationAlignment

| Value | CSS class added | Description |
|-------|-----------------|-------------|
| `Left` | *(none)* | Left-aligned (default). |
| `Center` | `justify-content-center` | Centered horizontally. |
| `Right` | `justify-content-end` | Right-aligned. |

### PaginationState

`PaginationState` is a `record struct` passed as context to `SimpleModeVisuallyHiddenTemplate`.

| Member | Type | Description |
|--------|------|-------------|
| `CurrentPage` | `int` | The currently active page number. |
| `NumberOfPages` | `int` | Total number of pages. |
| `IsFirstPage` | `bool` | `true` when the current page is `1`. |
| `IsLastPage` | `bool` | `true` when the current page equals `NumberOfPages`. |

## Usage Examples

### Basic pagination

```razor
<BitPagination NumberOfPages="10"
               Description="Navigate pages"
               @bind-Page="currentPage"
               @bind-Page:after="HandlePageChanged" />

@code {
    private int currentPage = 1;

    private async Task HandlePageChanged(int page)
    {
        currentPage = page;
        await LoadDataAsync(page);
    }
}
```

### Centered alignment

```razor
<BitPagination NumberOfPages="5"
               Description="Navigate pages"
               @bind-Page="currentPage"
               Alignment="PaginationAlignment.Center" />
```

### Simple mode

```razor
<BitPagination NumberOfPages="20"
               Description="Navigate pages"
               @bind-Page="currentPage"
               ViewMode="PaginationViewMode.Simple" />
```

### With ellipsis (PageRangeSize)

When `PageRangeSize` is set, only the first page, the last page, the current page, and the specified number of neighbours are shown. Other page numbers are replaced with `...`.

```razor
<BitPagination NumberOfPages="50"
               Description="Navigate pages"
               @bind-Page="currentPage"
               PageRangeSize="2" />
```

### With jump-to-page input

```razor
<BitPagination NumberOfPages="20"
               Description="Navigate pages"
               @bind-Page="currentPage"
               ShowJumpToPage="true" />
```

### With custom jump-to-page label

```razor
<BitPagination NumberOfPages="20"
               Description="Navigate pages"
               @bind-Page="currentPage"
               ShowJumpToPage="true">
    <JumpToPageLabelTemplate>
        <span aria-hidden="true">Go to page</span>
        <span class="visually-hidden">Enter page number to navigate to</span>
    </JumpToPageLabelTemplate>
</BitPagination>
```

### With total-items summary

```razor
<BitPagination NumberOfPages="10"
               Description="Navigate pages"
               @bind-Page="currentPage">
    <TotalItemsTemplate>
        Results @((currentPage - 1) * pageSize + 1)–@(currentPage * pageSize) of @totalItems
    </TotalItemsTemplate>
</BitPagination>

@code {
    private int currentPage = 1;
    private int pageSize = 10;
    private int totalItems = 100;
}
```

### Disabled state

```razor
<BitPagination NumberOfPages="10"
               Description="Navigate pages"
               Page="3"
               Disabled="true" />
```

### Custom previous/next page buttons

```razor
<BitPagination NumberOfPages="10"
               Description="Navigate pages"
               @bind-Page="currentPage">
    <PreviousPageTemplate>
        <span>&#8592; Prev</span>
    </PreviousPageTemplate>
    <NextPageTemplate>
        <span>Next &#8594;</span>
    </NextPageTemplate>
</BitPagination>
```

### Custom Simple mode screen-reader content

```razor
<BitPagination NumberOfPages="20"
               Description="Navigate pages"
               @bind-Page="currentPage"
               ViewMode="PaginationViewMode.Simple">
    <SimpleModeVisuallyHiddenTemplate Context="state">
        Currently viewing page @state.CurrentPage of @state.NumberOfPages
    </SimpleModeVisuallyHiddenTemplate>
</BitPagination>
```

## Accessibility

- The wrapping `<nav>` element always carries the `aria-label` set via the required `Description` parameter.
- The active page item receives `aria-current="page"` automatically.
- Disabled items receive `aria-hidden="true"` and `tabindex="-1"` so they are skipped by keyboard and screen-reader navigation.
- Previous and next page buttons include a `<span class="visually-hidden">` text sourced from `PreviousPageLabel` and `NextPageLabel` respectively.
- In Simple mode, a visually hidden element provides the full page context (e.g. `"page 3 of 20"`) for screen readers. Customise it with `SimpleModeVisuallyHiddenTemplate`.
- When `ShowJumpToPage` is `true`, the input and its label are properly associated via `id`/`for` attributes generated at runtime.

## Generated CSS Classes

| Element | CSS class | Condition |
|---------|-----------|-----------|
| `<nav>` | `pagination-wrapper` | Always |
| `<nav>` | `justify-content-center` | `Alignment == Center` |
| `<nav>` | `justify-content-end` | `Alignment == Right` |
| `<nav>` | `pagination-total` | `TotalItemsTemplate` is not `null` |
| `<ul>` | `pagination` | Always |
| `<li>` | `page-item` | Always |
| `<li>` | `disabled` | `Disabled == true` |

## Generated HTML Structure

```html
<nav class="pagination-wrapper" aria-label="Navigate pages">
    <ul class="pagination">
        <!-- Previous page button -->
        <li class="page-item">
            <a class="page-link" href="#" onclick="...">
                <!-- chevron-left icon -->
                <span class="visually-hidden">previous page</span>
            </a>
        </li>

        <!-- Page items (Default mode) -->
        <li class="page-item">
            <a class="page-link" href="#" aria-current="page">1</a>
        </li>
        <li class="page-item">
            <a class="page-link" href="#">2</a>
        </li>
        <!-- Ellipsis (when PageRangeSize is set and pages are truncated) -->
        <li class="page-item">
            <span class="page-link">...</span>
        </li>
        <li class="page-item">
            <a class="page-link" href="#">10</a>
        </li>

        <!-- Next page button -->
        <li class="page-item">
            <a class="page-link" href="#" onclick="...">
                <span class="visually-hidden">next page</span>
                <!-- chevron-right icon -->
            </a>
        </li>
    </ul>

    <!-- Jump-to-page input (when ShowJumpToPage is true) -->
    <div class="form-group">
        <input type="text" class="form-control" inputmode="numeric" pattern="[0-9]*" />
        <label for="jumpToPage-...">
            <span aria-hidden="true">go to...</span>
            <span class="visually-hidden">Set the destination page</span>
        </label>
    </div>

    <!-- Total items summary (when TotalItemsTemplate is provided) -->
    <p>Results 1–10 of 100</p>
</nav>
```

## Notes

- `NumberOfPages` and `Description` are both marked `[EditorRequired]`; omitting either will produce a build warning.
- The `Page` parameter does not use `@bind-Page` internally — it is a one-way input. Changes are propagated back to the parent via `PageChanged`. Using `@bind-Page` is the recommended pattern for keeping the parent in sync.
- When `ShowJumpToPage` is `true` and the user enters a value outside the valid range (`< 1` or `> NumberOfPages`), the input is silently reset without triggering navigation.
- `PageRangeSize` always preserves the first and last page buttons; only the middle pages are collapsed into ellipses.

## References

- [Bootstrap Italia — Paginazione](https://italia.github.io/bootstrap-italia/docs/componenti/paginazione/)
- [WAI-ARIA Authoring Practices — Pagination](https://www.w3.org/WAI/ARIA/apg/patterns/navigation/)
