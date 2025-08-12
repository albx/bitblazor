using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the element which display the date on a card component
/// </summary>
public partial class CardDate
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date to be displayed in the card.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the format string used to display the value in the datetime attribute
    /// </summary>
    /// <remarks>
    /// For the list of valid datetime values, please refer to the official documentation: https://developer.mozilla.org/en-US/docs/Web/HTML/Reference/Elements/time#valid_datetime_values
    /// </remarks>
    [Parameter]
    [EditorRequired]
    public string DatetimeFormat { get; set; } = "yyyy-MM-dd";

    /// <summary>
    /// Gets or sets the format for displaying the date.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string DisplayFormat { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the color of the text
    /// </summary>
    [Parameter]
    public Color? TextColor { get; set; }

    private string FormattedDatetimeValue => string.IsNullOrEmpty(DatetimeFormat) ? Date.ToString("yyyy-MM-dd") : Date.ToString(DatetimeFormat);

    private string FormattedDisplayDate => string.IsNullOrEmpty(DisplayFormat) ? Date.ToString() : Date.ToString(DisplayFormat);

    private string ComputeCssClasses()
    {
        List<string> cssClasses = ["it-card-date"];

        if (TextColor.HasValue)
        {
            var textColorClass = TextColor.Value switch
            {
                Color.Primary => "text-primary",
                Color.Secondary => "text-secondary",
                Color.Warning => "text-warning",
                Color.Success => "text-success",
                Color.Danger => "text-danger",
                _ => string.Empty
            };

            cssClasses.Add(textColorClass);
        }

        return string.Join(" ", cssClasses);
    }
}
