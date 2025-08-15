using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the header section of a card profile, containing profile information such as name, role, avatar, type, and image.
/// </summary>
public partial class CardProfileHeader
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the profile name section
    /// </summary>
    [Parameter]
    public RenderFragment ProfileName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the profile role section
    /// </summary>
    [Parameter]
    public RenderFragment? ProfileRole { get; set; }

    /// <summary>
    /// Gets or sets the content of the profile avatar section
    /// </summary>
    [Parameter]
    public RenderFragment? ProfileAvatar { get; set; }

    /// <summary>
    /// Gets or sets the content of the profile type section
    /// </summary>
    [Parameter]
    public RenderFragment? ProfileType { get; set; }

    /// <summary>
    /// Gets or sets the content of the profile image section
    /// </summary>
    [Parameter]
    public RenderFragment? ProfileImage { get; set; }

    /// <summary>
    /// Gets or sets the type of typography to be used for the profile name
    /// </summary>
    [Parameter]
    public Typography Typography { get; set; } = Typography.H4;
}
