using System.Linq.Expressions;
using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BitBlazor.Form;

/// <summary>
/// Represents the base class for the form components
/// </summary>
/// <typeparam name="T">The data type supported by the component</typeparam>
public abstract class BitFormComponentBase<T> : BitComponentBase, IDisposable
{
    private string validationCssClass = string.Empty;
    private bool disposedValue;

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
    protected abstract string FieldIdPrefix { get; }

    /// <summary>
    /// Gets the list of supported types
    /// </summary>
    /// <remarks>
    /// This property will validate the <typeparamref name="T"/> type in the constructor.
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
        if (SupportedTypes.Length > 0 && !SupportedTypes.Contains(ComponentType))
        {
            throw new NotSupportedException("Type not supported");
        }
    }

    /// <summary>
    /// Gets the underlying non-nullable type of the generic parameter T, or T itself if it is not nullable.
    /// </summary>
    /// <remarks>
    /// This property is useful when working with generic types that may be nullable value types. 
    /// If T is a nullable value type (for example, int?), the property returns the underlying type (int). Otherwise, it returns T. 
    /// This can help ensure type consistency when performing reflection or type-based operations.
    /// </remarks>
    protected Type ComponentType => Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (CurrentEditContext is not null)
        {
            CurrentEditContext.OnValidationStateChanged += OnFieldValidationStateChanged;
        }
    }

    private void OnFieldValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        if (ValueExpression is not null)
        {
            var fieldIdentifier = FieldIdentifier.Create(ValueExpression);
            var fieldValidationCssClass = CurrentEditContext!.IsValid(fieldIdentifier) ? "just-validate-success-field" : "is-invalid";

            if (fieldValidationCssClass != validationCssClass)
            {
                validationCssClass = fieldValidationCssClass;
                InvokeAsync(StateHasChanged);
            }
        }
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetRequiredAttribute();
        SetAdditionalTextAttributes();
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

    /// <summary>
    /// Updates the validation CSS class based on the current validation state of the field.
    /// </summary>
    /// <remarks>
    /// This method checks the EditContext for validation state and applies the appropriate CSS class:
    /// <list type="bullet">
    /// <item><description>"is-invalid" - field has validation errors (shown immediately when validation runs)</description></item>
    /// <item><description>"just-validate-success-field" - field is modified and is valid</description></item>
    /// <item><description>Empty string - field is valid and has not been modified</description></item>
    /// </list>
    /// </remarks>
    private void UpdateValidationCssClass()
    {
        if (CurrentEditContext is null || ValueExpression is null)
        {
            validationCssClass = string.Empty;
            return;
        }

        var fieldIdentifier = FieldIdentifier.Create(ValueExpression);
        
        validationCssClass = CurrentEditContext.IsValid(fieldIdentifier) ? "just-validate-success-field" : "is-invalid";
    }

    /// <summary>
    /// Adds the Bootstrap Italia validation CSS class to the provided <see cref="CssClassBuilder"/>.
    /// </summary>
    /// <param name="builder">The CSS class builder to add the validation class to.</param>
    /// <remarks>
    /// Adds "is-invalid" for invalid fields, "just-validate-success-field" for valid modified fields,
    /// or nothing for unmodified fields. This method should be called when building the CSS classes
    /// for form input elements.
    /// </remarks>
    protected void AddValidationCssClass(CssClassBuilder builder)
    {
        builder.Add(validationCssClass);
    }

    /// <summary>
    /// Renders a validation message for the specified field.
    /// </summary>
    /// <remarks>
    /// This method generates a <see cref="ValidationMessage{T}"/> component for the field specified by the <see cref="BitFormComponentBase{T}.For"/> property. 
    /// The rendered validation message will include the CSS class "is-invalid" to indicate an invalid state.
    /// </remarks>
    /// <returns>
    /// A <see cref="RenderFragment"/> that renders the validation message, or <see langword="null"/> if the <see cref="BitFormComponentBase{T}.For"/> property is not set.
    /// </returns>
    protected RenderFragment? RenderValidationMessage()
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

    /// <summary>
    /// Creates a render fragment that displays additional text within a <c>small</c> HTML element.
    /// </summary>
    /// <remarks>
    /// If the <see cref="AdditionalText"/> property is <see langword="null"/>, this method returns <see langword="null"/>. 
    /// Otherwise, it generates a render fragment that includes the additional text and assigns an optional attribute with the value of <see cref="AdditionalTextId"/>.
    /// </remarks>
    /// <returns>
    /// A <see cref="RenderFragment"/> that renders the additional text, or <see langword="null"/> if <see cref="AdditionalText"/> is <see langword="null"/>.
    /// </returns>
    protected virtual RenderFragment? RenderAdditionalText()
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

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (CurrentEditContext is not null)
                {
                    CurrentEditContext.OnValidationStateChanged -= OnFieldValidationStateChanged;
                }
            }

            disposedValue = true;
        }
    }

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
