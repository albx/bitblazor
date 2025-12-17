using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents a checkbox form component that allows users to select or clear a boolean value within a form.
/// </summary>
/// <remarks>
/// Use this component to capture binary choices, such as yes/no or true/false, in form scenarios.
/// </remarks>
public partial class BitCheckbox : BitFormComponentBase<bool>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "check";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(bool)];

    /// <summary>
    /// Gets or sets a value indicating whether the component's content should be rendered inline.
    /// </summary>
    [Parameter]
    public bool Inline { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the checkbox is grouped.
    /// </summary>
    /// <remarks>
    /// Setting this value to true will align the checkbox to the right of the text content
    /// </remarks>
    [Parameter]
    public bool Grouped { get; set; }

    private string ComputeContainerCssClasses()
    {
        var builder = new CssClassBuilder("form-check");
        if (Inline)
        {
            builder.Add("form-check-inline");
        }

        if (Grouped)
        {
            builder.Add("form-check-group");
        }

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private string ComputeLabelCssClass()
    {
        var builder = new CssClassBuilder();
        if (Disabled)
        {
            builder.Add("disabled");
        }

        return builder.Build();
    }
}
