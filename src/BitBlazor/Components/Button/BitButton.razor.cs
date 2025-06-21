using Microsoft.AspNetCore.Components;

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

    [Parameter]
    public EventCallback OnClick { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    private string ComputedCssClasses => $"btn {ComputeCssClasses()}".Trim();

    private string ButtonTypeString => Type switch
    {
        ButtonType.Submit => "submit",
        ButtonType.Reset => "reset",
        _ => "button"
    };

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();
        AddColorClass(cssClasses);
        AddSizeClass(cssClasses);

        if (!string.IsNullOrWhiteSpace(CssClass))
        {
            cssClasses.Add(CssClass);
        }

        return string.Join(" ", cssClasses);
    }

    private void AddColorClass(List<string> cssClasses)
    {
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

        if (!string.IsNullOrEmpty(colorClass))
        {
            cssClasses.Add(colorClass);
        }
    }

    private void AddSizeClass(List<string> cssClasses)
    {
        var sizeClass = Size switch
        {
            BitBlazor.Size.Large => "btn-lg",
            BitBlazor.Size.Small => "btn-sm",
            BitBlazor.Size.Mini => "btn-xs",
            _ => string.Empty
        };

        if (!string.IsNullOrEmpty(sizeClass))
        {
            cssClasses.Add(sizeClass);
        }
    }
}
