namespace BitBlazor.Components;

/// <summary>
/// Represents the state of the pagination control, including the current page and the total number of pages
/// </summary>
/// <param name="CurrentPage">The current page selected</param>
/// <param name="NumberOfPages">The total number of pages</param>
public record struct PaginationState(int CurrentPage, int NumberOfPages)
{
    /// <summary>
    /// Gets a value indicating whether the current page is the first page in the sequence.
    /// </summary>
    public bool IsFirstPage => CurrentPage == 1;

    /// <summary>
    /// Gets a value indicating whether the current page is the last page in the collection.
    /// </summary>
    public bool IsLastPage => CurrentPage == NumberOfPages;
}
