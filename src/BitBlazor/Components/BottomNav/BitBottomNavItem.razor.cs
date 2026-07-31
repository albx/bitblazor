using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents an item in a <see cref="BitBottomNav"/> navigation component.
/// </summary>
public partial class BitBottomNavItem
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
    /// Gets or sets the URL that the toolbar item should link to.
    /// In SSR rendering, the browser follows this URL directly on click.
    /// In interactive rendering, this URL is used as a navigation fallback when <see cref="OnClick"/> has no delegate,
    /// and as a secondary browser behavior target (right-click, Ctrl+Click) when <see cref="OnClick"/> is set.
    /// When both <see cref="Href"/> and <see cref="OnClick"/> are set, <see cref="OnClick"/> takes precedence for primary interaction.
    /// </summary>
    public string? Href { get; set; }

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
    /// Gets or sets the primary interactive callback, invoked when the toolbar item is clicked.
    /// When set, it takes precedence over <see cref="Href"/> navigation in interactive rendering.
    /// Not invoked during static (SSR) rendering — provide <see cref="Href"/> as a navigation fallback for SSR contexts.
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

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
