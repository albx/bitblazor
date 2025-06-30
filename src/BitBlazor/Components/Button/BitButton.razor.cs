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
    public Size Size { get; set; } = Size.Default;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? Icon { get; set; }

    [Parameter]
    public bool IconRounded { get; set; }

    [Parameter]
    public IconPosition IconPosition { get; set; } = IconPosition.Start;

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
        var cssClasses = new HashSet<string>();
        AddColorClass(cssClasses);
        AddSizeClass(cssClasses);

        if (Disabled)
        {
            cssClasses.Add("disabled");
        }

        if (!string.IsNullOrWhiteSpace(CssClass))
        {
            cssClasses.Add(CssClass);
        }

        return string.Join(" ", cssClasses);
    }

    private void AddColorClass(HashSet<string> cssClasses)
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

    private void AddSizeClass(HashSet<string> cssClasses)
    {
        var sizeClass = Size switch
        {
            Size.Large => "btn-lg",
            Size.Small => "btn-sm",
            Size.Mini => "btn-xs",
            _ => string.Empty
        };

        if (!string.IsNullOrEmpty(sizeClass))
        {
            cssClasses.Add(sizeClass);
        }
    }
}
