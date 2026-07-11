using BitBlazor.Components.Dropdown;
using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a dropdown component that can be used to display a list of options or actions in a collapsible menu.
/// </summary>
public partial class BitDropdown : BitComponentBase
{
    /// <summary>
    /// Gets or sets the content to be rendered inside the dropdown. This content can include a list of <see cref="BitDropdownItem"/> components.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the label for the dropdown activator button. This label is displayed on the button that triggers the dropdown menu.
    /// </summary>
    [Parameter]
    public string ActivatorLabel { get; set; } = string.Empty;

    private DropdownState state = new();

    private string AriaExpanded => state.IsOpen ? "true" : "false";

    private string ComputeDropdownContainerClass()
    {
        var builder = new CssClassBuilder("dropdown");

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private string ComputeDropdownMenuClass()
    {
        var builder = new CssClassBuilder("dropdown-menu");
        if (state.IsOpen)
        {
            builder.Add("show");
        }

        return builder.Build();
    }
}
