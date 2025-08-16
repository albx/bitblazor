using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the title of the card
/// </summary>
public partial class CardTitle
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the title
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the level of the heading of the card title (default h3)
    /// </summary>
    [Parameter]
    public Typography Typography { get; set; } = Typography.H3;

    /// <summary>
    /// Gets or sets whether the title has an icon associated
    /// </summary>
    [Parameter]
    public bool HasIcon { get; set; }

    private string ComputeCssClasses()
    {
        var builder = new CssClassBuilder("it-card-title");

        if (HasIcon)
        {
            builder.Add("it-card-title-icon");
        }

        return builder.Build();
    }
}
