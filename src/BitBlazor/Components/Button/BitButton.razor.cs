using Microsoft.AspNetCore.Components;
using System.Text;

namespace BitBlazor.Components;

public partial class BitButton
{
    [Parameter]
    [EditorRequired]
    public string Text { get; set; } = string.Empty;

    [Parameter]
    [EditorRequired]
    public Color Color { get; set; }

    [Parameter]
    public ButtonType Type { get; set; } = ButtonType.Button;

    
    private string ComputeCssClasses()
    {
        var computedClasses = new StringBuilder();

        var colorClass = Color switch
        {
            Color.Primary => "btn-primary",
            _ => string.Empty
        };
        computedClasses.Append(colorClass);

        return $"{computedClasses}";
    }
}
