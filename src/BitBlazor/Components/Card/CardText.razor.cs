using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a component that displays a block of text content.
/// </summary>
public partial class CardText
{
    [CascadingParameter]
    BitCard Card { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content to be displayed
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;
}
