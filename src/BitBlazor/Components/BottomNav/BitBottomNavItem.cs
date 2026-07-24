namespace BitBlazor.Components;

/// <summary>
/// Represents an item in a <see cref="BitBottomNav"/> navigation component.
/// </summary>
public class BitBottomNavItem
{
    /// <summary>
    /// Gets or sets the text to display for the item.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the icon name to display before the item.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the URL to link to the item.
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the badge text.
    /// </summary>
    public string? BadgeText { get; set; }

    /// <summary>
    /// Gets or sets the ARIA label.
    /// </summary>
    public string? BadgeAriaLabel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item has an alert to display.
    /// </summary>
    public bool IsAlertActive { get; set; }

    /// <summary>
    /// Gets a value indicating whether the item has a badge to display.
    /// </summary>
    public bool HasBadge() => !string.IsNullOrEmpty(BadgeText);

    /// <summary>
    /// Gets a value indicating whether the item's badge has an ARIA label.
    /// </summary>
    public bool HasAriaLabel() => (HasBadge() || IsAlertActive) && !string.IsNullOrEmpty(BadgeAriaLabel);

    /// <summary>
    /// Gets a value indicating whether the item has an icon.
    /// </summary>
    /// <returns><code>true</code> if the item has an icon specified, <code>false</code> otherwise</returns>
    public bool HasIcon() => !string.IsNullOrEmpty(Icon);

}