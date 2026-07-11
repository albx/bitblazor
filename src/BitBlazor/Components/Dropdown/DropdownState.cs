namespace BitBlazor.Components.Dropdown;

public class DropdownState
{
    private bool _isOpen;

    public bool IsOpen => _isOpen;

    public void Toggle() => _isOpen = !_isOpen;
}
