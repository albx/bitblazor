using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents an icon displayed within a card title, providing customization options for its appearance and accessibility.
/// </summary>
/// <remarks>
/// The <see cref="CardTitleIcon"/> component is used to define an icon within a card title.
/// It allows customization of the icon's name, role, title, and accessibility attributes.
/// </remarks>
public partial class CardTitleIcon
{
    [CascadingParameter]
    CardTitle Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the icon to be displayed in the card title.
    /// </summary>
    /// <remarks>
    /// This property sets the <see cref="Utilities.BitIcon.IconName"/> value
    /// </remarks>
    [Parameter]
    [EditorRequired]
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role of the icon, typically used to define its semantic purpose or accessibility role.
    /// </summary>
    /// <remarks>
    /// This property sets the <see cref="Utilities.BitIcon.Role"/> value.
    /// </remarks>
    [Parameter]
    public string? IconRole { get; set; }

    /// <summary>
    /// Gets or sets the title text associated with the icon.
    /// </summary>
    /// <remarks>
    /// This property sets the <see cref="Utilities.BitIcon.Title"/> value.
    /// </remarks>
    [Parameter]
    public string? IconTitle { get; set; }

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
