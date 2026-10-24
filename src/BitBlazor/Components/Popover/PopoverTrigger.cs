namespace BitBlazor.Components;

/// <summary>
/// Defines how a <see cref="BitPopover"/> is triggered. Values can be combined
/// (for example <c>Hover | Focus</c>), except <see cref="Manual"/> which always wins.
/// </summary>
[Flags]
public enum PopoverTrigger
{
    /// <summary>The popover is shown when the trigger element is clicked.</summary>
    Click = 1,

    /// <summary>The popover is shown when the pointer hovers over the trigger element.</summary>
    Hover = 2,

    /// <summary>The popover is shown when the trigger element receives focus.</summary>
    Focus = 4,

    /// <summary>
    /// The popover is controlled programmatically via JavaScript methods.
    /// When set, any other flag is ignored.
    /// </summary>
    Manual = 8
}
