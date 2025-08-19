using BitBlazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the container for the icon displayed in the card banner.
/// </summary>
public partial class CardBannerIcon
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

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
    /// Gets or sets additional attributes that do not match any other defined parameters.
    /// </summary>
    /// <remarks>
    /// This property is typically used to capture arbitrary HTML attributes for components or elements. 
    /// The keys represent attribute names, and the values represent their corresponding values.
    /// </remarks>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();
}
