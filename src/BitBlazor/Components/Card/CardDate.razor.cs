using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class CardDate
{
    [CascadingParameter]
    BitCard Card { get; set; } = default!;

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

    private string FormattedDatetimeValue => string.IsNullOrEmpty(DatetimeFormat) ? Date.ToString("yyyy-MM-dd") : Date.ToString(DatetimeFormat);

    private string FormattedDisplayDate => string.IsNullOrEmpty(DisplayFormat) ? Date.ToString() : Date.ToString(DisplayFormat);
}
