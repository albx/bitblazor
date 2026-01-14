namespace BitBlazor.Form;

/// <summary>
/// Represents a time picker input component that allows users to select a time value.
/// </summary>
/// <remarks>
/// The BitTimepicker component supports binding to values of type TimeOnly. 
/// It provides validation, formatting, and user interaction consistent with other BitInputFieldBase components. 
/// </remarks>
public partial class BitTimepicker : BitInputFieldBase<TimeOnly?>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "time";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(TimeOnly)];
}
