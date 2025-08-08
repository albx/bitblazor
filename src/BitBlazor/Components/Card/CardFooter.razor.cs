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
}
