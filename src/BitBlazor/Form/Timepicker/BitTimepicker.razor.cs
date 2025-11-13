
namespace BitBlazor.Form;

public partial class BitTimepicker<T> : BitInputFieldBase<T>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "time";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(TimeOnly)];
}
