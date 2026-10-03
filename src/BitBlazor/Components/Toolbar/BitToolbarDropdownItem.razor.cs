using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BitBlazor.Components;

/// <summary>
/// Represents a dropdown item that can be used within a <see cref="BitToolbar"/> component. 
/// It provides a way to include dropdown functionality in a toolbar, allowing users to select from a list of options or actions.
/// </summary>
/// <remarks>
/// Rendered with <see cref="BitDropdown"/>, which requires an interactive render mode (Server, WebAssembly, or Auto) to open and close. 
/// Under static SSR (no circuit/WASM runtime attached) the activator cannot be toggled, so the dropdown menu can never be opened.
/// </remarks>
public partial class BitToolbarDropdownItem : BitToolbarItemBase
{
    /// <summary>
    /// Gets or sets the content to be rendered inside the dropdown item.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the dropdown item. 
    /// This ID is used to associate the item with its corresponding dropdown menu for accessibility purposes.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Id { get; set; } = string.Empty;

    private string ComputeActivatorCssClass()
    {
        var builder = new CssClassBuilder("btn", "btn-dropdown", "dropdown-toggle");

        if (Disabled)
        {
            builder.Add("disabled");
        }

        return builder.Build();
    }

    // Merges the ActivatorContext's dropdown-managed attributes with the aria-disabled state set by SetDisabled().
    private IDictionary<string, object> ComputeActivatorAttributes(ActivatorContext context)
    {
        var attributes = new Dictionary<string, object>(context.Attributes);

        foreach (var attribute in AdditionalAttributes)
        {
            attributes[attribute.Key] = attribute.Value;
        }

        return attributes;
    }

    private void HandleActivatorClick(ActivatorContext context)
    {
        if (Disabled)
        {
            return;
        }

        context.ToggleDropdown();
    }

    private async Task HandleActivatorKeyDownAsync(ActivatorContext context, KeyboardEventArgs args)
    {
        if (Disabled)
        {
            return;
        }

        await context.HandleKeyDownAsync(args);
    }
}
