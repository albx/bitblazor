using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a component that provides pagination functionality for navigating large data sets in a user interface.
/// </summary>
/// <remarks>
/// Use this component to enable users to move between pages of data efficiently.
/// </remarks>
public partial class BitPagination : BitComponentBase
{
    /// <summary>
    /// Gets or sets the total number of pages available for pagination.
    /// </summary>
    /// <remarks>
    /// This property must be set before rendering the component to ensure correct pagination behavior. 
    /// The value should be a positive integer representing the total number of pages in the data set.
    /// </remarks>
    [Parameter]
    [EditorRequired]
    public int NumberOfPages { get; set; } = 1;

    /// <summary>
    /// Gets or sets the current page number. The default value is 1.
    /// </summary>
    /// <remarks>
    /// The value must be greater than or equal to 1; specifying a value less than 1 may result in unexpected behavior.
    /// </remarks>
    [Parameter]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the callback that is invoked when the current page changes.
    /// </summary>
    /// <remarks>
    /// Use this parameter to handle page change events in the parent component. 
    /// The callback receives the new page number as an integer argument, allowing the parent to update its state or perform additional actions in response to pagination.
    /// </remarks>
    [Parameter]
    public EventCallback<int> PageChanged { get; set; }

    /// <summary>
    /// Gets or sets the component's description which will be put in the aria-label attribute of the main container
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the label text displayed for the previous page navigation link.
    /// </summary>
    /// <remarks>
    /// Set this property to customize the text shown for the previous page button in the pagination component. 
    /// The default value is "previous page".
    /// </remarks>
    [Parameter]
    public string PreviousPageLabel { get; set; } = "previous page";

    /// <summary>
    /// Gets or sets the template used to render the previous page button in the pager component.
    /// </summary>
    /// <remarks>
    /// Set this property to customize the appearance or content of the previous page navigation button. 
    /// If not set, a default template is used.
    /// </remarks>
    [Parameter]
    public RenderFragment? PreviousPageTemplate { get; set; }

    /// <summary>
    /// Gets or sets the label text displayed for the next page navigation link.
    /// </summary>
    /// <remarks> 
    /// Set this property to customize the text shown to users for the next page button in the pagination component.
    /// The default value is "next page".
    /// </remarks>
    [Parameter]
    public string NextPageLabel { get; set; } = "next page";

    /// <summary>
    /// Gets or sets the template used to render the content for the next page button.
    /// </summary>
    /// <remarks>
    /// Set this property to customize the appearance or behavior of the next page navigation element. 
    /// If not set, a default template may be used.
    /// </remarks>
    [Parameter]
    public RenderFragment? NextPageTemplate { get; set; }

    /// <summary>
    /// Gets or sets the alignment of the pagination controls within their container.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="PaginationAlignment.Left"/>, which aligns the pagination controls to the left. 
    /// Other options, such as center or right alignment, can be used to adjust the visual positioning of the pagination elements as needed.
    /// </remarks>
    [Parameter]
    public PaginationAlignment Alignment { get; set; } = PaginationAlignment.Left;

    /// <summary>
    /// Gets or sets a value indicating whether the component is disabled and cannot be interacted with.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the number of page buttons to show on each side of the current page
    /// before an ellipsis is rendered. When <c>null</c>, all pages are always shown.
    /// </summary>
    [Parameter]
    public int? PageRangeSize { get; set; }
    
    /// <summary>
    /// Gets or sets the template used to display the total number of items.
    /// </summary>
    /// <remarks>
    /// Use this property to customize how to display the pagination total section.
    /// </remarks>
    [Parameter]
    public RenderFragment? TotalItemsTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the jump-to-page control is displayed in the pagination component.
    /// </summary>
    [Parameter]
    public bool ShowJumpToPage { get; set; }

    /// <summary>
    /// Gets or sets the template used to render the label for the jump-to-page input.
    /// </summary>
    [Parameter]
    public RenderFragment? JumpToPageLabelTemplate { get; set; }

    /// <summary>
    /// Gets or sets the display mode for the pagination component.
    /// </summary>
    /// <remarks>
    /// Use this property to control how the pagination UI is rendered. 
    /// The available modes are defined by the PaginationViewMode enumeration. 
    /// Changing this property affects the appearance and behavior of the pagination controls.
    /// </remarks>
    [Parameter]
    public PaginationViewMode ViewMode { get; set; } = PaginationViewMode.Default;

    /// <summary>
    /// Gets or sets the template used to render visually hidden content for simple mode pagination, typically for accessibility purposes.
    /// If not provided a default content (i.e. "page 1 of 10") will be displayed.
    /// </summary>
    /// <remarks>
    /// Use this template to provide additional information for screen readers or assistive technologies when simple mode pagination is enabled. 
    /// The template receives the current pagination state as its context.
    /// </remarks>
    [Parameter]
    public RenderFragment<PaginationState>? SimpleModeVisuallyHiddenTemplate { get; set; }

    /// <summary>
    /// Gets or sets a function that generates the navigation URL for a given page number.
    /// </summary>
    /// <remarks>
    /// When set, each page item (including previous and next) renders as a real anchor link,
    /// enabling SSR-compatible navigation and browser features such as "Open in new tab".
    /// In interactive render modes, <see cref="PageChanged"/> still fires on click; the href
    /// acts as progressive enhancement.
    /// Previous and next links receive <c>null</c> when already on the first or last page respectively,
    /// falling back to <c>href="#"</c>.
    /// </remarks>
    [Parameter]
    public Func<int, string>? PageLinkGenerator { get; set; }

    private string jumpToPageId = string.Empty;
    private string jumpToPageLabelClass = string.Empty;
    private string jumpToPageValue = string.Empty;

    private PaginationState state;

    private bool PreviousPageLinkDisabled => Disabled || state.IsFirstPage;

    private bool NextPageLinkDisabled => Disabled || state.IsLastPage;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        state = new(Page, NumberOfPages);

        if (ShowJumpToPage)
        {
            SetJumpToPageDefaults();
        }
    }

    internal bool IsCurrentPage(int page) => state.CurrentPage == page;

    private void SetJumpToPageDefaults()
    {
        if (string.IsNullOrEmpty(jumpToPageId))
        {
            var baseId = !string.IsNullOrWhiteSpace(Id) ? Id : Guid.NewGuid().ToString("N");
            jumpToPageId = $"jumpToPage-{baseId}";
        }
    }

    private async Task ChangePageAsync(int page)
    {
        if (state.CurrentPage == page)
        {
            return;
        }

        state = state with { CurrentPage = page };
        await PageChanged.InvokeAsync(page);
    }

    private async Task MoveToPreviousPageAsync()
    {
        if (state.IsFirstPage)
        {
            return;
        }

        var page = state.CurrentPage - 1;
        await ChangePageAsync(page);
    }

    private async Task MoveToNextPageAsync()
    {
        if (state.IsLastPage)
        {
            return;
        }

        var page = state.CurrentPage + 1;
        await ChangePageAsync(page);
    }

    private string ComputeContainerCssClass()
    {
        var builder = new CssClassBuilder("pagination-wrapper");

        var alignmentClass = Alignment switch
        {
            PaginationAlignment.Center => "justify-content-center",
            PaginationAlignment.Right => "justify-content-end",
            _ => string.Empty
        };
        builder.Add(alignmentClass);

        if (TotalItemsTemplate is not null)
        {
            builder.Add("pagination-total");
        }

        AddCustomCssClass(builder);

        return builder.Build();
    }

    internal IEnumerable<int?> GetPageSequence()
    {
        if (PageRangeSize is null)
            return Enumerable.Range(1, state.NumberOfPages).Select(p => (int?)p);

        var range = PageRangeSize.Value;
        var start = Math.Max(2, state.CurrentPage - range);
        var end = Math.Min(state.NumberOfPages - 1, state.CurrentPage + range);

        var result = new List<int?> { 1 };

        if (start > 2) result.Add(null);
        for (int i = start; i <= end; i++) result.Add(i);
        if (end < state.NumberOfPages - 1) result.Add(null);

        if (state.NumberOfPages > 1) result.Add(state.NumberOfPages);

        return result;
    }

    private async Task JumpToPageAsync()
    {
        try
        {
            if (!int.TryParse(jumpToPageValue, out int page))
            {
                return;
            }

            if (page < 1 || page > state.NumberOfPages)
            {
                return;
            }

            await ChangePageAsync(page);
        }
        finally
        {
            jumpToPageValue = string.Empty;
        }
    }

    private void SetJumpToPageLabelActive() => jumpToPageLabelClass = "active";

    private void UpdateJumpToPageLabelClass() 
        => jumpToPageLabelClass = string.IsNullOrEmpty(jumpToPageValue) ? string.Empty : "active";

    private string? GetPageHref(int page)
        => PageLinkGenerator?.Invoke(page);

    private string? GetPreviousPageHref()
        => !state.IsFirstPage ? GetPageHref(state.CurrentPage - 1) : null;

    private string? GetNextPageHref()
        => !state.IsLastPage ? GetPageHref(state.CurrentPage + 1) : null;
}
