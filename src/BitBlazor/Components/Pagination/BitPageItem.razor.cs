using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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
    public int? Page { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when a page item is clicked.
    /// </summary>
    /// <remarks>
    /// Use this property to handle page item click events in the parent component. 
    /// The callback is triggered when the user interacts with a page item, allowing custom logic to be executed in response.
    /// </remarks>
    [Parameter]
    public EventCallback PageItemClicked { get; set; }

    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    /// <remarks>
    /// Use this property to specify the child elements or markup that will be rendered within the component. 
    /// Typically set implicitly by placing content between the component's opening and closing tags in a Razor file.
    /// </remarks>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets a value indicating whether the component is disabled and cannot be interacted with.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the URL to navigate to when the page item is clicked.
    /// </summary>
    /// <remarks>
    /// When set, the page item renders as a real anchor link with the provided href,
    /// enabling SSR-compatible navigation and improving accessibility (e.g. right-click "Open in new tab").
    /// When not set, navigation is handled via the <see cref="PageItemClicked"/> callback.
    /// </remarks>
    [Parameter]
    public string? Href { get; set; }

    private IDictionary<string, object> attributes = new Dictionary<string, object>();

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (Page.HasValue && Parent.IsCurrentPage(Page.Value))
        {
            attributes["aria-current"] = "page";
        }
        else
        {
            attributes.Remove("aria-current");
        }

        SetDisabledAttributes();
    }

    private void SetDisabledAttributes()
    {
        if (Disabled)
        {
            attributes["aria-hidden"] = "true";
            attributes["tabindex"] = "-1";
        }
        else
        {
            attributes.Remove("aria-hidden");
            if (string.IsNullOrEmpty(Href))
            {
                attributes["tabindex"] = "0";
            }
            else
            {
                attributes.Remove("tabindex");
            }
        }
    }

    private async Task ClickPageItemAsync()
    {
        if (Disabled)
        {
            return;
        }
        
        await PageItemClicked.InvokeAsync();
    }

    private async Task OnKeyDownAsync(KeyboardEventArgs args)
    {
        if (args.Key is "Enter")
        {
            await ClickPageItemAsync();
        }
    }

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
