namespace BitBlazor.Components;

/// <summary>
/// Defines the backdrop behaviour for the <see cref="BitModal"/> component.
/// </summary>
public enum ModalBackdrop
{
    /// <summary>
    /// A backdrop is shown and clicking it closes the modal.
    /// </summary>
    Default,

    /// <summary>
    /// A backdrop is shown but clicking it does <b>not</b> close the modal.
    /// </summary>
    Static,

    /// <summary>
    /// No backdrop is shown.
    /// </summary>
    None,
}
