using BitBlazor.Core;

namespace BitBlazor.Form;

/// <summary>
/// Represents a numeric input field component that supports various numeric types.
/// </summary>
/// <remarks>
/// This component is designed to handle numeric input fields and enforces type safety by restricting the generic type parameter <typeparamref name="T"/> to supported numeric types. 
/// It validates the type at runtime.
/// </remarks>
/// <typeparam name="T">
/// The type of the numeric value. Must be one of the supported types: 
/// <see cref="int"/>, <see cref="long"/>, <see cref="short"/>, <see cref="float"/>, <see cref="double"/>, or <see cref="decimal"/>.
/// </typeparam>
public partial class BitNumberField<T> : BitInputFieldBase<T>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "number";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [
        typeof(int), typeof(long), typeof(short), typeof(float), typeof(double), typeof(decimal),
    ];

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        UpdateLabelActiveState();
    }

    private void UpdateLabelActiveState()
    {
        var active = Value is not null || !string.IsNullOrWhiteSpace(Placeholder);
        SetLabelActiveState(active);
    }

    /// <inheritdoc/>
    protected override string ComputeLabelCssClass()
    {
        var builder = new CssClassBuilder(base.ComputeLabelCssClass());
        builder.Add("input-number-label");

        return builder.Build();
    }
}
