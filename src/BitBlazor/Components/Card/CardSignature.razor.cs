using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class CardSignature
{
    [CascadingParameter]
    BitCard Card { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content to be rendered in the signature section of the card.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;
}
