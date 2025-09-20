using BitBlazor.Core;
using System.ComponentModel.DataAnnotations;

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

    private void Increment() => ChangeValue(factor: 1);
    private void Decrement() => ChangeValue(factor: -1);

    #region Increment/Decrement helpers
    private void ChangeValue(int factor)
    {
        if (!ValueChangers.TryGetValue(ComponentType, out var valueChanger))
        {
            throw new NotSupportedException($"Type {ComponentType} is not supported");
        }

        Value = valueChanger(Value, factor);
    }

    private readonly static Dictionary<Type, Func<T?, int, T>> ValueChangers = new()
    {
        [typeof(int)] = (value, factor) =>
        {
            int intValue = value is null ? 0 : Convert.ToInt32(value);
            int newValue = intValue + factor * 1;

            return (T)(object)newValue;
        },
        [typeof(long)] = (value, factor) =>
        {
            long longValue = value is null ? 0 : Convert.ToInt64(value);
            long newValue = longValue + factor * 1;

            return (T)(object)newValue;
        },
        [typeof(short)] = (value, factor) =>
        {
            short shortValue = value is null ? (short)0 : Convert.ToInt16(value);
            short newValue = (short)(shortValue + factor * 1);

            return (T)(object)newValue;
        },
        [typeof(float)] = (value, factor) =>
        {
            float floatValue = value is null ? 0f : Convert.ToSingle(value);
            float newValue = floatValue + factor * 1f;

            return (T)(object)newValue;
        },
        [typeof(double)] = (value, factor) =>
        {
            double doubleValue = value is null ? 0 : Convert.ToDouble(value);
            double newValue = doubleValue + factor * 1;

            return (T)(object)newValue;
        },
        [typeof(decimal)] = (value, factor) =>
        {
            decimal decimalValue = value is null ? 0 : Convert.ToDecimal(value);
            decimal newValue = decimalValue + factor * 1;

            return (T)(object)newValue;
        }
    };
    #endregion
}
