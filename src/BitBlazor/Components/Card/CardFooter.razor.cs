using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the footer section of the card
/// </summary>
public partial class CardFooter
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the card footer
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the it-card-related class
    /// </summary>
    [Parameter]
    public bool CardRelated { get; set; } = true;

    /// <summary>
    /// Gets or sets the aria-label attribute
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets additional css classes
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    private Dictionary<string, object> attributes = new();

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (!string.IsNullOrWhiteSpace(AriaLabel))
        {
            attributes["aria-label"] = AriaLabel;
        }
        else
        {
            attributes.Remove("aria-label");
        }
    }

    private string ComputeCssClasses()
    {
        var builder = new CssClassBuilder("it-card-footer");

        if (CardRelated)
        {
            builder.Add("it-card-related");
        }

        if (!string.IsNullOrWhiteSpace(CssClass))
        {
            builder.Add(CssClass);
        }

        return builder.Build();
    }
}
