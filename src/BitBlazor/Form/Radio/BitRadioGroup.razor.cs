using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BitBlazor.Form;

/// <summary>
/// Provides a group of radio buttons for selecting a single value from a set of options. 
/// Supports data binding, validation, and integration with forms in Blazor applications.
/// </summary>
/// <remarks>
/// Use <see cref="BitRadioGroup{T}"/> within an <see cref="EditForm"/> to enable form validation and model binding. 
/// The component supports inline and grouped layouts, and can be customized with child content representing individual radio items.
/// </remarks>
/// <typeparam name="T">The type of the value represented by the radio group. Typically corresponds to the type of the bound property.</typeparam>
public partial class BitRadioGroup<T> : BitComponentBase
{
    /// <summary>
    /// Gets or sets the value bound to the component
    /// </summary>
    /// <remarks>
    /// @bind-Value="model.PropertyName"
    /// </remarks>
    [Parameter]
    public T? Value { get; set; }

    /// <summary>
    /// Gets or sets the event callback raised when the bound value changes
    /// </summary>
    [Parameter]
    public EventCallback<T> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets an expression that identifies the bound value.
    /// </summary>
    [Parameter]
    public Expression<Func<T>>? ValueExpression { get; set; }

    /// <summary>
    /// Gets or sets the expression which triggers the validation
    /// </summary>
    [Parameter]
    public Expression<Func<T>>? For { get; set; }

    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    /// <remarks>
    /// Use this parameter to specify the child elements or markup that will be rendered within the component. 
    /// </remarks>
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets a value indicating whether the component's content should be rendered inline.
    /// </summary>
    [Parameter]
    public bool Inline { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the radio items are grouped.
    /// </summary>
    /// <remarks>
    /// Setting this value to true will align the radio items to the right of the text content.
    /// </remarks>
    [Parameter]
    public bool Grouped { get; set; }

    /// <summary>
    /// Gets the prefix used to generate the unique Id of the component
    /// </summary>
    protected string FieldIdPrefix { get; } = "radiogrp";

    /// <inheritdoc/>
    protected override void SetElementId()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            Id = $"{FieldIdPrefix}-{Guid.NewGuid():N}";
        }

        AdditionalAttributes["id"] = Id!;
    }

    private RenderFragment? RenderValidationMessage()
    {
        if (For is null)
        {
            return null;
        }

        return builder =>
        {
            builder.OpenComponent<ValidationMessage<T>>(0);
            builder.AddComponentParameter(1, nameof(ValidationMessage<T>.For), For);
            builder.AddAttribute(2, "class", "just-validate-error-label");
            builder.CloseComponent();
        };
    }
}
