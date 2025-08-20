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
    /// Gets or sets additional css classes
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// Gets or sets additional attributes that do not match any other defined parameters.
    /// </summary>
    /// <remarks>
    /// This property is typically used to capture arbitrary HTML attributes for components or elements. 
    /// The keys represent attribute names, and the values represent their corresponding values.
    /// </remarks>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();


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
