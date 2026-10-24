using System.Text.Json.Serialization;

namespace BitBlazor.Components;

/// <summary>
/// Options payload sent to the Bootstrap Italia Popover JS API.
/// Property names are serialized to camelCase to match the Bootstrap JS API.
/// </summary>
internal class PopoverOptions
{
    /// <summary>Applies a CSS fade transition. Default: <c>true</c>.</summary>
    [JsonPropertyName("animation")]
    public bool Animation { get; set; } = true;

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
    /// Events that trigger the popover: <c>click</c>, <c>hover</c>, <c>focus</c>,
    /// <c>manual</c> (space-separated). Default: <c>"click"</c>.
    /// </summary>
    [JsonPropertyName("trigger")]
    public string Trigger { get; set; } = "click";
}
