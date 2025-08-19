using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Utilities;

/// <summary>
/// Represents an icon component which displays one of the icons available in Bootstrap Italia.
/// </summary>
public partial class BitIcon : BitComponentBase
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

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (!string.IsNullOrWhiteSpace(Role))
        {
            AdditionalAttributes["role"] = Role;
        }
        else
        {
            AdditionalAttributes.Remove("role");
        }

        base.OnParametersSet();
    }

    private string ComputeCssClasses()
    {
        var builder = new CssClassBuilder("icon");
        AddSizeClass(builder);
        AddColorClass(builder);
        AddAlignmentClass(builder);

        if (Padded)
        {
            builder.Add("icon-padded");
        }

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private void AddAlignmentClass(CssClassBuilder builder)
    {
        var alignmentClass = Align switch
        {
            IconAlignment.Bottom => "align-bottom",
            IconAlignment.Middle => "align-middle",
            IconAlignment.Top => "align-top",
            _ => string.Empty
        };

        builder.Add(alignmentClass);
    }

    private void AddColorClass(CssClassBuilder builder)
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

        builder.Add(colorClass);
    }

    private void AddSizeClass(CssClassBuilder builder)
    {
        var sizeClass = Size switch
        {
            IconSize.ExtraSmall => "icon-xs",
            IconSize.Small => "icon-sm",
            IconSize.Large => "icon-lg",
            IconSize.ExtraLarge => "icon-xl",
            _ => string.Empty
        };

        builder.Add(sizeClass);
    }
}
