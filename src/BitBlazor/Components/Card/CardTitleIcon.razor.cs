using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class CardTitleIcon
{
    [CascadingParameter]
    CardTitle Parent { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Icon { get; set; } = string.Empty;

    [Parameter]
    public string? IconRole { get; set; }

    [Parameter]
    public string? IconTitle { get; set; }
}
