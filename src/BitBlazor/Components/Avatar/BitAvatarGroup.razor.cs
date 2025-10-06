using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class BitAvatarGroup : BitComponentBase
{
    /// <summary>
    /// Gets or sets the content of the button
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; }
}

