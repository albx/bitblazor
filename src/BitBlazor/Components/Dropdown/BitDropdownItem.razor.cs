using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class BitDropdownItem
{
    [CascadingParameter]
    BitDropdown Parent { get; set; } = default!;

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (Parent is null)
        {
            throw new InvalidOperationException("BitDropdownItem component must be used inside a BitDropdown component");
        }
    }
}
