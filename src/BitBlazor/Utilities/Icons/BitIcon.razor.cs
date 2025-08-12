using Microsoft.AspNetCore.Components;

namespace BitBlazor.Utilities;

/// <summary>
/// Represents an icon component which displays one of the icons available in Bootstrap Italia.
/// </summary>
public partial class BitIcon
{
    /// <summary>
    /// Gets or sets the name of the icon to display. This is required.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string IconName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the size of the icon.
    /// </summary>
    [Parameter]
    public IconSize Size { get; set; } = IconSize.Default;

    /// <summary>
    /// Gets or sets a value indicating whether the icon should have padding.
    /// </summary>
    [Parameter]
    public bool Padded { get; set; }

    /// <summary>
    /// Gets or sets the color of the icon.
    /// </summary>
    [Parameter]
    public IconColor Color { get; set; } = IconColor.Default;

    /// <summary>
    /// Gets or sets the alignment of the icon.
    /// </summary>
    [Parameter]
    public IconAlignment Align { get; set; } = IconAlignment.Default;

    /// <summary>
    /// Gets or sets additional CSS classes to apply to the icon.
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// Gets or sets the role of the icon
    /// </summary>
    [Parameter]
    public string? Role { get; set; }

    /// <summary>
    /// Gets or sets the title of the icon
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    private string Href => $"/_content/BitBlazor/bootstrap-italia/svg/sprites.svg#{IconName}";

    private string ComputedCssClasses => $"icon {ComputeCssClasses()}".Trim();

    private Dictionary<string, object> attributes = new();

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (!string.IsNullOrWhiteSpace(Role))
        {
            attributes["role"] = Role;
        }
        else
        {
            attributes.Remove("role");
        }
    }

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();
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

    private void AddAlignmentClass(List<string> cssClasses)
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

    private void AddColorClass(List<string> cssClasses)
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

    private void AddSizeClass(List<string> cssClasses)
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
