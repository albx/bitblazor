using BitBlazor.Form;
using Bunit;

namespace BitBlazor.Test.Form.PasswordField;

public class BitPasswordFieldTest
{
    [Fact]
    public void BitPasswordField_Should_Enable_Password_Visibility_On_Button_Click()
    {
        string? value = null;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitPasswordField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var input = component.Find("input.input-password");
        var button = component.Find("button.password-icon");
        var passwordVisibleIcon = component.Find("svg.password-icon-visible");
        var passwordInvisibleIcon = component.Find("svg.password-icon-invisible");

        button.Click();

        Assert.Equal("text", input.Attributes["type"]!.Value);
        Assert.Equal("true", button.Attributes["aria-checked"]!.Value);

        Assert.Contains("d-none", passwordVisibleIcon.ClassList);
        Assert.DoesNotContain("d-none", passwordInvisibleIcon.ClassList);
    }

    [Fact]
    public void BitPasswordField_Should_Disable_Password_Visibility_On_Button_Click()
    {
        string? value = null;

        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitPasswordField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var button = component.Find("button.password-icon");
        button.Click();

        var input = component.Find("input.input-password");
        var passwordVisibleIcon = component.Find("svg.password-icon-visible");
        var passwordInvisibleIcon = component.Find("svg.password-icon-invisible");

        button.Click();

        Assert.Equal("password", input.Attributes["type"]!.Value);
        Assert.Equal("false", button.Attributes["aria-checked"]!.Value);

        Assert.Contains("d-none", passwordInvisibleIcon.ClassList);
        Assert.DoesNotContain("d-none", passwordVisibleIcon.ClassList);
    }

    [Fact]
    public void BitPasswordField_Should_Set_Active_Class_To_Label_On_Focus()
    {
        using var ctx = new TestContext();

        string? value = null;

        var component = ctx.RenderComponent<BitPasswordField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var label = component.Find("label");
        Assert.DoesNotContain("active", label.ClassList);

        var input = component.Find("input.form-control");
        input.Focus();

        Assert.Contains("active", label.ClassList);
    }

    [Fact]
    public void BitPasswordField_Should_Set_Active_Class_To_Label_On_Blur_If_Value_Is_Not_Empty()
    {
        using var ctx = new TestContext();

        string? value = null;

        var component = ctx.RenderComponent<BitPasswordField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var label = component.Find("label");
        Assert.DoesNotContain("active", label.ClassList);

        component.SetParametersAndRender(
            parameters => parameters.Add(p => p.Value, "new value"));

        var input = component.Find("input.form-control");
        input.Blur();

        Assert.Contains("active", label.ClassList);
    }

    [Fact]
    public void BitPasswordField_Should_Set_Active_Class_To_Label_On_Change_If_Value_Is_Not_Empty()
    {
        using var ctx = new TestContext();

        string? value = null;

        var component = ctx.RenderComponent<BitPasswordField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var label = component.Find("label");
        Assert.DoesNotContain("active", label.ClassList);

        component.SetParametersAndRender(
            parameters => parameters.Add(p => p.Value, "new value"));

        Assert.Contains("active", label.ClassList);
    }

    [Fact]
    public void BitPasswordField_Should_Remove_Active_Class_From_Label_On_Blur_If_Value_Is_Empty()
    {
        using var ctx = new TestContext();

        string? value = "initial value";

        var component = ctx.RenderComponent<BitPasswordField>(parameters => parameters
            .Add(p => p.Label, "label")
            .Bind(p => p.Value, value, v => value = v));

        var label = component.Find("label");
        Assert.Contains("active", label.ClassList);

        component.SetParametersAndRender(
            parameters => parameters.Add(p => p.Value, null));

        var input = component.Find("input.form-control");
        input.Blur();

        Assert.DoesNotContain("active", label.ClassList);
    }
}
