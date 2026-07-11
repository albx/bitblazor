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

    /// <summary>
    /// Gets or sets the position of the dropdown menu relative to its toggle button. 
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="DropdownPosition.Down"/>, which positions the menu below the button.
    /// Other options include <see cref="DropdownPosition.Up"/>, <see cref="DropdownPosition.End"/>, and <see cref="DropdownPosition.Start"/>.
    /// </remarks>
    [Parameter]
    public DropdownPosition Position { get; set; } = DropdownPosition.Down;

    private readonly DropdownState state = new();

    private string AriaExpanded => state.IsOpen ? "true" : "false";

    private string ComputeDropdownContainerClass()
    {
        var builder = new CssClassBuilder("dropdown");
        AddPositionClass(builder);

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private void AddPositionClass(CssClassBuilder builder)
    {
        var positionClass = Position switch
        {
            DropdownPosition.Up => "dropup",
            DropdownPosition.End => "dropend",
            DropdownPosition.Start => "dropstart",
            _ => string.Empty
        };

        builder.Add(positionClass);
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
