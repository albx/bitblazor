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
    /// Toggles the open/closed state of the dropdown. If the dropdown is currently open, calling this method will close it, and vice versa.
    /// </summary>
    public void Toggle() => _isOpen = !_isOpen;
}
