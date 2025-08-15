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
    /// Gets or sets a value indicating whether the element is hidden from assistive technologies.
    /// </summary>
    [Parameter]
    public bool AriaHidden { get; set; }

    private Dictionary<string, object> attributes = new();

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (AriaHidden)
        {
            attributes["aria-hidden"] = "true";
        }
        else
        {
            attributes.Remove("aria-hidden");
        }
    }
}
