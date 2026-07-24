namespace BitBlazor.Components;

/// <summary>
/// Specifies the available display modes for pagination controls.
/// </summary>
/// <remarks>
/// Use this enumeration to select between different pagination UI styles, such as a full-featured or simplified view. 
/// The selected mode determines how navigation elements are presented to the user.
/// </remarks>
public enum PaginationViewMode
{
    /// <summary>
    /// Renders the full pagination control
    /// </summary>
    Default,

    /// <summary>
    /// Renders the pagination control in simple mode (see https://italia.github.io/bootstrap-italia/docs/componenti/paginazione/#simple-mode)
    /// </summary>
    Simple
}
