namespace BitBlazor.Components;

/// <summary>
/// Defines how a <see cref="BitTooltip"/> is triggered.
/// </summary>
public enum TooltipTrigger
{
    /// <summary>The tooltip is shown when the pointer hovers over the trigger element.</summary>
    Hover,

    /// <summary>The tooltip is shown when the trigger element receives focus.</summary>
    Focus,

    /// <summary>The tooltip is shown when the trigger element is clicked.</summary>
    Click,

    /// <summary>The tooltip is controlled programmatically via JavaScript methods.</summary>
    Manual
}
