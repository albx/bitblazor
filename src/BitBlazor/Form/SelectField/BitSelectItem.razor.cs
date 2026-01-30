using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents an individual selectable option within a BitSelectField component.
/// </summary>
/// <remarks>
/// Use BitSelectItem as a child of BitSelectFieldto define selectable options. 
/// The Value property specifies the underlying value for the option, and ChildContent defines the display content shown to the user.
/// </remarks>
/// <typeparam name="TValue">The type of the value associated with the select item.</typeparam>
public partial class BitSelectItem<TValue>
{
    [CascadingParameter]
    BitSelectField<TValue> Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the value of the option.
    /// </summary>
    [Parameter]
    public TValue? Value { get; set; }

    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets a collection of additional attributes to be applied to the component's rendered HTML element.
    /// </summary>
    /// <remarks>
    /// Attributes in this dictionary are rendered as HTML attributes on the component's root element. 
    /// This allows you to specify custom attributes such as data-* or aria-* values that are not explicitly defined by the component.
    /// </remarks>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();
}
