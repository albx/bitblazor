using BitBlazor.Components.Dropdown;
using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a dropdown component that can be used to display a list of options or actions in a collapsible menu.
/// </summary>
public partial class BitDropdown : BitComponentBase, IDisposable
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
    /// Gets or sets the unique identifier for the dropdown activator button. 
    /// This ID is used to associate the button with the dropdown menu for accessibility purposes.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string ActivatorId { get; set; } = string.Empty;

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
    private bool disposedValue;

    private string AriaExpanded => state.IsOpen ? "true" : "false";

    private IDictionary<string, object> dropdownMenuAttributes = new Dictionary<string, object>();

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        state.StateChanged += OnDropdownStateChanged;
    }

    private void OnDropdownStateChanged(object? sender, DropdownState.DropdownStateChangedEventArgs e)
    {
        if (e.IsOpen)
        {
            dropdownMenuAttributes["data-popper-placement"] = Position switch
            {
                DropdownPosition.Up => "top-start",
                DropdownPosition.End => "right-start",
                DropdownPosition.Start => "left-start",
                _ => "bottom-start"
            };
        }
        else
        {
            dropdownMenuAttributes.Remove("data-popper-placement");
        }

        InvokeAsync(StateHasChanged);
    }

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

    #region IDisposable implementation
    /// <summary>
    /// Disposes of the resources used by the <see cref="BitDropdown"/> component. 
    /// This method is called when the component is no longer needed and is being removed from the UI.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                state.StateChanged -= OnDropdownStateChanged;
            }

            disposedValue = true;
        }
    }

    void IDisposable.Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
