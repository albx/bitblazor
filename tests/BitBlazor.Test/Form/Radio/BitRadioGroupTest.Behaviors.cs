using BitBlazor.Form;
using Bunit;

namespace BitBlazor.Test.Form.Radio;

public class BitRadioGroupTest
{
    enum RadioItemValues
    {
        Value1,
        Value2
    }

    [Fact]
    public void BitRadioGroup_Should_Change_Value_Correctly()
    {
        RadioItemValues? value = RadioItemValues.Value1;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitRadioGroup<RadioItemValues?>>(parameters => parameters
            .Add(p => p.Id, "radiogroup1")
            .Bind(p => p.Value, value, v => value = v)
            .AddChildContent<BitRadio<RadioItemValues?>>(child => child
                .Add(p => p.Label, "label 1")
                .Add(p => p.Id, "radio1")
                .Add(p => p.Value, RadioItemValues.Value1))
            .AddChildContent<BitRadio<RadioItemValues?>>(child => child
                .Add(p => p.Label, "label 2")
                .Add(p => p.Id, "radio2")
                .Add(p => p.Value, RadioItemValues.Value2)));

        var radio2 = component.Find("#radio2");
        radio2.Change(RadioItemValues.Value2);

        Assert.Equal(RadioItemValues.Value2, value);
    }

    [Fact]
    public void BitRadioGroup_Should_Set_Checked_Attribute_Correctly()
    {
        RadioItemValues? value = null;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitRadioGroup<RadioItemValues?>>(parameters => parameters
            .Add(p => p.Id, "radiogroup1")
            .Bind(p => p.Value, value, v => value = v)
            .AddChildContent<BitRadio<RadioItemValues?>>(child => child
                .Add(p => p.Label, "label 1")
                .Add(p => p.Id, "radio1")
                .Add(p => p.Value, RadioItemValues.Value1))
            .AddChildContent<BitRadio<RadioItemValues?>>(child => child
                .Add(p => p.Label, "label 2")
                .Add(p => p.Id, "radio2")
                .Add(p => p.Value, RadioItemValues.Value2)));

        var radio1 = component.Find("#radio1");
        radio1.Change(RadioItemValues.Value1);

        Assert.True(radio1.HasAttribute("checked"));
    }

    [Fact]
    public void BitRadioGroup_Should_Change_Checked_Attribute_Correctly()
    {
        RadioItemValues? value = RadioItemValues.Value1;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitRadioGroup<RadioItemValues?>>(parameters => parameters
            .Add(p => p.Id, "radiogroup1")
            .Bind(p => p.Value, value, v => value = v)
            .AddChildContent<BitRadio<RadioItemValues?>>(child => child
                .Add(p => p.Label, "label 1")
                .Add(p => p.Id, "radio1")
                .Add(p => p.Value, RadioItemValues.Value1))
            .AddChildContent<BitRadio<RadioItemValues?>>(child => child
                .Add(p => p.Label, "label 2")
                .Add(p => p.Id, "radio2")
                .Add(p => p.Value, RadioItemValues.Value2)));

        var radio2 = component.Find("#radio2");
        radio2.Change(RadioItemValues.Value2);

        var radio1 = component.Find("#radio1");

        Assert.True(radio2.HasAttribute("checked"));
        Assert.False(radio1.HasAttribute("checked"));
    }
}
