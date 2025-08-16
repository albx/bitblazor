using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a badge component using Bootstrap Italia styles.
/// </summary>
public partial class BitBadge
{
    /// <summary>
    /// Gets or sets the text content of the badge
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the background color of the badge
    /// </summary>
    [Parameter]
    [EditorRequired]
    public Color BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the variant of the badge
    /// </summary>
    [Parameter]
    public Variant Variant { get; set; } = Variant.Solid;

    /// <summary>
    /// Gets or sets whether the badge is rounded
    /// </summary>
    [Parameter]
    public bool Rounded { get; set; }

    private string ComputeCssClasses()
    {
        var builder = new CssClassBuilder("badge");
        AddColorClass(builder);

        if (Rounded)
        {
            builder.Add("rounded-pill");
        }

        return builder.Build();
    }

    private void AddColorClass(CssClassBuilder builder)
    {
        var backgroundColorCssClass = (BackgroundColor, Variant) switch
        {
            (Color.Primary, Variant.Solid) => "bg-primary",
            (Color.Secondary, Variant.Solid) => "bg-secondary",
            (Color.Success, Variant.Solid) => "bg-success",
            (Color.Warning, Variant.Solid) => "bg-warning",
            (Color.Danger, Variant.Solid) => "bg-danger",
            (Color.Primary, Variant.Outline) => "bg-white text-primary",
            (Color.Secondary, Variant.Outline) => "bg-white text-secondary",
            (Color.Success, Variant.Outline) => "bg-white text-success",
            (Color.Warning, Variant.Outline) => "bg-white text-warning",
            (Color.Danger, Variant.Outline) => "bg-white text-danger",
            _ => string.Empty
        };

        builder.Add(backgroundColorCssClass);
    }
}
