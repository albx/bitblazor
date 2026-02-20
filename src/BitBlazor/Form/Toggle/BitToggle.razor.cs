using BitBlazor.Core;
using BitBlazor.Form.Toggle;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents a toggle switch component that allows users to select between two states, typically on and off.
/// </summary>
/// <remarks>
/// Use this component to capture boolean input from users in forms or interactive UI scenarios. 
/// The toggle displays a visual indicator reflecting its current state and can be bound to a boolean value in a data model.
/// </remarks>
public partial class BitToggle : BitFormComponentBase<bool>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "toggle";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(bool)];

    /// <summary>
    /// Gets or sets the display mode used to render the toggle component.
    /// </summary>
    /// <remarks>
    /// Use this property to control whether the toggle is rendered inline or grouped, with the control aligned to the right of the label. 
    /// The default value is <see cref="ToggleViewMode.Inline"/>.
    /// </remarks>
    [Parameter]
    public ToggleViewMode ViewMode { get; set; } = ToggleViewMode.Inline;

    private string ComputeContainerCssClass()
    {
        var builder = new CssClassBuilder("form-check");

        var viewModeClass = ViewMode switch
        {
            ToggleViewMode.Grouped => "form-check-group",
            _ => "form-check-inline"
        };

        builder.Add(viewModeClass);

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private string ComputeInputCssClass()
    {
        var builder = new CssClassBuilder();
        AddValidationCssClass(builder);
        return builder.Build();
    }
}
