namespace BitBlazor.Core;

/// <summary>
/// Provides a utility for building CSS class strings
/// </summary>
public sealed class CssClassBuilder
{
    private readonly HashSet<string> _cssClasses = new();

    /// <summary>
    /// Construct the <see cref="CssClassBuilder"/> instance, with a list of base css classes
    /// </summary>
    /// <param name="baseClasses">The css classes required to be included</param>
    public CssClassBuilder(params string[] baseClasses)
    {
        AddRange(baseClasses);
    }

    /// <summary>
    /// Constructs the <see cref="CssClassBuilder"/> instance
    /// </summary>
    public CssClassBuilder() { }

    /// <summary>
    /// Adds a new css class
    /// </summary>
    /// <param name="cssClass">The class to add</param>
    /// <returns>The <see cref="CssClassBuilder"/> instance for method chaining</returns>
    public CssClassBuilder Add(string cssClass)
    {
        if (!string.IsNullOrWhiteSpace(cssClass))
        {
            _cssClasses.Add(cssClass);
        }

        return this;
    }

    /// <summary>
    /// Adds a list of css classes
    /// </summary>
    /// <param name="cssClasses">The list of classes to add</param>
    /// <returns>The <see cref="CssClassBuilder"/> instance for method chaining</returns>
    public CssClassBuilder AddRange(IEnumerable<string> cssClasses)
    {
        foreach (var cssClass in cssClasses)
        {
            Add(cssClass);
        }

        return this;
    }

    /// <summary>
    /// Builds and returns a single string containing all CSS class names in the collection, separated by spaces.
    /// </summary>
    /// <returns>A string containing the concatenated CSS class names, separated by spaces.</returns>
    public string Build() => string.Join(" ", _cssClasses).Trim();
}
