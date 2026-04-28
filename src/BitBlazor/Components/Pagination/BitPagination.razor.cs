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
    /// Gets or sets a value indicating whether navigation to the previous page is disabled.
    /// </summary>
    [Parameter]
    public bool PreviousPageDisabled { get; set; }

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
    /// Gets or sets a value indicating whether navigation to the next page is disabled.
    /// </summary>
    [Parameter]
    public bool NextPageDisabled { get; set; }

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
    /// Gets or sets the collection of page indexes that are disabled and cannot be selected.
    /// </summary>
    /// <remarks>
    /// Page indexes in this collection will be rendered as unavailable in the UI. 
    /// Use this property to prevent user interaction with specific pages, such as for access control or workflow restrictions.
    /// </remarks>
    [Parameter]
    public int[] DisabledPages { get; set; } = [];

    /// <summary>
    /// Gets or sets the number of page buttons to show on each side of the current page
    /// before an ellipsis is rendered. When <c>null</c>, all pages are always shown.
    /// </summary>
    [Parameter]
    public int? PageRangeSize { get; set; }

    internal int CurrentPage { get; private set; }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        CurrentPage = Page;
    }

    internal async Task ChangePageAsync(int page)
    {
        if (CurrentPage == page)
        {
            return;
        }

        CurrentPage = page;
        await PageChanged.InvokeAsync(page);

        StateHasChanged();
    }

    private async Task MoveToPreviousPageAsync()
    {
        if (CurrentPage == 1)
        {
            return;
        }

        var page = CurrentPage - 1;
        await ChangePageAsync(page);
    }

    private async Task MoveToNextPageAsync()
    {
        if (CurrentPage == NumberOfPages)
        {
            return;
        }

        var page = CurrentPage + 1;
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

        return builder.Build();
    }

    internal IEnumerable<int?> GetPageSequence()
    {
        if (PageRangeSize is null)
            return Enumerable.Range(1, NumberOfPages).Select(p => (int?)p);

        var range = PageRangeSize.Value;
        var start = Math.Max(2, CurrentPage - range);
        var end = Math.Min(NumberOfPages - 1, CurrentPage + range);

        var result = new List<int?> { 1 };

        if (start > 2) result.Add(null);
        for (int i = start; i <= end; i++) result.Add(i);
        if (end < NumberOfPages - 1) result.Add(null);

        if (NumberOfPages > 1) result.Add(NumberOfPages);

        return result;
    }

    private bool IsPageDisabled(int page) => Disabled || DisabledPages.Contains(page);

    private string ComputePreviousPageItemCssClass()
    {
        var builder = new CssClassBuilder("page-item");
        if (Disabled || PreviousPageDisabled)
        {
            builder.Add("disabled");
        }

        return builder.Build();
    }

    private string ComputeNextPageItemCssClass()
    {
        var builder = new CssClassBuilder("page-item");
        if (Disabled || NextPageDisabled)
        {
            builder.Add("disabled");
        }

        return builder.Build();
    }
}
