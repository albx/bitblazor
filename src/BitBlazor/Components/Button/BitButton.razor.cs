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

    [Parameter]
    public Size? Size { get; set; }

    private string ComputedCssClasses => $"btn {ComputeCssClasses()}".Trim();

    private string ButtonTypeString => Type switch
    {
        ButtonType.Submit => "submit",
        ButtonType.Reset => "reset",
        _ => "button"
    };

    private string ComputeCssClasses()
    {
        var computedClasses = new StringBuilder();

        var colorClass = Color switch
        {
            Color.Primary => "btn-primary",
            Color.Secondary => "btn-secondary",
            Color.Success => "btn-success",
            Color.Danger => "btn-danger",
            Color.Warning => "btn-warning",
            Color.PrimaryOutline => "btn-outline-primary",
            Color.SecondaryOutline => "btn-outline-secondary",
            Color.SuccessOutline => "btn-outline-success",
            Color.DangerOutline => "btn-outline-danger",
            Color.WarningOutline => "btn-outline-warning",
            _ => string.Empty
        };
        computedClasses.Append(colorClass);

        var sizeClass = Size switch
        {
            BitBlazor.Size.Large => "btn-lg",
            BitBlazor.Size.Small => "btn-sm",
            BitBlazor.Size.Mini => "btn-xs",
            _ => string.Empty
        };
        computedClasses.Append($" {sizeClass}");

        return computedClasses.ToString();
    }
}
