using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents an individual page item within a pagination component.
/// </summary>
/// <remarks>
/// This class is typically used as a child of the BitPagination component to display a specific page number. 
/// It manages accessibility attributes based on the current page selection.
/// </remarks>
public partial class BitPageItem
{
    [CascadingParameter]
    BitPagination Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the current page index for pagination.
    /// </summary>
    /// <remarks>
    /// Setting this property determines which page of data is displayed or processed. 
    /// Ensure that the value is within the valid range for the data source.
    /// </remarks>
    [Parameter]
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the component is disabled and cannot be interacted with.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    private IDictionary<string, object> attributes = new Dictionary<string, object>();

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (Page == Parent.CurrentPage)
        {
            attributes["aria-current"] = "page";
        }
        else
        {
            attributes.Remove("aria-current");
        }
    }

    private Task SetPageAsync() => Parent.ChangePageAsync(Page);

    private string ComputePageItemCssClass()
    {
        var builder = new CssClassBuilder("page-item");
        if (Disabled)
        {
            builder.Add("disabled");
        }

        return builder.Build();
    }
}
