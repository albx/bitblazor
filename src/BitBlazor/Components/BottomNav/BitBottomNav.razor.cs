using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary> 
/// BitBottomNav renders a bottom navigation component to display the current location in a mobile-optimized way.
/// The component supports customization of items, badges, alerts and accessibility features.
/// </summary>
public partial class BitBottomNav : BitComponentBase
{
    /// <summary>
    /// Gets or sets the items shown on the bottom navigation.
    /// </summary>
    [Parameter]
    public IReadOnlyList<BitBottomNavItem> Items { get; set; } = Enumerable.Empty<BitBottomNavItem>().ToList();

    private string ComputeCssClasses()
    {
        var builder = new CssClassBuilder("bottom-nav");

        AddCustomCssClass(builder);

        return builder.Build();
    }
}

