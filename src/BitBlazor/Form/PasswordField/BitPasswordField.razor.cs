using BitBlazor.Core;

namespace BitBlazor.Form;

/// <summary>
/// Represents a password input field component.
/// </summary>
/// <remarks>
/// This component is designed to handle password input scenarios, providing built-in support for binding and validation of string values. 
/// It is optimized for use in forms and other data entry contexts.
/// </remarks>
public partial class BitPasswordField : BitInputFieldBase<string?>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "pwd";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(string)];

    private bool isPasswordVisible = false;

    private string PasswordFieldType => isPasswordVisible ? "text" : "password";

    private string ButtonAriaChecked => isPasswordVisible ? "true" : "false";

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

    /// <inheritdoc/>
    protected override string ComputeInputCssClass()
    {
        var builder = new CssClassBuilder(base.ComputeInputCssClass());
        builder.Add("input-password");

        return builder.Build();
    }

    private void TogglePasswordVisibility() => isPasswordVisible = !isPasswordVisible;

    private string ComputePasswordVisibleIconClass()
    {
        var builder = new CssClassBuilder("password-icon-visible");
        if (isPasswordVisible)
        {
            builder.Add("d-none");
        }

        return builder.Build();
    }

    private string ComputePasswordInvisibleIconClass()
    {
        var builder = new CssClassBuilder("password-icon-invisible");
        if (!isPasswordVisible)
        {
            builder.Add("d-none");
        }

        return builder.Build();
    }
}
