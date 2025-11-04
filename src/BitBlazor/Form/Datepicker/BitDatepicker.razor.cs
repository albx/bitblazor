namespace BitBlazor.Form;

/// <summary>
/// Represents a date picker input component that allows users to select a date value of a specified type.
/// </summary>
/// <remarks>
/// Use this component to provide a user interface for date selection in forms or other data entry scenarios. 
/// The generic type parameter restricts the value to supported date types, ensuring type safety.
/// </remarks>
/// <typeparam name="T">
/// The type of the value to be selected. Supported types are <see cref="DateTime"/> and <see cref="DateOnly"/>.
/// </typeparam>
public partial class BitDatepicker<T> : BitInputFieldBase<T>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "date";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(DateTime), typeof(DateOnly)];
}
