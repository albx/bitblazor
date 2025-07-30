using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a card component using Bootstrap Italia styles.
/// </summary>
public partial class BitCard
{
    /// <summary>
    /// Gets or sets the content to be rendered in the title section of the card.
    /// </summary>
    [Parameter]
    public RenderFragment CardTitle { get; set; } = default!;

    /// <summary>
    /// Gets or sets the level of the heading of the card title (default h3)
    /// </summary>
    [Parameter]
    public Typography TitleTypography { get; set; } = Typography.H3;

    /// <summary>
    /// Gets or sets the content to be rendered in the body section of the card
    /// </summary>
    [Parameter]
    public RenderFragment CardBody { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content to be rendered in the footer section of the card
    /// </summary>
    [Parameter]
    public RenderFragment? CardFooter { get; set; }

    /// <summary>
    /// Gets or sets the content to be rendered in the card image wrapper section of the card
    /// </summary>
    [Parameter]
    public RenderFragment? CardImageContainer { get; set; }

    private string ComputedCssClasses => $"it-card rounded shadow-sm border {ComputeCssClasses()}".Trim();

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();
        if (CardImageContainer is not null)
        {
            cssClasses.Add("it-card-image");
        }

        return string.Join(" ", cssClasses);
    }
}
