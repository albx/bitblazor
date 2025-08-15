using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the container to use for inline cards
/// </summary>
public partial class InlineCardContent
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the inline content component
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}
