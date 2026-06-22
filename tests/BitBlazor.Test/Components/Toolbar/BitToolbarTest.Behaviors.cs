using BitBlazor.Components;
using BitBlazor.Utilities;
using Bunit;

namespace BitBlazor.Test.Components.Toolbar;

public class BitToolbarTest
{
    [Fact]
    public void BitToolbar_Should_Handle_Item_Click_Event()
    {
        using var ctx = new BunitContext();

        bool clicked = false;
        Action onItemClick = () => clicked = true;

        // Arrange
        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarItem>(childParameters =>
            {
                childParameters.Add(p => p.Label, "Item 1");
                childParameters.Add(p => p.IconName, Icons.ItComment);
                childParameters.Add(p => p.OnClick, onItemClick);
            })
        );

        // Act
        cut.Find("a").Click();
        // Assert
        Assert.True(clicked);
    }
}
