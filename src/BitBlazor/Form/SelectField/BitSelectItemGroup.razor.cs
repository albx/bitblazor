using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents a group of selectable items within a BitSelect component.
/// </summary>
/// <remarks>
/// Use BitSelectItemGroup to organize related BitSelectItem components under a common label within a BitSelect field. 
/// This enhances accessibility and user experience by grouping options logically. 
/// The group label is typically rendered as an optgroup label in the resulting markup.
/// </remarks>
public partial class BitSelectItemGroup
{
    [CascadingParameter]
    IBitSelectField Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the text label displayed for the component.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Gets or sets additional attributes that do not match any of the explicitly defined parameters.
    /// </summary>
    /// <remarks>
    /// This property is typically used to capture arbitrary HTML attributes or other key-value pairs that are not explicitly defined in the component's parameters. 
    /// The keys represent attribute names, and the values represent their corresponding values.
    /// </remarks>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();
}
