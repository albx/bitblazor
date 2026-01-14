using BitBlazor.Form;
using Bunit;

namespace BitBlazor.Test.Form.Checkbox;

public class BitCheckboxTest
{
    [Fact]
    public void BitCheckbox_Should_Change_Value_Correctly()
    {
        bool value = false;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitCheckbox>(parameters => parameters
            .Add(p => p.Label, "label")
            .Add(p => p.Id, "test-checkbox")
            .Bind(p => p.Value, value, v => value = v));

        var checkbox = component.Find("#test-checkbox");
        checkbox.Change(true);

        Assert.True(value);
    }

    [Fact]
    public void BitCheckbox_Should_Add_Checked_Attribute_When_Value_Is_Changed_To_True()
    {
        bool value = false;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitCheckbox>(parameters => parameters
            .Add(p => p.Label, "label")
            .Add(p => p.Id, "test-checkbox")
            .Bind(p => p.Value, value, v => value = v));

        var checkbox = component.Find("#test-checkbox");
        checkbox.Change(true);

        Assert.True(checkbox.HasAttribute("checked"));
    }

    [Fact]
    public void BitCheckbox_Should_Remove_Checked_Attribute_When_Value_Is_Changed_To_False()
    {
        bool value = true;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitCheckbox>(parameters => parameters
            .Add(p => p.Label, "label")
            .Add(p => p.Id, "test-checkbox")
            .Bind(p => p.Value, value, v => value = v));

        var checkbox = component.Find("#test-checkbox");
        Assert.True(checkbox.HasAttribute("checked"));

        checkbox.Change(false);

        Assert.False(checkbox.HasAttribute("checked"));
    }
}
