using BitBlazor.Form;
using Bunit;

namespace BitBlazor.Test.Form.Toggle;

public class BitToggleTest
{
    [Fact]
    public void BitToggle_Should_Change_Value_Correctly()
    {
        bool value = false;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitToggle>(parameters => parameters
            .Add(p => p.Label, "label")
            .Add(p => p.Id, "test-toggle")
            .Bind(p => p.Value, value, v => value = v));

        var toggle = component.Find("#test-toggle");
        toggle.Change(true);

        Assert.True(value);
    }

    [Fact]
    public void BitToggle_Should_Add_Checked_Attribute_When_Value_Is_Changed_To_True()
    {
        bool value = false;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitToggle>(parameters => parameters
            .Add(p => p.Label, "label")
            .Add(p => p.Id, "test-toggle")
            .Bind(p => p.Value, value, v => value = v));

        var toggle = component.Find("#test-toggle");
        toggle.Change(true);

        Assert.True(toggle.HasAttribute("checked"));
    }

    [Fact]
    public void BitToggle_Should_Remove_Checked_Attribute_When_Value_Is_Changed_To_False()
    {
        bool value = true;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitToggle>(parameters => parameters
            .Add(p => p.Label, "label")
            .Add(p => p.Id, "test-toggle")
            .Bind(p => p.Value, value, v => value = v));

        var toggle = component.Find("#test-toggle");
        Assert.True(toggle.HasAttribute("checked"));

        toggle.Change(false);

        Assert.False(toggle.HasAttribute("checked"));
    }
}
