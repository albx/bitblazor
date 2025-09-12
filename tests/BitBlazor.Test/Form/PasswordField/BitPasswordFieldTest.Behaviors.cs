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
}
