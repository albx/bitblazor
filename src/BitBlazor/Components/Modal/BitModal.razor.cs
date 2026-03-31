using BitBlazor.Core;
using BitBlazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// Represents a modal dialog component following Bootstrap Italia styles and accessibility guidelines.
/// </summary>
/// <remarks>
/// The modal visibility is controlled entirely via Blazor state using <see cref="IsVisible"/> /
/// <see cref="IsVisibleChanged"/> — no JavaScript interop is required to show or hide it.
/// </remarks>
public partial class BitModal : BitComponentBase
{
    private string _autoId = string.Empty;

    /// <summary>
    /// Gets or sets whether the modal is currently visible.
    /// </summary>
    [Parameter]
    public bool IsVisible { get; set; }

    /// <summary>
    /// Callback invoked when the modal visibility changes, enabling two-way binding via <c>@bind-IsVisible</c>.
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets the main body content of the modal.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment BodyContent { get; set; } = default!;

    /// <summary>
    /// Gets or sets an optional title displayed in the modal header as an <c>h2</c> element.
    /// </summary>
    /// <remarks>
    /// When set, the modal automatically receives an <c>aria-labelledby</c> attribute pointing to the title element.
    /// When <see langword="null"/>, use <see cref="AriaLabel"/> for accessibility.
    /// </remarks>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets an override for the <c>id</c> attribute of the title <c>h2</c> element.
    /// When <see langword="null"/>, an auto-generated ID based on the modal's own ID is used.
    /// </summary>
    [Parameter]
    public string? TitleId { get; set; }

    /// <summary>
    /// Gets or sets the <c>aria-label</c> attribute for the modal.
    /// Use this when <see cref="Title"/> is not set to ensure accessibility for screen readers.
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets the value of the <c>aria-describedby</c> attribute on the modal element,
    /// pointing to the element ID that provides a description of the dialog.
    /// </summary>
    [Parameter]
    public string? AriaDescribedById { get; set; }

    /// <summary>
    /// Gets or sets a fully custom header render fragment that replaces the default title/close-button header.
    /// When set, <see cref="Title"/> is ignored but <see cref="ShowCloseButton"/> still applies.
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderContent { get; set; }

    /// <summary>
    /// Gets or sets the footer content of the modal.
    /// When <see langword="null"/>, no footer element is rendered.
    /// </summary>
    [Parameter]
    public RenderFragment? FooterContent { get; set; }

    /// <summary>
    /// Gets or sets whether a close button (<c>×</c>) is shown in the modal header. Defaults to <c>true</c>.
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// Gets or sets the <c>aria-label</c> value for the close button, used by screen readers.
    /// Defaults to <c>"Chiudi"</c>.
    /// </summary>
    [Parameter]
    public string CloseButtonAriaLabel { get; set; } = "Chiudi";

    /// <summary>
    /// Gets or sets the size variant of the modal. Defaults to <see cref="ModalSize.Default"/>.
    /// </summary>
    [Parameter]
    public ModalSize Size { get; set; } = ModalSize.Default;

    /// <summary>
    /// Gets or sets the positioning variant of the modal. Defaults to <see cref="ModalPosition.Default"/>.
    /// </summary>
    [Parameter]
    public ModalPosition Position { get; set; } = ModalPosition.Default;

    /// <summary>
    /// Gets or sets the visual type variant of the modal. Defaults to <see cref="ModalType.Default"/>.
    /// </summary>
    [Parameter]
    public ModalType Type { get; set; } = ModalType.Default;

    /// <summary>
    /// Gets or sets the backdrop behaviour. Defaults to <see cref="ModalBackdrop.Default"/>.
    /// </summary>
    [Parameter]
    public ModalBackdrop Backdrop { get; set; } = ModalBackdrop.Default;

    /// <summary>
    /// Gets or sets whether the modal body content should scroll internally,
    /// keeping the header and footer always visible.
    /// Applies the <c>it-dialog-scrollable</c> class to the modal element.
    /// Defaults to <c>false</c>.
    /// </summary>
    [Parameter]
    public bool ScrollableContent { get; set; }

    /// <summary>
    /// Gets or sets whether the modal footer renders with a top shadow (<c>modal-footer-shadow</c>).
    /// Useful for long scrollable content. Defaults to <c>false</c>.
    /// </summary>
    [Parameter]
    public bool FooterShadow { get; set; }

    /// <summary>
    /// Gets or sets whether the modal uses a fade animation when appearing/disappearing.
    /// Defaults to <c>true</c>.
    /// </summary>
    [Parameter]
    public bool Fade { get; set; } = true;

    /// <summary>
    /// Callback invoked just before the modal is closed (before <see cref="IsVisible"/> is set to <c>false</c>).
    /// </summary>
    [Parameter]
    public EventCallback OnClose { get; set; }

    /// <summary>Gets the effective element ID, using the auto-generated fallback when <see cref="BitComponentBase.Id"/> is not set.</summary>
    private string _effectiveId => string.IsNullOrWhiteSpace(Id) ? _autoId : Id!;

    /// <summary>Gets the ID used for the title <c>h2</c> element and <c>aria-labelledby</c>.</summary>
    private string _titleId => string.IsNullOrWhiteSpace(TitleId) ? $"{_effectiveId}-title" : TitleId!;

    /// <summary>
    /// Returns <c>true</c> when a header section should be rendered (title, custom header content or close button).
    /// </summary>
    private bool HasHeader => !string.IsNullOrWhiteSpace(Title) || HeaderContent is not null || ShowCloseButton;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        _autoId = $"modal-{Guid.NewGuid():N}"[..14];
        base.OnInitialized();
    }

    /// <inheritdoc/>
    protected override void SetElementId()
    {
        AdditionalAttributes["id"] = _effectiveId;
    }

    private string ComputeModalCssClasses()
    {
        var builder = new CssClassBuilder("modal");

        if (Fade)
        {
            builder.Add("fade");
        }

        if (IsVisible)
        {
            builder.Add("show");
        }

        var typeClass = Type switch
        {
            ModalType.Alert => "alert-modal",
            ModalType.LinkList => "it-dialog-link-list",
            ModalType.Popconfirm => "popconfirm-modal",
            _ => string.Empty,
        };
        builder.Add(typeClass);

        if (ScrollableContent || Position == ModalPosition.Left || Position == ModalPosition.Right)
        {
            builder.Add("it-dialog-scrollable");
        }

        AddCustomCssClass(builder);

        return builder.Build();
    }

    private string ComputeDialogCssClasses()
    {
        var builder = new CssClassBuilder("modal-dialog");

        var sizeClass = Size switch
        {
            ModalSize.Small => "modal-sm",
            ModalSize.Large => "modal-lg",
            ModalSize.ExtraLarge => "modal-xl",
            _ => string.Empty,
        };
        builder.Add(sizeClass);

        var positionClass = Position switch
        {
            ModalPosition.Centered => "modal-dialog-centered",
            ModalPosition.Left => "modal-dialog-left",
            ModalPosition.Right => "modal-dialog-right",
            _ => string.Empty,
        };
        builder.Add(positionClass);

        return builder.Build();
    }

    private string ComputeFooterCssClasses()
    {
        var builder = new CssClassBuilder("modal-footer");

        if (FooterShadow)
        {
            builder.Add("modal-footer-shadow");
        }

        return builder.Build();
    }

    private async Task CloseAsync()
    {
        await OnClose.InvokeAsync();
        await IsVisibleChanged.InvokeAsync(false);
    }

    private async Task HandleBackdropClickAsync()
    {
        if (Backdrop != ModalBackdrop.Static)
        {
            await CloseAsync();
        }
    }
}
