using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BitBlazor.Components;

/// <summary>
/// Represents a toolbar item component that can be used within a <see cref="BitToolbar"/> component.
/// </summary>
public partial class BitToolbarItem
{
    [CascadingParameter]
    BitToolbar Parent { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    /// <summary>
    /// Gets or sets the label for the toolbar item.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the icon to be displayed for the toolbar item.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string IconName { get; set; } = string.Empty;

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
    /// Gets or sets a value indicating whether the toolbar item is active. When set to true, the item will be styled as active.
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets whether the toolbar item is disabled. When set to true, the item will be styled as disabled and will not respond to user interactions.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the count to be displayed as a badge on the toolbar item. If set, a badge will be shown with the specified count.
    /// </summary>
    [Parameter]
    public int? BadgeCount { get; set; }

    /// <summary>
    /// Gets or sets the label for the badge on the toolbar item. This label can provide additional context for the badge count.
    /// </summary>
    [Parameter]
    public string? BadgeLabel { get; set; }

    /// <summary>
    /// Gets or sets the primary interactive callback, invoked when the toolbar item is clicked.
    /// When set, it takes precedence over <see cref="Href"/> navigation in interactive rendering.
    /// Not invoked during static (SSR) rendering — provide <see cref="Href"/> as a navigation fallback for SSR contexts.
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Gets or sets additional attributes that do not match any of the explicitly defined parameters.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

    private bool HasBadgeNumber => BadgeCount.HasValue && BadgeCount.Value > 0;

    private bool HasBadgeLabel => !string.IsNullOrWhiteSpace(BadgeLabel);

    private bool HasBadge => HasBadgeNumber || HasBadgeLabel;

    private bool IsToolbarSizeDefault => Parent.Size is ToolbarSize.Default;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (Parent is null)
        {
            throw new InvalidOperationException("BitToolbarItem component must be used inside a BitToolbar component");
        }
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        SetDisabled();
    }

    private void SetDisabled()
    {
        if (Disabled)
        {
            AdditionalAttributes["aria-disabled"] = "true";
        }
        else
        {
            AdditionalAttributes.Remove("aria-disabled");
        }
    }

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

    private string ComputeLabelCssClass()
    {
        var builder = new CssClassBuilder();
        
        var labelClass = Parent.Size switch
        {
            ToolbarSize.Medium or ToolbarSize.Small => "visually-hidden",
            _ => "toolbar-label"
        };
        builder.Add(labelClass);


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
