namespace BitBlazor.Components;

/// <summary>
/// Defines the visual type variants available for the <see cref="BitModal"/> component.
/// </summary>
public enum ModalType
{
    /// <summary>
    /// Default modal with no additional type class.
    /// </summary>
    Default,

    /// <summary>
    /// Alert modal. Applies the <c>alert-modal</c> class, used alongside an icon in the header.
    /// </summary>
    Alert,

    /// <summary>
    /// Link list modal. Applies the <c>it-dialog-link-list</c> class, optimised for rendering navigation link lists.
    /// </summary>
    LinkList,

    /// <summary>
    /// Popconfirm modal. Applies the <c>popconfirm-modal</c> class for compact confirmation dialogs.
    /// </summary>
    Popconfirm,
}
