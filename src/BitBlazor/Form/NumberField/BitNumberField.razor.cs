using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using System.Globalization;

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
    private const string StepDefaultValue = "any";

    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "number";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [
        typeof(int), typeof(long), typeof(short), typeof(float), typeof(double), typeof(decimal),
    ];

    /// <summary>
    /// Gets or sets the increment value used when changing the parameter.
    /// </summary>
    /// <remarks>
    /// The value determines the amount by which the associated parameter increases or decreases with each step. 
    /// The type and meaning of the increment depend on the generic type <typeparamref name="T"/> and the context in which the parameter is used.
    /// </remarks>
    [Parameter]
    public T? Step { get; set; }

    /// <summary>
    /// Gets or sets the minimum allowable value for the parameter.
    /// </summary>
    /// <remarks>
    /// If set, input values less than <see cref="Min"/> may be considered invalid or rejected, depending on the component's validation logic. 
    /// The type parameter <typeparamref name="T"/> must support comparison operations for this property to be meaningful.
    /// </remarks>
    [Parameter]
    public T? Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum allowable value for the parameter.
    /// </summary>
    /// <remarks>
    /// If set, input values greater than <see cref="Max"/> may be considered invalid or rejected, depending on the component's validation logic.
    /// The type parameter <typeparamref name="T"/> must support comparison operations for this property to be meaningful.
    /// </remarks>
    [Parameter]
    public T? Max { get; set; }

    private string StepString => NumericHelpers<T>.FormatValue(Step) ?? StepDefaultValue;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetMinAndMaxAttributes();
        UpdateLabelActiveState();
    }

    private void SetMinAndMaxAttributes()
    {
        if (Min is not null)
        {
            AdditionalAttributes["min"] = Min;
        }
        else
        {
            AdditionalAttributes.Remove("min");
        }

        if (Max is not null)
        {
            AdditionalAttributes["max"] = Max;
        }
        else
        {
            AdditionalAttributes.Remove("max");
        }
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

    private void Increment()
    {
        Value = NumericHelpers<T>.ChangeValue(Value, Min, Max, Step, factor: 1);
    }

    private void Decrement()
    {
        Value = NumericHelpers<T>.ChangeValue(Value, Min, Max, Step, factor: -1);
    }
}
