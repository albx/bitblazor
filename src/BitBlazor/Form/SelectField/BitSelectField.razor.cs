using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents a select field component.
/// </summary>
/// <remarks>
/// Use <see cref="ChildContent"/> to define the selectable options for the field.
/// This component is intended for use within Bit form layouts and renders options using the native HTML &lt;select&gt; element,
/// so <c>BitSelectItem</c> option content should be plain text only (nested markup inside options is not supported).</remarks>
/// <typeparam name="T">The type of the value represented and selected by the field.</typeparam>
public partial class BitSelectField<T> : BitFormComponentBase<T>, IBitSelectField
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "select";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [];

    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    private string ComputeContainerCssClass()
    {
        var builder = new CssClassBuilder("select-wrapper");
        AddCustomCssClass(builder);

        return builder.Build();
    }
}
