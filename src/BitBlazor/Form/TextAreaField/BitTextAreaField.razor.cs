using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents a text area input field component that supports binding to a nullable string value.
/// </summary>
/// <remarks>
/// This component is designed to handle user input for multi-line text fields. 
/// It supports binding to a nullable string value and provides additional functionality such as placeholder text and label activation based on the input state.
/// </remarks>
public partial class BitTextAreaField : BitInputFieldBase<string?>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "textarea";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(string)];

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        UpdateLabelActiveState();
    }

    private void UpdateLabelActiveState()
    {
        var active = !string.IsNullOrEmpty(Value) || !string.IsNullOrWhiteSpace(Placeholder);
        SetLabelActiveState(active);
    }

    /// <summary>
    /// Gets or sets the number of rows to display.
    /// </summary>
    /// <remarks>
    /// This property determines the vertical size of the component by specifying the number of rows.
    /// </remarks>
    [Parameter]
    public int Rows { get; set; } = 1;
}
