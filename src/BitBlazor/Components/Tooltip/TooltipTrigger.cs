namespace BitBlazor.Components;

/// <summary>
/// Defines how a <see cref="BitTooltip"/> is triggered. Values can be combined
/// (for example <c>Hover | Focus</c>), except <see cref="Manual"/> which always wins.
/// </summary>
[Flags]
public enum TooltipTrigger
{
    /// <summary>The tooltip is shown when the pointer hovers over the trigger element.</summary>
    Hover = 1,

    /// <summary>The tooltip is shown when the trigger element receives focus.</summary>
    Focus = 2,

    /// <summary>The tooltip is shown when the trigger element is clicked.</summary>
    Click = 4,

    /// <summary>
    /// The tooltip is controlled programmatically via JavaScript methods.
    /// When set, any other flag is ignored.
    /// </summary>
    Manual = 8
}
