using BitBlazor.Core;
using BitBlazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

public partial class BitAvatar : BitAvatarBase
{
    /// <summary>
    /// Gets or sets the presence status to be shown on the avatar
    /// </summary>
    [Parameter]
    public PresenceStatus PresenceStatus { get; set; } = PresenceStatus.None;

    /// <summary>
    /// Gets or sets the presence status description.
    /// </summary>
    [Parameter]
    public string? PresenceStatusDescription { get; set; }

    /// <summary>
    /// Gets or sets the icon name to display inside the presence status badge.
    /// </summary>
    [Parameter]
    public string? PresenceStatusIcon { get; set; }

    /// <summary>
    /// Gets or sets the foreground color of the icon inside the presence status badge.
    /// </summary>
    [Parameter]
    public IconColor PresenceStatusIconColor { get; set; } = IconColor.White;

    /// <summary>
    /// Gets or sets the user status to be shown on the avatar
    /// </summary>
    [Parameter]
    public UserStatus UserStatus { get; set; } = UserStatus.None;

    /// <summary>
    /// Gets or sets the user status description.
    /// </summary>
    [Parameter]
    public string? UserStatusDescription { get; set; }

    /// <summary>
    /// Gets or sets the icon name to display inside the user status badge.
    /// </summary>
    [Parameter]
    public string? UserStatusIcon { get; set; }

    /// <summary>
    /// Gets or sets the foreground color of the icon inside the user status badge.
    /// </summary>
    [Parameter]
    public IconColor UserStatusIconColor { get; set; } = IconColor.White;

    /// <summary>
    /// Gets or sets the extra text shown beside the avatar.
    /// </summary>
    [Parameter]
    public string? ExtraText { get; set; }
    
    private string GetUserStatusClass()
    {
        var statusClass = UserStatus switch
        {
            UserStatus.Approved => "approved",
            UserStatus.Declined => "declined",
            UserStatus.Notified => "notify",
            _ => string.Empty
        };

        return statusClass;
    }

    private string GetPresenceStatusClass()
    {
        var statusClass = PresenceStatus switch
        {
            PresenceStatus.Active => "active",
            PresenceStatus.Busy => "busy",
            PresenceStatus.Hidden => "hidden",
            _ => string.Empty
        };

        return statusClass;
    }

    private string ComputeWrapperCssClass()
    {
        var builder = new CssClassBuilder("avatar-wrapper");

        if (HasExtraText())
        {
            builder.Add("avatar-extra-text");
        }

        return builder.Build();
    }

    private bool HasPresenceStatus()
    {
        return PresenceStatus != PresenceStatus.None;
    }

    private bool HasPresenceStatusIcon()
    {
        return !string.IsNullOrWhiteSpace(PresenceStatusIcon);
    }

    private bool HasUserStatus()
    {
        return UserStatus != UserStatus.None;
    }

    private bool HasUserStatusIcon()
    {
        return !string.IsNullOrWhiteSpace(UserStatusIcon);
    }

    private bool HasExtraText()
    {
        return !string.IsNullOrEmpty(ExtraText);
    }

    private bool IsWrapperRequired()
    {
        return HasUserStatus() || HasPresenceStatus() || HasExtraText();
    }
}