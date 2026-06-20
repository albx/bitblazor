using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a toolbar item component that can be used within a <see cref="BitToolbar"/> component.
/// </summary>
public partial class BitToolbarItem
{
    [CascadingParameter]
    BitToolbar Parent { get; set; } = default!;

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

    private IDictionary<string, object> attributes = new Dictionary<string, object>();

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        SetDisabled();
    }

    private void SetDisabled()
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

    private string ComputeLinkCssClass()
    {
        var builder = new CssClassBuilder();
        
        if (Active)
        {
            builder.Add("active");
        }

        if (Disabled)
        {
            builder.Add("disabled");
        }

        return builder.Build();
    }

    private string ComputeLabelCssClass()
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
}
