using Microsoft.AspNetCore.Components;

namespace BitBlazor.Core;

/// <summary>
/// Represents the base component from which inherit all the others
/// </summary>
public abstract class BitComponentBase : ComponentBase
{
    /// <summary>
    /// Gets or sets additional CSS classes to apply to the button.
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// Gets or sets the id of the element (see https://developer.mozilla.org/en-US/docs/Web/HTML/Reference/Global_attributes/id)
    /// </summary>
    [Parameter]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets additional attributes that do not match any of the explicitly defined parameters.
    /// </summary>
    /// <remarks>
    /// This property is typically used to capture arbitrary HTML attributes or other key-value pairs that are not explicitly defined in the component's parameters. 
    /// The keys represent attribute names, and the values represent their corresponding values.
    /// </remarks>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        SetElementId();
    }

    /// <summary>
    /// Sets the element id based on the value of the <see cref="Id"/> property
    /// </summary>
    protected virtual void SetElementId()
    {
        if (!string.IsNullOrWhiteSpace(Id))
        {
            AdditionalAttributes["id"] = Id;
        }
        else
        {
            AdditionalAttributes.Remove("id");
        }
    }

    /// <summary>
    /// Adds the value of the <see cref="CssClass"/> property to the specified <see cref="CssClassBuilder"/> instance.
    /// </summary>
    /// <param name="builder">The <see cref="CssClassBuilder"/> to which the custom CSS class will be added.</param>
    protected void AddCustomCssClass(CssClassBuilder builder)
    {
        if (!string.IsNullOrWhiteSpace(CssClass))
        {
            builder.Add(CssClass);
        }
    }
}
