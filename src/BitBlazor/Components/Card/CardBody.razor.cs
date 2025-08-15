using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the body of the card
/// </summary>
public partial class CardBody
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the card body
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}
