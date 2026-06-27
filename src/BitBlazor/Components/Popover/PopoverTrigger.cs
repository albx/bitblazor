namespace BitBlazor.Components;

/// <summary>
/// Defines how a <see cref="BitPopover"/> is triggered.
/// </summary>
public enum PopoverTrigger
{
    /// <summary>The popover is shown when the trigger element is clicked.</summary>
    Click,

    /// <summary>The popover is shown when the pointer hovers over the trigger element.</summary>
    Hover,

    /// <summary>The popover is shown when the trigger element receives focus.</summary>
    Focus,

    /// <summary>The popover is controlled programmatically via JavaScript methods.</summary>
    Manual
}
