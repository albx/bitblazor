using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a card component using Bootstrap Italia styles.
/// </summary>
public partial class BitCard
{
    /// <summary>
    /// Gets or sets the type of the card (Default or Inline).
    /// </summary>
    [Parameter]
    public CardType Type { get; set; } = CardType.Default;

    /// <summary>
    /// Gets or sets whether display the inline card in reverse mode.
    /// </summary>
    /// <remarks>This parameter works only when the card is Inline</remarks>
    [Parameter]
    public bool Reverse { get; set; }

    /// <summary>
    /// Gets or sets whether display the inline card as mini variant.
    /// </summary>
    /// <remarks>This parameter works only when the card is Inline</remarks>
    [Parameter]
    public bool Mini { get; set; }

    /// <summary>
    /// Gets or sets the content to be rendered in the title section of the card.
    /// </summary>
    [Parameter]
    public RenderFragment CardTitle { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content to be rendered in the body section of the card
    /// </summary>
    [Parameter]
    public RenderFragment? CardBody { get; set; } = default!;

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

    /// <summary>
    /// Gets or sets the level of the heading of the card title (default h3)
    /// </summary>
    [Parameter]
    public Typography TitleTypography { get; set; } = Typography.H3;

    /// <summary>
    /// Gets or sets whether the component should display a border. (default true)
    /// </summary>
    [Parameter]
    public bool Bordered { get; set; } = true;

    /// <summary>
    /// Gets or sets the shadow style applied to the card.
    /// </summary>
    [Parameter]
    public CardShadow Shadow { get; set; } = CardShadow.Small;

    /// <summary>
    /// Gets or sets whether the component should occupy the full height of its container.
    /// </summary>
    [Parameter]
    public bool FullHeight { get; set; }

    private string ComputedCssClasses => $"it-card rounded {ComputeCssClasses()}".Trim();

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();

        var cardClass = Type switch
        {
            CardType.Inline => "it-card-inline",
            _ => string.Empty
        };

        if (Type is CardType.Inline)
        {
            if (Reverse)
            {
                cssClasses.Add("it-card-inline-reverse");
            }

            if (Mini)
            {
                cssClasses.Add("it-card-inline-mini");
            }
        }

        if (!string.IsNullOrEmpty(cardClass))
        {
            cssClasses.Add(cardClass);
        }

        if (Bordered)
        {
            cssClasses.Add("border");
        }

        var shadowClass = Shadow switch
        {
            CardShadow.Small => "shadow-sm",
            CardShadow.Medium => "shadow",
            CardShadow.Large => "shadow-lg",
            _ => string.Empty
        };
        cssClasses.Add(shadowClass);

        if (FullHeight)
        {
            cssClasses.Add("it-card-height-full");
        }

        if (CardImageContainer is not null)
        {
            cssClasses.Add("it-card-image");
        }

        return string.Join(" ", cssClasses);
    }
}
