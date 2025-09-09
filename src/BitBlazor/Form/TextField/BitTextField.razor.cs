using BitBlazor.Core;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BitBlazor.Form;

/// <summary>
/// Represents a text input field component.
/// </summary>
/// <remarks>
/// The <see cref="BitTextField"/> component is designed to handle string input values and provides built-in support for form integration and validation.
/// </remarks>
public partial class BitTextField : BitInputFieldBase<string?>
{
    /// <inheritdoc/>
    protected override string FieldIdPrefix { get; } = "text";

    /// <inheritdoc/>
    protected override Type[] SupportedTypes { get; } = [typeof(string)];

    /// <summary>
    /// Gets or sets the type of the text field.
    /// </summary>
    [Parameter]
    public TextFieldType Type { get; set; } = TextFieldType.Text;

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
    }

    private void UpdateLabelActiveState()
    {
        var active = !string.IsNullOrEmpty(Value) || !string.IsNullOrWhiteSpace(Placeholder);
        SetLabelActiveState(active);
    }
}
