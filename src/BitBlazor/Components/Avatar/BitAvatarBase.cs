using BitBlazor.Core;
using BitBlazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Components;

/// <summary>
/// R
/// </summary>
public abstract class BitAvatarBase : BitComponentBase
{
    /// <summary>
    /// Gets or sets the background color of the avatar box.
    /// </summary>
    [Parameter]
    public Color BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the size of the avatar.
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.Default;

    /// <summary>
    /// Gets or sets the image url to display in the avatar.
    /// </summary>
    [Parameter]
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the icon name to display in the box.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the foreground color of the icon.
    /// </summary>
    [Parameter]
    public IconColor IconColor { get; set; } = IconColor.Default;

    /// <summary>
    /// Gets or sets the url to associate to the avatar.
    /// </summary>
    [Parameter]
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets the main text (e.g. the name of the user represented by the avatar).
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the short text (e.g. the initials of the user represented by the avatar).
    /// </summary>
    /// <example>Something example</example>
    [Parameter]
    public string? TextShort { get; set; }

    /// <summary>
    /// Computes CSS classes for base element
    /// </summary>
    /// <returns>a <see cref="string"/> with all required CSS classes</returns>
    protected string ComputeCssClasses()
    {
        var builder = new CssClassBuilder("avatar");
        AddColorClass(builder);
        AddSizeClass(builder);

        AddCustomCssClass(builder);

        return builder.Build();
    }

    /// <summary>
    /// Computes color CSS classes for base element
    /// </summary>
    /// <returns>a <see cref="string"/> with all required CSS classes</returns>
    protected void AddColorClass(CssClassBuilder builder)
    {
        var backgroundColorCssClass = (BackgroundColor) switch
        {
            (Color.Primary) => "avatar-primary",
            (Color.Secondary) => "avatar-secondary",
            (Color.Success) => "avatar-green",
            (Color.Warning) => "avatar-orange",
            (Color.Danger) => "avatar-red",
            _ => string.Empty
        };

        builder.Add(backgroundColorCssClass);
    }

    /// <summary>
    /// Computes size CSS classes for base element
    /// </summary>
    /// <returns>a <see cref="string"/> with all required CSS classes</returns>
    protected void AddSizeClass(CssClassBuilder builder)
    {
        var sizeClass = Size switch
        {
            Size.Default => "size-md",
            Size.ExtraExtraLarge => "size-xxl",
            Size.ExtraLarge => "size-xl",
            Size.Large => "size-lg",
            Size.Small => "size-sm",
            Size.Mini => "size-xs",
            _ => string.Empty
        };

        if (!string.IsNullOrEmpty(sizeClass))
        {
            builder.Add(sizeClass);
        }
    }

    /// <summary>
    /// Get the short name for the avatar
    /// </summary>
    /// <returns>the short text if defined, the initials of the given name otherwise</returns>
    protected string? GetShortName()
    {
        string? shortName = string.Empty;

        if (!string.IsNullOrEmpty(TextShort))
        {
            shortName = TextShort;
        }
        else
        {
            shortName = GetInitials(Text);
        }

        int limit = Size switch
        {
            Size.Small or Size.Mini => 1,
            Size.Default or Size.ExtraExtraLarge or Size.ExtraLarge or Size.Large => 2,
            _ => 3
        };

        return shortName.Length > limit ? shortName.Substring(0, limit) : shortName;
    }
    
    /// <summary>
    /// Gets a value indicating wether the avatar has an image
    /// </summary>
    /// <returns><code>true</code> if the avatar has an image, <code>false</code> otherwise</returns>
    protected bool HasImage()
    {
        return !string.IsNullOrWhiteSpace(Image);
    }

    /// <summary>
    /// Gets a value indicating wether the avatar has a URL link
    /// </summary>
    /// <returns><code>true</code> if the avatar has a link, <code>false</code> otherwise</returns>
    protected bool HasLink()
    {
        return !string.IsNullOrWhiteSpace(Link);
    }

    /// <summary>
    /// Gets a value indicating wether the avatar has an icon
    /// </summary>
    /// <returns><code>true</code> if the avatar has an icon, <code>false</code> otherwise</returns>
    protected bool HasIcon()
    {
        return !string.IsNullOrWhiteSpace(Icon);
    }
    
    private static string GetInitials(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return string.Concat(value
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(x => x.Length >= 1 && char.IsLetter(x[0]))
            .Select(x => char.ToUpper(x[0])));
    }
}
