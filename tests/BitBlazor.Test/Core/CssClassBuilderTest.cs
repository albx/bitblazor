using BitBlazor.Core;

namespace BitBlazor.Test.Core;

public class CssClassBuilderTest
{
    [Fact]
    public void CssClassBuilder_Should_Return_Css_Class_String_As_Expected()
    {
        var builder = new CssClassBuilder("btn")
            .Add("btn-primary")
            .Add("btn-lg");

        var classString = builder.Build();

        Assert.Equal("btn btn-primary btn-lg", classString);
    }

    [Fact]
    public void CssClassBuilder_AddRange_Should_Add_All_Specified_Classes()
    {
        var builder = new CssClassBuilder("btn")
            .AddRange(["btn-primary", "btn-lg"]);

        var classString = builder.Build();

        Assert.Equal("btn btn-primary btn-lg", classString);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void CssClassBuilder_Add_Should_Avoid_Add_Empty_Or_Whitespace_String(string value)
    {
        var builder = new CssClassBuilder("btn")
            .Add("btn-primary")
            .Add(value);

        var classString = builder.Build();

        Assert.Equal("btn btn-primary", classString);
    }
}
