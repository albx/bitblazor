using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents an item within a <see cref="BitDropdown"/> component. 
/// Each <see cref="BitDropdownItem"/> can contain custom content and is rendered as part of the dropdown menu.
/// </summary>
public partial class BitDropdownItem
{
    [CascadingParameter]
    BitDropdown Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content to be rendered inside the dropdown item.
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (Parent is null)
        {
            throw new InvalidOperationException("BitDropdownItem component must be used inside a BitDropdown component");
        }
    }
}
