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
    /// Gets or sets whether the badge is rounded
    /// </summary>
    [Parameter]
    public bool Rounded { get; set; }

    private string ComputedCssClasses => $"badge {ComputeCssClasses()}".Trim();

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();
        AddBackgroundColorClass(cssClasses);

        if (Rounded)
        {
            cssClasses.Add("rounded-pill");
        }

        return string.Join(" ", cssClasses);
    }

    private void AddBackgroundColorClass(List<string> cssClasses)
    {
        var backgroundColorCssClass = BackgroundColor switch
        {
            Color.Primary => "bg-primary",
            Color.Secondary => "bg-secondary",
            Color.Success => "bg-success",
            Color.Warning => "bg-warning",
            Color.Danger => "bg-danger",
            _ => string.Empty
        };

        cssClasses.Add(backgroundColorCssClass);
    }
}
