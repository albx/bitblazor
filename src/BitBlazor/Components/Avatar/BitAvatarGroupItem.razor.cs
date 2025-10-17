using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// BitAvatarGroupitem component renders a user avatar with support for icons, images, text and links included in a <see cref="BitAvatarGroup"/> component
/// </summary>
public partial class BitAvatarGroupItem : BitAvatarBase
{   
    [CascadingParameter]
    BitAvatarGroup Parent { get; set; } = default!;
}