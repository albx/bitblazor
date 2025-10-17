using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// BitAvatarGroup component renders a container for showing multiple user avatars as a list
/// It can contains both <see cref="BitAvatarGroupItem"/> and <see cref="BitAvatar"/> elements.
/// A <see cref="BitAvatar"/> contained in a <see cref="BitAvatarGroup"/> is rendered as a <see cref="BitAvatarGroupItem"/> element.
/// </summary>
public partial class BitAvatarGroup : BitComponentBase
{
    /// <summary>
    /// Gets or sets the content of the button
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;
}

