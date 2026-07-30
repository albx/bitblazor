using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a divider in a <see cref="BitToolbar"/> component.
/// </summary>
public partial class BitToolbarDivider
{
    [CascadingParameter]
    BitToolbar Parent { get; set; } = default!;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (Parent is null)
        {
            throw new InvalidOperationException("BitToolbarDivider component must be used inside a BitToolbar component");
        }
    }
}
