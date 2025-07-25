using BitBlazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a button component using Bootstrap Italia styles.
/// </summary>
public partial class BitButton
{
    /// <summary>
    /// Gets or sets the content of the button
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the color style of the button.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public Color Color { get; set; }

    /// <summary>
    /// Gets or sets the variant of the button
    /// </summary>
    [Parameter]
    public Variant Variant { get; set; } = Variant.Solid;

    /// <summary>
    /// Gets or sets the type of the button (Button, Submit, or Reset).
    /// </summary>
    [Parameter]
    public ButtonType Type { get; set; } = ButtonType.Button;

    /// <summary>
    /// Gets or sets the size of the button.
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.Default;

    /// <summary>
    /// Gets or sets a value indicating whether the button is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the icon name to display in the button.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the icon should be rounded.
    /// </summary>
    [Parameter]
    public bool IconRounded { get; set; }

    /// <summary>
    /// Gets or sets the position of the icon in the button.
    /// </summary>
    [Parameter]
    public IconPosition IconPosition { get; set; } = IconPosition.Start;

    /// <summary>
    /// Gets or sets the callback to invoke when the button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Gets or sets additional CSS classes to apply to the button.
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    private string ComputedCssClasses => $"btn {ComputeCssClasses()}".Trim();

    private Dictionary<string, object> attributes = new();

    private string ButtonTypeString => Type switch
    {
        ButtonType.Submit => "submit",
        ButtonType.Reset => "reset",
        _ => "button"
    };

    /// <summary>
    /// Invoked when the component's parameters are set. Updates the accessibility attributes based on the component's state.
    /// </summary>
    /// <remarks>
    /// This method ensures that the "aria-disabled" attribute is added or removed from the component's attributes dictionary based on the value of the <see cref="Disabled"/> property. 
    /// If <see cref="Disabled"/> is <see langword="true"/>, the "aria-disabled" attribute is set to "true", otherwise the attribute is removed.
    /// </remarks>
    protected override void OnParametersSet()
    {
        if (Disabled)
        {
            attributes["aria-disabled"] = "true";
        }
        else
        {
            attributes.Remove("aria-disabled");
        }
    }

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();
        AddColorClass(cssClasses);
        AddSizeClass(cssClasses);

        if (Disabled)
        {
            cssClasses.Add("disabled");
        }

        if (!string.IsNullOrWhiteSpace(Icon))
        {
            cssClasses.Add("btn-icon");
        }

        if (!string.IsNullOrWhiteSpace(CssClass))
        {
            cssClasses.Add(CssClass);
        }

        return string.Join(" ", cssClasses);
    }

    private void AddColorClass(List<string> cssClasses)
    {
        var colorClass = (Color, Variant) switch
        {
            (Color.Primary, Variant.Solid) => "btn-primary",
            (Color.Secondary, Variant.Solid) => "btn-secondary",
            (Color.Success, Variant.Solid) => "btn-success",
            (Color.Danger, Variant.Solid) => "btn-danger",
            (Color.Warning, Variant.Solid) => "btn-warning",
            (Color.Primary, Variant.Outline) => "btn-outline-primary",
            (Color.Secondary, Variant.Outline) => "btn-outline-secondary",
            (Color.Success, Variant.Outline) => "btn-outline-success",
            (Color.Danger, Variant.Outline) => "btn-outline-danger",
            (Color.Warning, Variant.Outline) => "btn-outline-warning",
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

    private bool HasIcon(IconPosition position)
    {
        return !string.IsNullOrWhiteSpace(Icon) && IconPosition == position;
    }

    private IconColor ComputeIconColor()
    {
        if (IconRounded || Variant is Variant.Outline)
        {
            return Color switch
            {
                Color.Primary => IconColor.Primary,
                Color.Secondary => IconColor.Secondary,
                Color.Warning => IconColor.Warning,
                Color.Danger => IconColor.Danger,
                Color.Success => IconColor.Success,
                _ => IconColor.White
            };
        }

        return IconColor.White;
    }
}
