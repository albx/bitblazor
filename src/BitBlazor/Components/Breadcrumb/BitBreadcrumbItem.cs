namespace BitBlazor.Components.Breadcrumb;

/// <summary>
/// Represents an item in a <see cref="BitBreadcrumb"/> navigation component.
/// </summary>
public class BitBreadcrumbItem
{
    /// <summary>
    /// Gets or sets the text to display for the item.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the icon name to display before the item.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the URL to link to the item.
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// Gets a value indicating whether the item has an icon.
    /// </summary>
    /// <returns><code>true</code> if the item has an icon specified, <code>false</code> otherwise</returns>
    public bool HasIcon() => !string.IsNullOrEmpty(Icon);
}
