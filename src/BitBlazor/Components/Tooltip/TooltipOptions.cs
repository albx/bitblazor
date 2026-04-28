using System.Text.Json.Serialization;

namespace BitBlazor.Components;

/// <summary>
/// Strongly-typed options for the Bootstrap Italia Tooltip component.
/// Property names are serialized to camelCase to match the Bootstrap JS API.
/// </summary>
public class TooltipOptions
{
    /// <summary>Applies a CSS fade transition. Default: <c>true</c>.</summary>
    [JsonPropertyName("animation")]
    public bool Animation { get; set; } = true;

    /// <summary>
    /// Overflow constraint boundary of the tooltip.
    /// Default: <c>"clippingParents"</c>.
    /// </summary>
    [JsonPropertyName("boundary")]
    public string Boundary { get; set; } = "clippingParents";

    /// <summary>
    /// Appends the tooltip to a specific element, e.g. <c>"body"</c>.
    /// Default: <c>null</c> (appended after the trigger element).
    /// </summary>
    [JsonPropertyName("container")]
    public string? Container { get; set; }

    /// <summary>Extra CSS classes added to the tooltip when shown.</summary>
    [JsonPropertyName("customClass")]
    public string? CustomClass { get; set; }

    /// <summary>Show/hide delay in milliseconds.</summary>
    [JsonPropertyName("delay")]
    public TooltipDelay Delay { get; set; } = new();

    /// <summary>
    /// Fallback placements tried when the preferred one overflows.
    /// Default: <c>["top","right","bottom","left"]</c>.
    /// </summary>
    [JsonPropertyName("fallbackPlacements")]
    public IList<string> FallbackPlacements { get; set; } =
        ["top", "right", "bottom", "left"];

    /// <summary>
    /// Allow HTML in the tooltip title.
    /// <para><b>Warning:</b> only use with trusted / sanitised content.</para>
    /// </summary>
    [JsonPropertyName("html")]
    public bool Html { get; set; }

    /// <summary>
    /// Offset of the tooltip relative to its target, as <c>"skidding,distance"</c>.
    /// Default: <c>"0,0"</c>.
    /// </summary>
    [JsonPropertyName("offset")]
    public string Offset { get; set; } = "0,0";

    /// <summary>
    /// Tooltip placement: <c>auto</c>, <c>top</c>, <c>bottom</c>,
    /// <c>left</c>, <c>right</c>. Default: <c>"top"</c>.
    /// </summary>
    [JsonPropertyName("placement")]
    public string Placement { get; set; } = "top";

    /// <summary>Enable HTML sanitisation of title/template. Default: <c>true</c>.</summary>
    [JsonPropertyName("sanitize")]
    public bool Sanitize { get; set; } = true;

    /// <summary>
    /// CSS selector used for tooltip delegation on dynamically added elements.
    /// </summary>
    [JsonPropertyName("selector")]
    public string? Selector { get; set; }

    /// <summary>Custom HTML template used to create the tooltip.</summary>
    [JsonPropertyName("template")]
    public string? Template { get; set; }

    /// <summary>
    /// Events that trigger the tooltip: <c>click</c>, <c>hover</c>, <c>focus</c>,
    /// <c>manual</c> (space-separated). Default: <c>"hover focus"</c>.
    /// </summary>
    [JsonPropertyName("trigger")]
    public string Trigger { get; set; } = "hover focus";
}

/// <summary>Show/hide delay configuration for a tooltip.</summary>
public class TooltipDelay
{
    /// <summary>Delay before showing the tooltip (ms). Default: <c>0</c>.</summary>
    [JsonPropertyName("show")]
    public int Show { get; set; } = 0;

    /// <summary>Delay before hiding the tooltip (ms). Default: <c>0</c>.</summary>
    [JsonPropertyName("hide")]
    public int Hide { get; set; } = 0;
}
