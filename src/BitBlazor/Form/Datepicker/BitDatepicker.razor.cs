namespace BitBlazor.Form;

public partial class BitDatepicker<T> : BitInputFieldBase<T>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "date";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(DateTime), typeof(DateOnly)];
}
