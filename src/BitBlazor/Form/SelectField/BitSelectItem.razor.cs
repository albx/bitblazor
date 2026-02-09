using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents an individual selectable option within a BitSelectField component.
/// </summary>
/// <remarks>
/// Use BitSelectItem as a child of BitSelectField to define selectable options. 
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
    /// Gets or sets whether the option is disabled
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

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
