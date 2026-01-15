using BitBlazor.Components;
using Bunit;

namespace BitBlazor.Test.Components.Button;

public class BitButtonTest
{
    [Fact]
    public void BitButton_Should_Call_OnClick_When_Clicked()
    {
        using var ctx = new BunitContext();

        // Arrange
        var clicked = false;
        Action onClickHandler = () => clicked = true;

        // Act
        var component = ctx.Render<BitButton>(
            parameters => parameters
                .Add(p => p.ChildContent, "Click Me")
                .Add(p => p.Color, Color.Primary)
                .Add(p => p.OnClick, onClickHandler));

        component.Find("button").Click();

        // Assert
        Assert.True(clicked);
    }
}
