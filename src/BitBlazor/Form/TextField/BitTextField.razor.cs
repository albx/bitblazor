using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

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

    /// <summary>
    /// Gets or sets whether the input component should be readonly
    /// </summary>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// Gets or sets whether the input should be rendered as plain-text
    /// </summary>
    [Parameter]
    public bool Plaintext { get; set; }

    private bool isLabelActive = false;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        UpdateLabelActiveState();
    }

    private void SetLabelActiveState(bool active) => isLabelActive = active;

    private void UpdateLabelActiveState()
    {
        var active = !string.IsNullOrEmpty(Value) || !string.IsNullOrWhiteSpace(Placeholder);
        SetLabelActiveState(active);
    }

    private string ComputeInputCssClass()
    {
        var builder = new CssClassBuilder();
        if (!Plaintext)
        {
            builder.Add("form-control");
        }
        else
        {
            builder.Add("form-control-plaintext");
        }

        return builder.Build();
    }

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
