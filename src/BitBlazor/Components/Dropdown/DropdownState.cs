namespace BitBlazor.Components.Dropdown;

/// <summary>
/// Represents the state of a dropdown component, including whether it is open or closed.
/// </summary>
public class DropdownState
{
    private bool _isOpen;

    /// <summary>
    /// Gets a value indicating whether the dropdown is currently open. If true, the dropdown is open; otherwise, it is closed.
    /// </summary>
    public bool IsOpen => _isOpen;

    /// <summary>
    /// Notifies subscribers when the open/closed state of the dropdown changes. 
    /// The event provides information about the new state through the <see cref="DropdownStateChangedEventArgs"/> class.
    /// </summary>
    public event EventHandler<DropdownStateChangedEventArgs>? StateChanged;

    /// <summary>
    /// Toggles the open/closed state of the dropdown. If the dropdown is currently open, calling this method will close it, and vice versa.
    /// </summary>
    public void Toggle()
    {
        _isOpen = !_isOpen;
        StateChanged?.Invoke(this, new DropdownStateChangedEventArgs(_isOpen));
    }

    /// <summary>
    /// Defines the event arguments for the <see cref="DropdownState.StateChanged"/> event, providing information about the new open/closed state of the dropdown.
    /// </summary>
    /// <param name="IsOpen">Whether the dropodown is open or not</param>
    public record struct DropdownStateChangedEventArgs(bool IsOpen);
}
