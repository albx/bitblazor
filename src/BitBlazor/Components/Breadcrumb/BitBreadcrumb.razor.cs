using BitBlazor.Components.Breadcrumb;
using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class BitBreadcrumb : BitComponentBase
{
    /// <summary>
    /// Gets or sets the ARIA label for the breadcrumb component.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the items shown on the breadcrumb.
    /// </summary>
    [Parameter]
    public IEnumerable<BitBreadcrumbItem>? Items { get; set; }

    /// <summary>
    /// Gets or sets the separator to show between breadcrumb components.
    /// </summary>
    [Parameter]
    public string? Separator { get; set; } = "/";

    /// <summary>
    /// Gets or sets a value indicating whether the component is rendered on a dark background, or not.
    /// </summary>
    [Parameter]
    public bool Dark { get; set; } = false;

    private string ComputeCssClasses()
    {
        var builder = new CssClassBuilder("breadcrumb");

        if (Dark)
        {
            builder.Add("dark");
        }

        return builder.Build();
    }

    private bool IsLeaf(BitBreadcrumbItem? item)
    {
        return item is not null && Items?.LastOrDefault() == item;
    }
}

