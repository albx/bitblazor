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
    /// Gets or sets whether the alert is dismissible
    /// </summary>
    [Parameter]
    public bool Dismissible { get; set; }

    /// <summary>
    /// Gets or sets the aria-label attribute for the close button in case of dismissible alert
    /// </summary>
    [Parameter]
    public string? CloseButtonAriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the callback which will be called just before the closing of the alert
    /// </summary>
    [Parameter]
    public EventCallback OnClose { get; set; }

    /// <summary>
    /// Gets or sets the callback which will be called just after the alert has been closed
    /// </summary>
    [Parameter]
    public EventCallback OnClosed { get; set; }

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

    /// <summary>
    /// Sets default values for component parameters when they are not provided.
    /// </summary>
    /// <remarks>If the <see cref="CloseButtonAriaLabel"/> parameter is not set or is whitespace, it defaults to "close this alert" to ensure accessibility.</remarks>
    protected override void OnParametersSet()
    {
        if (Dismissible && string.IsNullOrWhiteSpace(CloseButtonAriaLabel))
        {
            CloseButtonAriaLabel = "close this alert";
        }
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

    private async Task CloseAsync()
    {
        await OnClose.InvokeAsync();

        dismissibleClasses.Remove("show");
        closed = true;

        await OnClosed.InvokeAsync();
    }
}
