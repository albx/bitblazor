namespace BitBlazor.Form;

/// <summary>
/// Represents a toggle switch component that allows users to select between two states, typically on and off.
/// </summary>
/// <remarks>
/// Use this component to capture boolean input from users in forms or interactive UI scenarios. 
/// The toggle displays a visual indicator reflecting its current state and can be bound to a boolean value in a data model.
/// </remarks>
public partial class BitToggle : BitFormComponentBase<bool>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "toggle";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(bool)];
}
