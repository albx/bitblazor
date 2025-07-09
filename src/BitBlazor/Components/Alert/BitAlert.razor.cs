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

    /// <summary>
    /// Gets or sets whether the alert is dimissible
    /// </summary>
    [Parameter]
    public bool Dismissible { get; set; }

    /// <summary>
    /// Gets or sets the aria-label attribute for the close button in case of dismissible alert
    /// </summary>
    [Parameter]
    public string? CloseButtonAriaLabel { get; set; }

    private string ComputedCssClasses => $"alert {ComputeCssClasses()}".Trim();

    private List<string> dismissibleClasses = [];

    private bool closed;

    /// <summary>
    /// Initializes the component.
    /// </summary>
    protected override void OnInitialized()
    {
        dismissibleClasses = ["alert-dismissible", "fade", "show"];
        closed = false;
    }

    private string ComputeCssClasses()
    {
        var cssClasses = new List<string>();
        AddTypeClass(cssClasses);

        if (Dismissible)
        {
            cssClasses.AddRange(dismissibleClasses);
        }

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

    private void Close()
    {
        if (!Dismissible)
        {
            throw new InvalidOperationException("Alert is not dismissible");
        }

        dismissibleClasses.Remove("show");
        closed = true;
    }
}
