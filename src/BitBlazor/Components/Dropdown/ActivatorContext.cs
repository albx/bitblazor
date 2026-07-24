namespace BitBlazor.Components;

/// <summary>
/// Represents the context for the activator button of a <see cref="BitDropdown"/> component.
/// </summary>
/// <param name="dropdown">The <see cref="BitDropdown"/> instance</param>
public sealed class ActivatorContext(BitDropdown dropdown)
{
    /// <summary>
    /// Gets the value for the id of the dropdown activator button. 
    /// This ID is used to associate the button with the dropdown menu for accessibility purposes.
    /// </summary>
    public string ActivatorId { get; internal set; } = string.Empty;

    /// <summary>
    /// Gets the label for the dropdown activator button. 
    /// This label is displayed on the button that triggers the dropdown menu.
    /// </summary>
    public string ActivatorLabel { get; internal set; } = string.Empty;

    /// <summary>
    /// Gets a dictionary of attributes to be applied to the dropdown activator button.
    /// </summary>
    public IDictionary<string, object> Attributes { get; } = new Dictionary<string, object>()
    {
        ["aria-haspopup"] = "true",
        ["aria-expanded"] = "false"
    };

    /// <summary>
    /// Toggles the visibility of the dropdown menu. 
    /// This method is called when the activator button is clicked, and it updates the state of the dropdown accordingly.
    /// </summary>
    public void ToggleDropdown() => dropdown.Toggle();
}
