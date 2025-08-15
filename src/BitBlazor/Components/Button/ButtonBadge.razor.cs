using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a badge component that displays text and integrates with a parent button.
/// </summary>
/// <remarks>
/// The <see cref="ButtonBadge"/> is designed to be used in conjunction with a <see cref="BitButton"/> component. 
/// It displays a badge with customizable text and optional additional context for assistive technologies.
/// The badge's appearance is influenced by the parent button's properties, such as its variant and color.
/// </remarks>
public partial class ButtonBadge
{
    [CascadingParameter]
    BitButton Button { get; set; } = default!;

    /// <summary>
    /// Gets or sets the text to display in the badge
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional text to explain the context of the badge.
    /// This text is hidden visually and is only accessible to assistive technologies such as screen readers.
    /// </summary>
    [Parameter]
    public string? AdditionalText { get; set; }

    private Variant BadgeVariant => Button.Variant switch
    {
        Variant.Outline => Variant.Solid,
        Variant.Solid => Variant.Outline,
        _ => Variant.Outline
    };

    private Color BadgeBackgroundColor => Button.Color;
}
