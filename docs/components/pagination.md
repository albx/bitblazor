# BitPagination

The `BitPagination` component provides a [pagination control using Bootstrap Italia styles](https://italia.github.io/bootstrap-italia/docs/componenti/paginazione/).

## Namespace

```csharp
BitBlazor.Components
```

## Description

The Pagination component enables users to navigate through large data sets split across multiple pages. It renders previous/next buttons, individual page links with optional ellipsis truncation, and optional extras such as a jump-to-page input and a total-items summary. Two view modes are available: the default full control and a compact simple mode.

`BitPagination` supports both **interactive** and **static SSR** render modes. In interactive mode, page changes are handled via two-way binding with `@bind-Page`. In SSR mode, supply a `PageLinkGenerator` to render real `<a href>` links that trigger full browser navigation — no JavaScript required.

## Parameters

| Name | Type | Required | Default | Description |
|------|------|----------|---------|-------------|
| `NumberOfPages` | `int` | ✓ | `1` | Total number of pages in the data set. |
| `Description` | `string` | ✓ | `""` | Text placed in the `aria-label` of the wrapping `<nav>` element. |
| `Page` | `int` | ✗ | `1` | The currently selected page. Use `@bind-Page` for two-way binding. |
| `PageChanged` | `EventCallback<int>` | ✗ | - | Callback invoked when the active page changes. Receives the new page number. |
| `PageLinkGenerator` | `Func<int, string>?` | ✗ | `null` | When provided, each page button is rendered as a real `<a href>` pointing to the URL returned by this function for the given page number. Required for SSR compatibility. When `null`, page buttons use `href="#"` and rely on interactive C# event handlers. |
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

    private async Task HandlePageChanged()
    {
        await LoadDataAsync(currentPage);
    }
}
```

### SSR pagination (Static Server-Side Rendering)

Use `PageLinkGenerator` to make each page button a real `<a href>` link. This works without JavaScript and is required for components rendered with `@attribute [StreamRendering]` or in a fully static SSR context.

The current page is typically encoded as a route parameter so the server can pre-render the correct page on load.

```razor
@page "/news"
@page "/news/{Page:int}"
@attribute [StreamRendering]

@inject INewsService NewsService

<BitPagination NumberOfPages="@totalPages"
               Page="@currentPage"
               PageLinkGenerator="@(page => $"/news/{page}")"
               Description="Navigate news pages"
               Alignment="PaginationAlignment.Center"
               PageRangeSize="2" />

@code {
    [Parameter] public int Page { get; set; }

    private int currentPage;
    private int totalPages;

    protected override async Task OnInitializedAsync()
    {
        currentPage = Page < 1 ? 1 : Page;
        var result = await NewsService.GetNewsAsync(currentPage);
        totalPages = (int)Math.Ceiling((double)result.TotalCount / 10);
    }
}
```

### Interactive mode with shareable URL

Combine `PageLinkGenerator` with `@bind-Page` when you want both a C# callback (e.g. to update in-memory state without full navigation) and a real URL that can be bookmarked or shared. In interactive mode `@onclick:preventDefault` intercepts clicks so the C# handler runs; in SSR the browser follows the real `href`.

```razor
<BitPagination NumberOfPages="@totalPages"
               @bind-Page="currentPage"
               @bind-Page:after="LoadCurrentPage"
               PageLinkGenerator="@(page => $"/products?page={page}")"
               Description="Navigate products" />

@code {
    private int currentPage = 1;
    private int totalPages = 10;

    private async Task LoadCurrentPage()
    {
        await LoadDataAsync(currentPage);
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
- Disabled items receive `aria-hidden="true"` and `tabindex="-1"` so they are skipped by keyboard and screen-reader navigation. The previous-page and next-page buttons are disabled automatically when already on the first or last page respectively.
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
| `<li>` | `disabled` | `Disabled == true`, or the previous-page button when on page 1, or the next-page button when on the last page |

## Generated HTML Structure

When `PageLinkGenerator` is **not** set (interactive mode), page buttons use `href="#"` and rely on Blazor event handlers:

```html
<nav class="pagination-wrapper" aria-label="Navigate pages">
    <ul class="pagination">
        <!-- Previous page button (disabled on page 1) -->
        <li class="page-item disabled">
            <a class="page-link" style="cursor: pointer;" tabindex="-1" aria-hidden="true">
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

When `PageLinkGenerator` is set, each `href` is populated with the URL returned by the generator. The previous/next buttons receive the adjacent page URL; disabled nav buttons (first page's prev, last page's next) keep `href="#"`:

```html
<!-- With PageLinkGenerator="@(page => $"/news/{page}")" on page 3 of 10 -->
<li class="page-item">
    <a class="page-link" href="/news/2"><!-- prev --></a>
</li>
<li class="page-item">
    <a class="page-link" href="/news/1">1</a>
</li>
<li class="page-item">
    <a class="page-link" href="/news/2">2</a>
</li>
<li class="page-item">
    <a class="page-link" href="/news/3" aria-current="page">3</a>
</li>
...
<li class="page-item">
    <a class="page-link" href="/news/4"><!-- next --></a>
</li>
```

## Notes

- `NumberOfPages` and `Description` are both marked `[EditorRequired]`; omitting either will produce a build warning.
- The `Page` parameter supports two-way binding via `@bind-Page`. Use `@bind-Page:after` to react to page changes (e.g. to reload data).
- When `ShowJumpToPage` is `true` and the user enters a value outside the valid range (`< 1` or `> NumberOfPages`), the input is silently reset without triggering navigation.
- `PageRangeSize` always preserves the first and last page buttons; only the middle pages are collapsed into ellipses.
- **SSR compatibility**: In Blazor static SSR, `@onclick` C# callbacks never fire. Set `PageLinkGenerator` to produce real `<a href>` links — the component will then work purely via browser navigation with no JavaScript required.
- **Progressive enhancement**: When both `PageLinkGenerator` and `@bind-Page` are set, the component uses `@onclick:preventDefault` to intercept clicks in interactive mode (running the C# handler) while still exposing a valid `href` for SSR and for right-click / open-in-new-tab scenarios.
- **Disabled nav buttons**: The previous-page button on page 1 and the next-page button on the last page are automatically disabled — they receive the `disabled` CSS class on `<li>`, plus `aria-hidden="true"` and `tabindex="-1"` on the `<a>`, regardless of the `Disabled` parameter. When `PageLinkGenerator` is set, these boundary buttons render without an `href` since there is no valid target page to link to.

## References

- [Bootstrap Italia — Paginazione](https://italia.github.io/bootstrap-italia/docs/componenti/paginazione/)
- [WAI-ARIA Authoring Practices — Pagination](https://www.w3.org/WAI/ARIA/apg/patterns/navigation/)
