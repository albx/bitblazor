using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents an alert component using Bootstrap Italia styles.
/// </summary>
public partial class BitAlert
{
    /// <summary>
    /// Gets or sets the type of the alert
    /// </summary>
    [Parameter]
    [EditorRequired]
    public AlertType Type { get; set; }

    /// <summary>
    /// Gets or sets the child content of the component
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the alert's title
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    private string ComputedCssClasses => $"alert {ComputeCssClasses()}".Trim();

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();
        AddTypeClass(cssClasses);

        return string.Join(" ", cssClasses);
    }

    private void AddTypeClass(List<string> cssClasses)
    {
        var typeClass = Type switch
        {
            AlertType.Primary => "alert-primary",
            AlertType.Info => "alert-info",
            AlertType.Success => "alert-success",
            AlertType.Warning => "alert-warning",
            AlertType.Danger => "alert-danger",
            _ => string.Empty
        };

        cssClasses.Add(typeClass);
    }
}
