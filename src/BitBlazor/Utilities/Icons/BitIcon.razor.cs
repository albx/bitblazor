using Microsoft.AspNetCore.Components;

namespace BitBlazor.Utilities;

public partial class BitIcon
{
    [Parameter]
    [EditorRequired]
    public string IconName { get; set; } = string.Empty;

    [Parameter]
    public IconSize Size { get; set; } = IconSize.Default;

    [Parameter]
    public bool Padded { get; set; }

    [Parameter]
    public IconColor Color { get; set; } = IconColor.Default;

    [Parameter]
    public IconAlignment Align { get; set; } = IconAlignment.Default;

    [Parameter]
    public string? CssClass { get; set; }

    private string Href => $"/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#{IconName}";

    private string ComputedCssClasses => $"icon {ComputeCssClasses()}".Trim();

    private string ComputeCssClasses()
    {
        var cssClasses = new HashSet<string>();
        AddSizeClass(cssClasses);
        AddColorClass(cssClasses);
        AddAlignmentClass(cssClasses);

        if (Padded)
        {
            cssClasses.Add("icon-padded");
        }

        if (!string.IsNullOrWhiteSpace(CssClass))
        {
            cssClasses.Add(CssClass);
        }

        return string.Join(" ", cssClasses);
    }

    private void AddAlignmentClass(HashSet<string> cssClasses)
    {
        var alignmentClass = Align switch
        {
            IconAlignment.Bottom => "align-bottom",
            IconAlignment.Middle => "align-middle",
            IconAlignment.Top => "align-top",
            _ => string.Empty
        };

        if (!string.IsNullOrEmpty(alignmentClass))
        {
            cssClasses.Add(alignmentClass);
        }
    }

    private void AddColorClass(HashSet<string> cssClasses)
    {
        var colorClass = Color switch
        {
            IconColor.Primary => "icon-primary",
            IconColor.Secondary => "icon-secondary",
            IconColor.Success => "icon-success",
            IconColor.Warning => "icon-warning",
            IconColor.Danger => "icon-danger",
            IconColor.Light => "icon-light",
            IconColor.White => "icon-white",
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
            IconSize.ExtraSmall => "icon-xs",
            IconSize.Small => "icon-sm",
            IconSize.Large => "icon-lg",
            IconSize.ExtraLarge => "icon-xl",
            _ => string.Empty
        };

        if (!string.IsNullOrEmpty(sizeClass))
        {
            cssClasses.Add(sizeClass);
        }
    }
}
