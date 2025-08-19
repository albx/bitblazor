using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a component that displays a block of text content.
/// </summary>
public partial class CardText
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content to be displayed
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets additional attributes that do not match any other defined parameters.
    /// </summary>
    /// <remarks>
    /// This property is typically used to capture arbitrary HTML attributes for components or elements. 
    /// The keys represent attribute names, and the values represent their corresponding values.
    /// </remarks>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();
}
