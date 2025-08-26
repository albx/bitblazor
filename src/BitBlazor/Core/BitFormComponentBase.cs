using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BitBlazor.Core;

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

    private bool isLabelActive = false;

    /// <summary>
    /// Construct the <see cref="BitFormComponentBase{T}"/> instance
    /// </summary>
    /// <exception cref="NotSupportedException">
    /// Raised when the specified <typeparamref name="T"/> is not a supported type for the component
    /// </exception>
    protected BitFormComponentBase()
    {
        if (!SupportedTypes.Contains(typeof(T)))
        {
            throw new NotSupportedException("Type not supported");
        }
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetRequiredAttribute();
        SetInitialLabelState();
    }

    private void SetInitialLabelState()
    {
        isLabelActive = !ValueIsEmpty();
    }

    /// <summary>
    /// Determines whether the current value is considered empty.
    /// </summary>
    /// <remarks>The definition of "empty" is determined by the implementing class.</remarks>
    /// <returns><see langword="true"/> if the value is empty; otherwise, <see langword="false"/>.</returns>
    protected abstract bool ValueIsEmpty();

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

    /// <summary>
    /// Sets the label's active state.
    /// </summary>
    /// <param name="active">
    /// A value indicating whether the label should be marked as active.
    /// <see langword="true"/> to mark the label as active; otherwise, <see langword="false"/>.
    /// </param>
    protected void SetLabelAsActive(bool active) => isLabelActive = active;

    /// <summary>
    /// Computes the CSS class string for a label based on its current state.
    /// </summary>
    /// <returns>
    /// A string containing the CSS class for the label. 
    /// Returns "active" if the label is in an active state; otherwise, returns an empty string.
    /// </returns>
    protected string ComputeLabelCssClass()
    {
        var builder = new CssClassBuilder();
        if (isLabelActive)
        {
            builder.Add("active");
        }

        return builder.Build();
    }
}
