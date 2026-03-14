namespace BitBlazor.Components;

/// <summary>
/// Defines the positioning variants available for the <see cref="BitModal"/> component.
/// </summary>
public enum ModalPosition
{
    /// <summary>
    /// Default positioning (top-center).
    /// </summary>
    Default,

    /// <summary>
    /// Vertically centered modal. Applies <c>modal-dialog-centered</c> to the dialog element.
    /// </summary>
    Centered,

    /// <summary>
    /// Left-aligned, full-height modal. Applies <c>it-dialog-scrollable</c> to the modal element
    /// and <c>modal-dialog-left</c> to the dialog element.
    /// </summary>
    Left,

    /// <summary>
    /// Right-aligned, full-height modal. Applies <c>it-dialog-scrollable</c> to the modal element
    /// and <c>modal-dialog-right</c> to the dialog element.
    /// </summary>
    Right,
}
