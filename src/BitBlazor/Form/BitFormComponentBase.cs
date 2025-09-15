using System.Linq.Expressions;
using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BitBlazor.Form;

/// <summary>
/// Represents the base class for the form components
/// </summary>
/// <typeparam name="T">The data type supported by the component</typeparam>
public abstract class BitFormComponentBase<T> : BitComponentBase
{
    /// <summary>
    /// Gets or sets the <see cref="EditContext"/> instance in case of use of an <see cref="EditForm"/>
    /// </summary>
    [CascadingParameter]
    public EditContext? CurrentEditContext { get; set; }

    /// <summary>
    /// Gets or sets the label to display
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Label { get; set; } = string.Empty;

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
    /// Gets or sets whether the component should be marked as required
    /// </summary>
    [Parameter]
    public bool Required { get; set; }

    /// <summary>
    /// Gets or sets whether the component should be marked as disabled
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the placeholder to show in the component
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Gets the prefix used to generate the unique Id of the component
    /// </summary>
    protected abstract string FieldIdPrefix { get; }

    /// <summary>
    /// Gets the list of supported types
    /// </summary>
    /// <remarks>
    /// This property will validate the <typeparamref name="T"/> type in the costructor.
    /// </remarks>
    protected abstract Type[] SupportedTypes { get; }

    /// <summary>
    /// Construct the <see cref="BitFormComponentBase{T}"/> instance
    /// </summary>
    /// <exception cref="NotSupportedException">
    /// Raised when the specified <typeparamref name="T"/> is not a supported type for the component
    /// </exception>
    protected BitFormComponentBase()
    {
        var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        if (!SupportedTypes.Contains(targetType))
        {
            throw new NotSupportedException("Type not supported");
        }
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetRequiredAttribute();
        SetPlaceholderAttribute();
    }

    private void SetPlaceholderAttribute()
    {
        if (!string.IsNullOrWhiteSpace(Placeholder))
        {
            AdditionalAttributes["placeholder"] = Placeholder;
        }
        else
        {
            AdditionalAttributes.Remove("placeholder");
        }
    }

    private void SetRequiredAttribute()
    {
        if (Required)
        {
            AdditionalAttributes["required"] = "true";
            AdditionalAttributes["aria-required"] = "true";
        }
        else
        {
            AdditionalAttributes.Remove("required");
            AdditionalAttributes.Remove("aria-required");
        }
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
}
