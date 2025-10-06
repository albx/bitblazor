using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class BitAvatarGroupItem : BitAvatarBase
{   
    [CascadingParameter]
    BitAvatarGroup Parent { get; set; } = default!;
}