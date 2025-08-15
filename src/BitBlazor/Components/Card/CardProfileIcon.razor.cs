using BitBlazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class CardProfileIcon
{
    [CascadingParameter]
    CardProfileHeader Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the icon to be displayed in the profile section.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the color of the icon.
    /// </summary>
    [Parameter]
    public IconColor IconColor { get; set; } = IconColor.Primary;

    /// <summary>
    /// Gets or sets a value indicating whether the element is hidden from assistive technologies.
    /// </summary>
    [Parameter]
    public bool AriaHidden { get; set; }
}
