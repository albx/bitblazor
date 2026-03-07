using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class BitPageItem
{
    [CascadingParameter]
    BitPagination Parent { get; set; } = default!;

    [Parameter]
    public int Page { get; set; }

    private IDictionary<string, object> attributes = new Dictionary<string, object>();

    protected override void OnParametersSet()
    {
        if (Page == Parent.Page)
        {
            attributes["aria-current"] = "page";
        }
        else
        {
            attributes.Remove("aria-current");
        }
    }
}
