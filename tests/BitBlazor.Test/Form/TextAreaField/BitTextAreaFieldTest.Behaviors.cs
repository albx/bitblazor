using BitBlazor.Form;
using Bunit;

namespace BitBlazor.Test.Form.TextAreaField;

public class BitTextAreaFieldTest
{
    [Fact]
    public void BitTextAreaField_Should_Set_Active_Class_To_Label_On_Focus()
    {
        using var ctx = new BunitContext();

        string? value = null;

        var component = ctx.Render<BitTextAreaField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var label = component.Find("label");
        Assert.DoesNotContain("active", label.ClassList);

        var input = component.Find("textarea.form-control");
        input.Focus();

        Assert.Contains("active", label.ClassList);
    }

    [Fact]
    public void BitTextAreaField_Should_Set_Active_Class_To_Label_On_Blur_If_Value_Is_Not_Empty()
    {
        using var ctx = new BunitContext();

        string? value = null;

        var component = ctx.Render<BitTextAreaField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var label = component.Find("label");
        Assert.DoesNotContain("active", label.ClassList);

        component.Render(
            parameters => parameters.Add(p => p.Value, "new value"));

        var input = component.Find("textarea.form-control");
        input.Blur();

        Assert.Contains("active", label.ClassList);
    }

    [Fact]
    public void BitTextAreaField_Should_Set_Active_Class_To_Label_On_Change_If_Value_Is_Not_Empty()
    {
        using var ctx = new BunitContext();

        string? value = null;

        var component = ctx.Render<BitTextAreaField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var label = component.Find("label");
        Assert.DoesNotContain("active", label.ClassList);

        component.Render(
            parameters => parameters.Add(p => p.Value, "new value"));

        Assert.Contains("active", label.ClassList);
    }

    [Fact]
    public void BitTextAreaField_Should_Remove_Active_Class_From_Label_On_Blur_If_Value_Is_Empty()
    {
        using var ctx = new BunitContext();

        string? value = "initial value";

        var component = ctx.Render<BitTextAreaField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var label = component.Find("label");
        Assert.Contains("active", label.ClassList);

        component.Render(
            parameters => parameters.Add(p => p.Value, null));

        var input = component.Find("textarea.form-control");
        input.Blur();

        Assert.DoesNotContain("active", label.ClassList);
    }
}
