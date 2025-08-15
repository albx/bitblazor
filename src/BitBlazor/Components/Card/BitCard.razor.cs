using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a card component using Bootstrap Italia styles.
/// </summary>
public partial class BitCard
{
    /// <summary>
    /// Gets or sets the content of the card
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the card
    /// </summary>
    /// <remarks>
    /// Set this property in order to display the different types of card (inline cards, profile cards or banner cards)
    /// </remarks>
    [Parameter]
    public CardType Type { get; set; } = CardType.Default;

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
    /// Gets or sets additional css classes
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// Gets or sets the color of the top border of the card.
    /// </summary>
    [Parameter]
    public Color? BorderTopColor { get; set; }

    private string ComputedCssClasses => $"it-card rounded {ComputeCssClasses()}".Trim();

    #region Image management
    private bool hasImage = false;

    internal void NotifyHasImageChanged(bool hasImage)
    {
        this.hasImage = hasImage;
        StateHasChanged();
    }
    #endregion

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();

        switch (Type)
        {
            case CardType.Inline:
                AddInlineClasses(cssClasses);
                break;
            case CardType.Profile:
                cssClasses.Add("it-card-profile");
                break;
            case CardType.Banner:
                break;
            default:
                break;
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

        if (hasImage)
        {
            cssClasses.Add("it-card-image");
        }

        if (!string.IsNullOrWhiteSpace(CssClass))
        {
            cssClasses.Add(CssClass);
        }

        if (BorderTopColor.HasValue)
        {
            cssClasses.Add("it-card-border-top");
            var borderTopColorClass = BorderTopColor.Value switch
            {
                Color.Primary => "it-card-border-top-primary",
                Color.Secondary => "it-card-border-top-secondary",
                Color.Success => "it-card-border-top-success",
                Color.Warning => "it-card-border-top-warning",
                Color.Danger => "it-card-border-top-danger",
                _ => string.Empty
            };

            if (!string.IsNullOrEmpty(borderTopColorClass))
            {
                cssClasses.Add(borderTopColorClass);
            }
        }

        return string.Join(" ", cssClasses);
    }

    private void AddInlineClasses(List<string> cssClasses)
    {
        cssClasses.Add("it-card-inline");

        if (Mini)
        {
            cssClasses.Add("it-card-inline-mini");
        }
        if (Reverse)
        {
            cssClasses.Add("it-card-inline-reverse");
        }
    }
}
