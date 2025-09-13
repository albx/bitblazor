namespace BitBlazor.Form;

/// <summary>
/// Specifies the type of input field for a text-based form element.
/// </summary>
/// <remarks>
/// This enumeration is used to define the expected format or purpose of a text input field in a form. 
/// Each value corresponds to a specific type of input, such as plain text, email addresses, telephone numbers, or URLs.
/// </remarks>
public enum TextFieldType
{
    /// <summary>
    /// text type
    /// </summary>
    Text,

    /// <summary>
    /// email type
    /// </summary>
    Email,

    /// <summary>
    /// tel type
    /// </summary>
    Tel,

    /// <summary>
    /// url type
    /// </summary>
    Url
}
