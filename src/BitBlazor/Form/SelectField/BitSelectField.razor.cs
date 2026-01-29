namespace BitBlazor.Form;

public partial class BitSelectField<T> : BitFormComponentBase<T>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "select";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [];
}
