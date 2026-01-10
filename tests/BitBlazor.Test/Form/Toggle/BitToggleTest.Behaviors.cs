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
}
