using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using System.Collections;
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

    private static readonly T? minDefaultValue = NumericHelpers<T>.GetDefaultMinValue();
    private static readonly T? maxDefaultValue = NumericHelpers<T>.GetDefaultMaxValue();
    private static readonly T? stepDefaultValue = NumericHelpers<T>.GetDefaultStepValue();

    private readonly Comparer comparer = new(CultureInfo.InvariantCulture);

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
    public T? Step { get; set; } = stepDefaultValue;

    /// <summary>
    /// Gets or sets the minimum allowable value for the parameter.
    /// </summary>
    /// <remarks>
    /// If set, input values less than <see cref="Min"/> may be considered invalid or rejected, depending on the component's validation logic. 
    /// The type parameter <typeparamref name="T"/> must support comparison operations for this property to be meaningful.
    /// </remarks>
    [Parameter]
    public T? Min { get; set; } = minDefaultValue;

    /// <summary>
    /// Gets or sets the maximum allowable value for the parameter.
    /// </summary>
    /// <remarks>
    /// If set, input values greater than <see cref="Max"/> may be considered invalid or rejected, depending on the component's validation logic.
    /// The type parameter <typeparamref name="T"/> must support comparison operations for this property to be meaningful.
    /// </remarks>
    [Parameter]
    public T? Max { get; set; } = maxDefaultValue;

    /// <summary>
    /// Gets or sets a value indicating whether the component should adapt its size depending on the value
    /// </summary>
    [Parameter]
    public bool Adaptive { get; set; }

    /// <summary>
    /// Gets or sets the content to render as the symbol within the component.
    /// </summary>
    /// <remarks>
    /// Use this property to provide custom markup or components that represent the symbol. 
    /// If not set, no symbol content will be rendered.
    /// </remarks>
    [Parameter]
    public RenderFragment? SymbolContent { get; set; }

    /// <summary>
    /// Gets or sets the text displayed on the increment button.
    /// </summary>
    [Parameter]
    public string IncrementButtonText { get; set; } = "Increase value";

    /// <summary>
    /// Gets or sets the text displayed on the decrement button.
    /// </summary>
    [Parameter]
    public string DecrementButtonText { get; set; } = "Decrease value";

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
        if (comparer.Compare(Min, minDefaultValue) != 0)
        {
            AdditionalAttributes["min"] = Min!;
        }
        else
        {
            AdditionalAttributes.Remove("min");
        }

        if (comparer.Compare(Max, maxDefaultValue) != 0)
        {
            AdditionalAttributes["max"] = Max!;
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

        var labelClass = SymbolContent is null ? "input-number-label" : "input-symbol-label";
        builder.Add(labelClass);

        return builder.Build();
    }

    private string ComputeInputGroupClasses()
    {
        var builder = new CssClassBuilder("input-group input-number");
        if (Adaptive)
        {
            builder.Add("input-number-adaptive");
        }

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
