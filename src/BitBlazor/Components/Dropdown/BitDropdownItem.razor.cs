using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BitBlazor.Components;

/// <summary>
/// Represents an item within a <see cref="BitDropdown"/> component. 
/// Each <see cref="BitDropdownItem"/> can contain custom content and is rendered as part of the dropdown menu.
/// </summary>
public partial class BitDropdownItem : IDisposable
{
    private ElementReference itemAnchorRef;
    [CascadingParameter]
    BitDropdown Parent { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content to be rendered inside the dropdown item.
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the URL that the dropdown item should navigate to when clicked. 
    /// If this property is set, clicking the item will navigate to the specified URL.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Gets or sets a callback that is invoked when the dropdown item is clicked.
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dropdown item is currently active.
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dropdown item is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets additional attributes that do not match any of the explicitly defined parameters.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (Parent is null)
        {
            throw new InvalidOperationException("BitDropdownItem component must be used inside a BitDropdown component");
        }

        Parent.RegisterItem(this);
    }

    /// <inheritdoc/>
    public void Dispose() => Parent?.UnregisterItem(this);

    internal Task FocusAsync() => itemAnchorRef.FocusAsync().AsTask();

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
        var builder = new CssClassBuilder("dropdown-item", "list-item");

        if (Active)
        {
            builder.Add("active");
        }

        if (Disabled)
        {
            builder.Add("disabled");
        }

        AddSizeCssClass(builder);
        
        return builder.Build();
    }

    private void AddSizeCssClass(CssClassBuilder builder)
    {
        var sizeClass = Parent.ItemSize switch
        {
            DropdownItemSize.Large => "large",
            _ => string.Empty
        };

        builder.Add(sizeClass);
    }

    private async Task ClickAsync()
    {
        if (Disabled)
        {
            return;
        }

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
        switch (args.Key)
        {
            case "Enter" or " ":
                await ClickAsync();
                break;

            case "ArrowDown":
                await Parent.FocusNextItemAsync(this);
                break;

            case "ArrowUp":
                await Parent.FocusPreviousItemAsync(this);
                break;

            case "Escape":
                await Parent.CloseAsync();
                break;
        }
    }
}
