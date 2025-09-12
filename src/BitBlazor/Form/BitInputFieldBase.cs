﻿using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BitBlazor.Form;

/// <summary>
/// Represents the base class for input field components in the Bit framework.
/// </summary>
/// <remarks>
/// This abstract class provides common functionality for input field components, including support for readonly and plaintext modes, additional descriptive text, and configurable size options. 
/// Derived classes can extend this functionality to implement specific input field behaviors.
/// </remarks>
/// <typeparam name="T">The type of the value associated with the input field.</typeparam>
public abstract class BitInputFieldBase<T> : BitFormComponentBase<T>
{
    /// <summary>
    /// Gets or sets whether the input component should be readonly
    /// </summary>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// Gets or sets whether the input should be rendered as plain-text
    /// </summary>
    [Parameter]
    public bool Plaintext { get; set; }

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
    /// Gets or sets the size of the text field component.
    /// </summary>
    /// <remarks>
    /// The <see cref="Size"/> property determines the visual size of the text field.
    /// Supported values are <see cref="Size.Default"/>, <see cref="Size.Large"/>, and <see cref="Size.Small"/>.
    /// The default value is <see cref="Size.Default"/>.
    /// </remarks>
    [Parameter]
    [AllowedValues(Size.Default, Size.Large, Size.Small)]
    public Size Size { get; set; } = Size.Default;

    private bool isLabelActive = false;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetAdditionalTextAttributes();
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
            builder.AddAttribute(2, "class", "is-invalid");
            builder.CloseComponent();
        };
    }

    /// <summary>
    /// Sets the active state of the label.
    /// </summary>
    /// <param name="active"><see langword="true"/> to activate the label; <see langword="false"/> to deactivate it.</param>
    protected void SetLabelActiveState(bool active) => isLabelActive = active;

    /// <summary>
    /// Computes the CSS class string for an input element based on the current configuration.
    /// </summary>
    /// <returns>A string containing the computed CSS classes for the input element.</returns>
    protected virtual string ComputeInputCssClass()
    {
        var builder = new CssClassBuilder();

        AddDefaultCssClass(builder);
        AddSizeCssClass(builder);
        AddCustomCssClass(builder);

        return builder.Build();
    }

    /// <summary>
    /// Adds a default CSS class to the specified <see cref="CssClassBuilder"/> based on the current state.
    /// </summary>
    /// <remarks>
    /// If the <see cref="Plaintext"/> property is <see langword="false"/>, the "form-control" class is added. Otherwise, the "form-control-plaintext" class is added.
    /// </remarks>
    /// <param name="builder">
    /// The <see cref="CssClassBuilder"/> to which the CSS class will be added. Cannot be null.
    /// </param>
    protected void AddDefaultCssClass(CssClassBuilder builder)
    {
        if (!Plaintext)
        {
            builder.Add("form-control");
        }
        else
        {
            builder.Add("form-control-plaintext");
        }
    }

    /// <summary>
    /// Adds a CSS class to the specified <see cref="CssClassBuilder"/> based on the current size setting.
    /// </summary>
    /// <remarks>
    /// The CSS class added corresponds to the value of the <c>Size</c> property: 
    /// <list type="bullet"> 
    /// <item><description><c>"form-control-lg"</c> is added for <c>Size.Large</c>.</description></item> 
    /// <item><description><c>"form-control-sm"</c> is added for <c>Size.Small</c>.</description></item>
    /// <item><description>No class is added for other values.</description></item> 
    /// </list>
    /// </remarks>
    /// <param name="builder">The <see cref="CssClassBuilder"/> to which the size-related CSS class will be added.</param>
    protected void AddSizeCssClass(CssClassBuilder builder)
    {
        var sizeClass = Size switch
        {
            Size.Large => "form-control-lg",
            Size.Small => "form-control-sm",
            _ => string.Empty
        };
        builder.Add(sizeClass);
    }

    /// <summary>
    /// Computes the CSS class for a label based on its current state.
    /// </summary>
    /// <remarks>
    /// This method can be overridden in derived classes to customize the logic for computing the label's CSS class.</remarks>
    /// <returns>A string containing the computed CSS class. Returns "active" if the label is active; otherwise, an empty string.</returns>
    protected virtual string ComputeLabelCssClass()
    {
        var builder = new CssClassBuilder();
        if (isLabelActive)
        {
            builder.Add("active");
        }

        return builder.Build();
    }
}
