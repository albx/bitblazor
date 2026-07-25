using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a dropdown component that can be used to display a list of options or actions in a collapsible menu.
/// </summary>
public partial class BitDropdown : BitComponentBase
{
    /// <summary>
    /// Gets or sets the content to be rendered inside the dropdown. This content can include a list of <see cref="BitDropdownItem"/> components.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the label for the dropdown activator button. This label is displayed on the button that triggers the dropdown menu.
    /// </summary>
    [Parameter]
    public string ActivatorLabel { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unique identifier for the dropdown activator button. 
    /// This ID is used to associate the button with the dropdown menu for accessibility purposes.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string ActivatorId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a custom template for the dropdown activator button.
    /// </summary>
    [Parameter]
    public RenderFragment<ActivatorContext>? ActivatorTemplate { get; set; }

    /// <summary>
    /// Gets or sets the position of the dropdown menu relative to its toggle button. 
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="DropdownPosition.Down"/>, which positions the menu below the button.
    /// Other options include <see cref="DropdownPosition.Up"/>, <see cref="DropdownPosition.End"/>, and <see cref="DropdownPosition.Start"/>.
    /// </remarks>
    [Parameter]
    public DropdownPosition Position { get; set; } = DropdownPosition.Down;

    /// <summary>
    /// Gets or sets the color theme of the dropdown menu. 
    /// This property allows developers to customize the appearance of the dropdown menu based on their application's design requirements.
    /// </summary>
    [Parameter]
    public DropdownMenuColor MenuColor { get; set; } = DropdownMenuColor.Default;

    /// <summary>
    /// Gets or sets the width of the dropdown menu.
    /// </summary>
    [Parameter]
    public DropdownMenuWidth MenuWidth { get; set; } = DropdownMenuWidth.Default;

    /// <summary>
    /// Gets or sets the size of the dropdown items. 
    /// This property allows developers to specify the visual size of individual items within the dropdown menu.
    /// </summary>
    [Parameter]
    public DropdownItemSize ItemSize { get; set; } = DropdownItemSize.Default;

    /// <summary>
    /// Gets or sets a custom template for the heading of the dropdown menu.
    /// </summary>
    /// <remarks>
    /// This allows developers to provide a custom UI for the menu's heading section.
    /// </remarks>
    [Parameter]
    public RenderFragment? MenuHeaderTemplate { get; set; }

    private RenderFragment<ActivatorContext> RenderedActivator => ActivatorTemplate ?? DefaultActivator;

    private bool isOpen;

    internal bool IsOpen => isOpen;

    private readonly List<BitDropdownItem> _items = [];

    private IDictionary<string, object> dropdownMenuAttributes = new Dictionary<string, object>();

    private readonly ActivatorContext activatorContext;

    /// <summary>
    /// Constructs a new instance of the <see cref="BitDropdown"/> component and initializes the activator context.
    /// </summary>
    public BitDropdown()
    {
        activatorContext = new(this);
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        activatorContext.ActivatorId = ActivatorId;
        activatorContext.ActivatorLabel = ActivatorLabel;
    }

    internal void Toggle()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            dropdownMenuAttributes["data-popper-placement"] = Position switch
            {
                DropdownPosition.Up => "top-start",
                DropdownPosition.End => "right-start",
                DropdownPosition.Start => "left-start",
                _ => "bottom-start"
            };

            activatorContext.Attributes["aria-expanded"] = "true";
        }
        else
        {
            dropdownMenuAttributes.Remove("data-popper-placement");
            activatorContext.Attributes["aria-expanded"] = "false";
        }
    }

    private string ComputeDropdownContainerClass()
    {
        var builder = new CssClassBuilder("dropdown");
        AddPositionClass(builder);

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private void AddPositionClass(CssClassBuilder builder)
    {
        var positionClass = Position switch
        {
            DropdownPosition.Up => "dropup",
            DropdownPosition.End => "dropend",
            DropdownPosition.Start => "dropstart",
            _ => string.Empty
        };

        builder.Add(positionClass);
    }

    private string ComputeDropdownMenuClass()
    {
        var builder = new CssClassBuilder("dropdown-menu");

        if (isOpen)
        {
            builder.Add("show");
        }

        AddDropdownMenuColorCssClass(builder);
        AddDropdownMenuWidthCssClass(builder);

        return builder.Build();
    }

    private void AddDropdownMenuWidthCssClass(CssClassBuilder builder)
    {
        var widthClass = MenuWidth switch
        {
            DropdownMenuWidth.Full => "full-width",
            _ => string.Empty
        };

        builder.Add(widthClass);
    }

    private void AddDropdownMenuColorCssClass(CssClassBuilder builder)
    {
        var colorClass = MenuColor switch
        {
            DropdownMenuColor.Dark => "dark",
            _ => string.Empty
        };

        builder.Add(colorClass);
    }

    internal void RegisterItem(BitDropdownItem item) => _items.Add(item);

    internal void UnregisterItem(BitDropdownItem item) => _items.Remove(item);

    internal async Task FocusFirstItemAsync()
    {
        var first = _items.FirstOrDefault(i => !i.Disabled);
        if (first is not null)
        {
            await first.FocusAsync();
        }
    }

    internal async Task FocusLastItemAsync()
    {
        var last = _items.LastOrDefault(i => !i.Disabled);
        if (last is not null)
        {
            await last.FocusAsync();
        }
    }

    internal async Task FocusNextItemAsync(BitDropdownItem current)
    {
        var index = _items.IndexOf(current);
        var next = _items
            .Skip(index + 1)
            .FirstOrDefault(i => !i.Disabled)
            ?? _items.FirstOrDefault(i => !i.Disabled);

        if (next is not null)
        {
            await next.FocusAsync();
        }
    }

    internal async Task FocusPreviousItemAsync(BitDropdownItem current)
    {
        var index = _items.IndexOf(current);
        var previous = _items
            .Take(index)
            .LastOrDefault(i => !i.Disabled)
            ?? _items.LastOrDefault(i => !i.Disabled);

        if (previous is not null)
        {
            await previous.FocusAsync();
        }
    }

    internal async Task CloseAsync()
    {
        isOpen = false;
        activatorContext.Attributes["aria-expanded"] = "false";
        dropdownMenuAttributes.Remove("data-popper-placement");
        StateHasChanged();

        if (activatorContext.ActivatorRef.Id is not null)
        {
            await activatorContext.ActivatorRef.FocusAsync();
        }
    }
}
