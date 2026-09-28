using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the base class for toolbar items that can be used within a <see cref="BitToolbar"/> component.
/// </summary>
public abstract class BitToolbarItemBase : ComponentBase
{
    [CascadingParameter]
    protected BitToolbar Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the label for the toolbar item.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the icon to be displayed for the toolbar item.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string IconName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the toolbar item is active. When set to true, the item will be styled as active.
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets whether the toolbar item is disabled. When set to true, the item will be styled as disabled and will not respond to user interactions.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the count to be displayed as a badge on the toolbar item. If set, a badge will be shown with the specified count.
    /// </summary>
    [Parameter]
    public int? BadgeCount { get; set; }

    /// <summary>
    /// Gets or sets the label for the badge on the toolbar item. This label can provide additional context for the badge count.
    /// </summary>
    [Parameter]
    public string? BadgeLabel { get; set; }

    /// <summary>
    /// Gets or sets additional attributes that do not match any of the explicitly defined parameters.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// Gets a value indicating whether the toolbar item has a badge count greater than zero. This is used to determine if a badge should be displayed.
    /// </summary>
    protected bool HasBadgeNumber => BadgeCount.HasValue && BadgeCount.Value > 0;

    /// <summary>
    /// Gets a value indicating whether the toolbar item has a non-empty badge label. This is used to determine if a badge label should be displayed.
    /// </summary>
    protected bool HasBadgeLabel => !string.IsNullOrWhiteSpace(BadgeLabel);

    /// <summary>
    /// Gets a value indicating whether the toolbar item has either a badge count or a badge label. This is used to determine if any badge should be displayed.
    /// </summary>
    protected bool HasBadge => HasBadgeNumber || HasBadgeLabel;

    /// <summary>
    /// Gets a value indicating whether the parent toolbar's size is set to the default size. This is used to apply specific styling based on the toolbar's size.
    /// </summary>
    protected bool IsToolbarSizeDefault => Parent.Size is ToolbarSize.Default;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (Parent is null)
        {
            throw new InvalidOperationException($"This component must be used inside a BitToolbar component");
        }
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        SetDisabled();
    }

    /// <summary>
    /// Sets the "aria-disabled" attribute based on the Disabled parameter.
    /// </summary>
    protected virtual void SetDisabled()
    {
        if (Disabled)
        {
            AdditionalAttributes["aria-disabled"] = "true";
        }
        else
        {
            AdditionalAttributes.Remove("aria-disabled");
        }
    }

    /// <summary>
    /// Computes the CSS classes for the label based on the parent toolbar's size.
    /// </summary>
    /// <returns>The computed CSS classes</returns>
    protected virtual string ComputeLabelCssClass()
    {
        var builder = new CssClassBuilder();

        var labelClass = Parent.Size switch
        {
            ToolbarSize.Medium or ToolbarSize.Small => "visually-hidden",
            _ => "toolbar-label"
        };

        builder.Add(labelClass);

        return builder.Build();
    }

    protected RenderFragment RenderBadgeLabel() => 
        builder =>
        {
            builder.OpenElement(0, "span");
            builder.AddAttribute(1, "class", "visually-hidden");
            builder.AddContent(2, BadgeLabel);
            builder.CloseElement();
        };
}
