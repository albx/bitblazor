using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents the image container to be used inside the card component
/// </summary>
public partial class CardImageWrapper
{
    [CascadingParameter]
    BitCard Parent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the card image wrapper
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    /// <summary>
    /// Notifies the Card parent component in order to add the specific class
    /// </summary>
    protected override void OnInitialized() => Parent.NotifyHasImageChanged(true);

    void IDisposable.Dispose()
    {
        Parent.NotifyHasImageChanged(false);
        GC.SuppressFinalize(this);
    }
}
