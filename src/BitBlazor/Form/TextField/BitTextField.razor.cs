using BitBlazor.Core;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Form;

/// <summary>
/// Represents a text input field component.
/// </summary>
/// <remarks>
/// The <see cref="BitTextField"/> component is designed to handle string input values and provides built-in support for form integration and validation.
/// It is a part of the BitFormComponentBase framework, which facilitates consistent handling of form fields.
/// </remarks>
public partial class BitTextField : BitFormComponentBase<string?>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "text";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(string)];

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
    /// Gets or sets the type of the text field.
    /// </summary>
    [Parameter]
    public TextFieldType Type { get; set; } = TextFieldType.Text;

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
    /// Gets or sets the content which will be displayed before the input
    /// </summary>
    [Parameter]
    public RenderFragment? PrependContent { get; set; }

    /// <summary>
    /// Gets or sets the content which will be displayed after the input
    /// </summary>
    [Parameter]
    public RenderFragment? AppendContent { get; set; }

    private bool isLabelActive = false;

    private string FieldTypeString => Type switch
    {
        TextFieldType.Email => "email",
        TextFieldType.Tel => "tel",
        TextFieldType.Url => "url",
        _ => "text"
    };

    private bool IsInputGroup => PrependContent is not null || AppendContent is not null;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        UpdateLabelActiveState();
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

    private void SetLabelActiveState(bool active) => isLabelActive = active;

    private void UpdateLabelActiveState()
    {
        var active = !string.IsNullOrEmpty(Value) || !string.IsNullOrWhiteSpace(Placeholder);
        SetLabelActiveState(active);
    }

    private string ComputeInputCssClass()
    {
        var builder = new CssClassBuilder();
        if (!Plaintext)
        {
            builder.Add("form-control");
        }
        else
        {
            builder.Add("form-control-plaintext");
        }

        return builder.Build();
    }

    private string ComputeLabelCssClass()
    {
        var builder = new CssClassBuilder();
        if (isLabelActive)
        {
            builder.Add("active");
        }

        return builder.Build();
    }
}
