using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BitBlazor.Components;

/// <summary>
/// Represents a toolbar item component that can be used within a <see cref="BitToolbar"/> component.
/// </summary>
public partial class BitToolbarItem : BitToolbarItemBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    /// <summary>
    /// Gets or sets the URL that the toolbar item should link to.
    /// In SSR rendering, the browser follows this URL directly on click.
    /// In interactive rendering, this URL is used as a navigation fallback when <see cref="OnClick"/> has no delegate,
    /// and as a secondary browser behavior target (right-click, Ctrl+Click) when <see cref="OnClick"/> is set.
    /// When both <see cref="Href"/> and <see cref="OnClick"/> are set, <see cref="OnClick"/> takes precedence for primary interaction.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Gets or sets the primary interactive callback, invoked when the toolbar item is clicked.
    /// When set, it takes precedence over <see cref="Href"/> navigation in interactive rendering.
    /// Not invoked during static (SSR) rendering — provide <see cref="Href"/> as a navigation fallback for SSR contexts.
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    private string ComputeLinkCssClass()
    {
        var builder = new CssClassBuilder();

        if (Active)
        {
            builder.Add("active");
        }

        if (Disabled)
        {
            builder.Add("disabled");
        }

        return builder.Build();
    }

    private async Task ClickAsync()
    {
        if (Disabled)
            return;

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
        else if (Href is not null)
        {
            NavigationManager.NavigateTo(Href);
        }
    }

    private async Task OnKeyDownAsync(KeyboardEventArgs args)
    {
        if (args.Key is "Enter" or " ")
        {
            await ClickAsync();
        }
    }
}
