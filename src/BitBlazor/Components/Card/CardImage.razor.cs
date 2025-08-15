using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents an image displayed within a card component, with configurable aspect ratio, source, and alternative text.
/// </summary>
public partial class CardImage
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the aspect ratio for the component.
    /// </summary>
    [Parameter]
    public Ratio Ratio { get; set; } = Ratio.Ratio1x1;

    /// <summary>
    /// Gets or sets the source URL of the image to be displayed.
    /// </summary>
    /// <remarks>This property is required and must be set to a valid image URL for proper rendering.</remarks>
    [Parameter]
    [EditorRequired]
    public string ImageSrc { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the alternative text for the image.
    /// </summary>
    /// <remarks>
    /// If the image is just for decorative purposes (i.e., purely decorative), this parameter can be left empty for accessibility compliance.
    /// </remarks>
    [Parameter]
    public string ImageAlt { get; set; } = string.Empty;

    private string ComputeRatioClass()
    {
        var ratioCssClass = Ratio switch
        {
            Ratio.Ratio1x1 => "ratio-1x1",
            Ratio.Ratio4x3 => "ratio-4x3",
            Ratio.Ratio16x9 => "ratio-16x9",
            Ratio.Ratio21x9 => "ratio-21x9",
            _ => string.Empty
        };

        return $"ratio {ratioCssClass}";
    }
}
