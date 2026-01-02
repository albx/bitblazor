using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents a single radio button item within a group, allowing selection of a value of type <typeparamref name="T"/> in a form or user interface.
/// </summary>
/// <remarks>
/// Use <see cref="BitRadio{T}"/> within a <see cref="BitRadioGroup{T}"/> to enable grouped selection behavior. 
/// The component supports labeling, disabling, and custom value association for flexible form scenarios.
/// </remarks>
/// <typeparam name="T">The type of the value associated with the radio button item.</typeparam>
public partial class BitRadio<T> : BitComponentBase
{
    [CascadingParameter]
    BitRadioGroup<T> Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the label to display
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value associated to the current radio button item
    /// </summary>
    [Parameter]
    public T? Value { get; set; }

    /// <summary>
    /// Gets or sets whether the component should be marked as disabled
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets an optional fragment of additional content to render.
    /// </summary>
    [Parameter]
    public RenderFragment? AdditionalText { get; set; }

    /// <summary>
    /// Gets or sets the identifier for additional text associated with the component.
    /// </summary>
    [Parameter]
    public string? AdditionalTextId { get; set; }

    /// <summary>
    /// Gets the prefix used to generate the unique Id of the component
    /// </summary>
    protected string FieldIdPrefix { get; } = "radio";

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetAdditionalTextAttributes();
    }

    /// <inheritdoc/>
    protected override void SetElementId()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            Id = $"{FieldIdPrefix}-{Guid.NewGuid():N}";
        }

        AdditionalAttributes["id"] = Id!;
    }

    private void SetAdditionalTextAttributes()
    {
        if (AdditionalText is not null && !string.IsNullOrWhiteSpace(AdditionalTextId))
        {
            AdditionalAttributes["aria-describedby"] = AdditionalTextId;
        }
        else
        {
            AdditionalAttributes.Remove("aria-describedby");
        }
    }

    private string ComputeContainerCssClasses()
    {
        var builder = new CssClassBuilder("form-check");
        if (Parent.Inline)
        {
            builder.Add("form-check-inline");
        }

        if (Parent.Grouped)
        {
            builder.Add("form-check-group");
        }

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private string ComputeLabelCssClasses()
    {
        var builder = new CssClassBuilder();
        if (Disabled)
        {
            builder.Add("disabled");
        }

        return builder.Build();
    }

    private RenderFragment? RenderAdditionalText()
    {
        if (AdditionalText is null)
        {
            return null;
        }

        return builder =>
        {
            builder.OpenElement(0, "small");
            builder.AddAttribute(1, "id", AdditionalTextId);
            builder.AddAttribute(2, "class", "form-text");
            builder.AddContent(3, AdditionalText);
            builder.CloseElement();
        };
    }
}
