using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a dropdown item that can be used within a <see cref="BitToolbar"/> component. 
/// It provides a way to include dropdown functionality in a toolbar, allowing users to select from a list of options or actions.
/// </summary>
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
}
