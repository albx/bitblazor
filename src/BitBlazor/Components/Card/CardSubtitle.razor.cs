using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the element which displays the subtitle on a card component
/// </summary>
public partial class CardSubtitle
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the content to be rendered in the subtitle section of the card.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;
}
