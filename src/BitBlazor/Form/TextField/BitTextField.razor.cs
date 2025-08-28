using BitBlazor.Core;

namespace BitBlazor.Form;

/// <summary>
/// Represents a text input field component.
/// </summary>
/// <remarks>
/// The <see cref="BitTextField"/> component is designed to handle string input values and provides built-in support for form integration and validation.
/// It is a part of the BitFormComponentBase framework, which facilitates consistent handling of form fields.
/// </remarks>
public partial class BitTextField : BitFormComponentBase<string?>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "text";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(string)];

    private bool isLabelActive = false;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetInitialLabelState();
    }

    private void SetInitialLabelState()
    {
        isLabelActive = !string.IsNullOrEmpty(Value) || !string.IsNullOrWhiteSpace(Placeholder);
    }

    private void SetLabelAsActive(bool active) => isLabelActive = active;

    private string ComputeLabelCssClass()
    {
        var builder = new CssClassBuilder();
        if (isLabelActive)
        {
            builder.Add("active");
        }

        return builder.Build();
    }
}
