using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a toolbar component that can contain multiple <see cref="BitToolbarItem"/> components. It provides a container for organizing and displaying toolbar items
/// </summary>
public partial class BitToolbar : BitComponentBase
{
    /// <summary>
    /// Gets or sets the content to be rendered inside the toolbar. 
    /// This can include one or more <see cref="BitToolbarItem"/> components,
    /// optionally separated by <see cref="BitToolbarDivider"/> components.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the size of the toolbar. 
    /// This property allows you to specify the visual size of the toolbar, which can affect its layout and appearance. 
    /// The default value is <see cref="ToolbarSize.Default"/>.
    /// </summary>
    [Parameter]
    public ToolbarSize Size { get; set; } = ToolbarSize.Default;

    /// <summary>
    /// Gets or sets the orientation of the toolbar. 
    /// This property determines whether the toolbar items are arranged horizontally or vertically. 
    /// The default value is <see cref="Orientation.Horizontal"/>.
    /// </summary>
    [Parameter]
    public Orientation Orientation { get; set; } = Orientation.Horizontal;

    private string ComputeContainerCssClass()
    {
        var builder = new CssClassBuilder("toolbar");
        AddSizeClass(builder);
        AddOrientationClass(builder);

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private void AddOrientationClass(CssClassBuilder builder)
    {
        var orientationClass = Orientation switch
        {
            Orientation.Vertical => "toolbar-vertical",
            _ => string.Empty
        };

        builder.Add(orientationClass);
    }

    private void AddSizeClass(CssClassBuilder builder)
    {
        var sizeClass = Size switch
        {
            ToolbarSize.Small => "toolbar-small",
            ToolbarSize.Medium => "toolbar-medium",
            _ => string.Empty
        };

        builder.Add(sizeClass);
    }
}
