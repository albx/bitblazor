using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BitBlazor.Components;

/// <summary>
/// Represents the context for the activator button of a <see cref="BitDropdown"/> component.
/// </summary>
/// <param name="dropdown">The <see cref="BitDropdown"/> instance</param>
public sealed class ActivatorContext(BitDropdown dropdown)
{
    /// <summary>
    /// Gets the value for the id of the dropdown activator button. 
    /// This ID is used to associate the button with the dropdown menu for accessibility purposes.
    /// </summary>
    public string ActivatorId { get; internal set; } = string.Empty;

    /// <summary>
    /// Gets the label for the dropdown activator button. 
    /// This label is displayed on the button that triggers the dropdown menu.
    /// </summary>
    public string ActivatorLabel { get; internal set; } = string.Empty;

    /// <summary>
    /// Gets or sets the element reference for the activator element.
    /// When using the default activator this is set automatically.
    /// When providing a custom <see cref="BitDropdown.ActivatorTemplate"/>, bind this to your
    /// activator element via <c>@ref="context.ActivatorRef"</c> to enable focus restoration on Escape.
    /// Omitting it is safe: <see cref="CloseAsync"/> degrades gracefully without it.
    /// </summary>
    public ElementReference ActivatorRef { get; set; }

    /// <summary>
    /// Gets a dictionary of attributes to be applied to the dropdown activator button.
    /// </summary>
    public IDictionary<string, object> Attributes { get; } = new Dictionary<string, object>()
    {
        ["aria-haspopup"] = "true",
        ["aria-expanded"] = "false"
    };

    /// <summary>
    /// Toggles the visibility of the dropdown menu. 
    /// This method is called when the activator button is clicked, and it updates the state of the dropdown accordingly.
    /// </summary>
    public void ToggleDropdown() => dropdown.Toggle();

    /// <summary>
    /// Handles keyboard events on the activator element.
    /// <list type="bullet">
    ///   <item><description><c>Enter</c> / <c>Space</c> — toggles the dropdown open or closed, keeping focus on the activator.</description></item>
    ///   <item><description><c>ArrowDown</c> — opens the dropdown and moves focus to the first enabled item.</description></item>
    ///   <item><description><c>ArrowUp</c> — opens the dropdown and moves focus to the last enabled item.</description></item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// When providing a custom <see cref="BitDropdown.ActivatorTemplate"/>, wire the following four
    /// directives onto your activator element to get full keyboard navigation and accessibility:
    /// <code>
    /// &lt;ActivatorTemplate Context="ctx"&gt;
    ///     &lt;button @ref="ctx.ActivatorRef"
    ///             @onclick="ctx.ToggleDropdown"
    ///             @onkeydown="ctx.HandleKeyDownAsync"
    ///             @onkeydown:preventDefault="true"
    ///             @attributes="ctx.Attributes"&gt;
    ///         Your custom content
    ///     &lt;/button&gt;
    /// &lt;/ActivatorTemplate&gt;
    /// </code>
    /// <list type="bullet">
    ///   <item><description><c>@ref</c> — optional; enables focus restoration on Escape. Safe to omit.</description></item>
    ///   <item><description><c>@onclick</c> — handles mouse / touch toggle.</description></item>
    ///   <item><description><c>@onkeydown</c> — handles keyboard toggle and arrow-key navigation.</description></item>
    ///   <item><description><c>@onkeydown:preventDefault</c> — prevents the browser from scrolling on arrow keys.</description></item>
    ///   <item><description><c>@attributes</c> — spreads <c>aria-haspopup</c> and <c>aria-expanded</c>.</description></item>
    /// </list>
    /// </remarks>
    public async Task HandleKeyDownAsync(KeyboardEventArgs args)
    {
        switch (args.Key)
        {
            case "Enter" or " ":
                dropdown.Toggle();
                break;

            case "ArrowDown":
                if (!dropdown.IsOpen)
                {
                    dropdown.Toggle();
                }
                await dropdown.FocusFirstItemAsync();
                break;

            case "ArrowUp":
                if (!dropdown.IsOpen)
                {
                    dropdown.Toggle();
                }
                await dropdown.FocusLastItemAsync();
                break;
        }
    }
}
