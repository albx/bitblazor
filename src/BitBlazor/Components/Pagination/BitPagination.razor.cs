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

    [Parameter]
    public int Page { get; set; } = 1;

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
    /// Gets or sets the label text displayed for the next page navigation link.
    /// </summary>
    /// <remarks> 
    /// Set this property to customize the text shown to users for the next page button in the pagination component.
    /// The default value is "next page".
    /// </remarks>
    [Parameter]
    public string NextPageLabel { get; set; } = "next page";
}
