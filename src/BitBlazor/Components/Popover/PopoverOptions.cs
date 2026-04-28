using System.Text.Json.Serialization;

namespace BitBlazor.Components;

/// <summary>
/// Strongly-typed options for the Bootstrap Italia Popover component.
/// Property names are serialized to camelCase to match the Bootstrap JS API.
/// </summary>
public class PopoverOptions
{
    /// <summary>Applies a CSS fade transition. Default: <c>true</c>.</summary>
    [JsonPropertyName("animation")]
    public bool Animation { get; set; } = true;

    /// <summary>
    /// Overflow constraint boundary of the popover.
    /// Default: <c>"clippingParents"</c>.
    /// </summary>
    [JsonPropertyName("boundary")]
    public string Boundary { get; set; } = "clippingParents";

    /// <summary>
    /// Appends the popover to a specific element, e.g. <c>"body"</c>.
    /// Default: <c>null</c> (appended after the trigger element).
    /// </summary>
    [JsonPropertyName("container")]
    public string? Container { get; set; }

    /// <summary>Extra CSS classes added to the popover when shown.</summary>
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
    /// Allow HTML in the popover title and content.
    /// <para><b>Warning:</b> only use with trusted / sanitised content.</para>
    /// </summary>
    [JsonPropertyName("html")]
    public bool Html { get; set; }

    /// <summary>
    /// Offset of the popover relative to its target, as <c>"skidding,distance"</c>.
    /// Default: <c>"0,0"</c>.
    /// </summary>
    [JsonPropertyName("offset")]
    public string Offset { get; set; } = "0,0";

    /// <summary>
    /// Popover placement: <c>auto</c>, <c>top</c>, <c>bottom</c>,
    /// <c>left</c>, <c>right</c>. Default: <c>"top"</c>.
    /// </summary>
    [JsonPropertyName("placement")]
    public string Placement { get; set; } = "top";

    /// <summary>Enable HTML sanitisation of title/content/template. Default: <c>true</c>.</summary>
    [JsonPropertyName("sanitize")]
    public bool Sanitize { get; set; } = true;

    /// <summary>
    /// CSS selector used for popover delegation on dynamically added elements.
    /// </summary>
    [JsonPropertyName("selector")]
    public string? Selector { get; set; }

    /// <summary>Custom HTML template used to create the popover.</summary>
    [JsonPropertyName("template")]
    public string? Template { get; set; }

    /// <summary>
    /// Events that trigger the popover: <c>click</c>, <c>hover</c>, <c>focus</c>,
    /// <c>manual</c> (space-separated). Default: <c>"click"</c>.
    /// </summary>
    [JsonPropertyName("trigger")]
    public string Trigger { get; set; } = "click";
}
