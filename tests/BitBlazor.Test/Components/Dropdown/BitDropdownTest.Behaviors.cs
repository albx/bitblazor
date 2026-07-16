using BitBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Test.Components.Dropdown;

public class BitDropdownTest
{
    [Fact]
    public void BitDropdown_Should_Open_On_Toggle_Click()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var button = component.Find("button");
        button.Click();

        var menu = component.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
        Assert.Equal("true", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitDropdown_Should_Close_On_Toggle_Click_Again()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var button = component.Find("button");
        button.Click();
        button.Click();

        var menu = component.Find("div.dropdown-menu");
        Assert.False(menu.ClassList.Contains("show"));
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
    }

    [Theory]
    [InlineData(DropdownPosition.Down, "bottom-start")]
    [InlineData(DropdownPosition.Up, "top-start")]
    [InlineData(DropdownPosition.End, "right-start")]
    [InlineData(DropdownPosition.Start, "left-start")]
    public void BitDropdown_Should_Set_Placement_Attribute_Correctly_When_Open(DropdownPosition position, string expectedPlacement)
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        
        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .Add(p => p.Position, position)
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));
        
        var button = component.Find("button");
        button.Click();

        var menu = component.Find("div.dropdown-menu");
        Assert.Equal(expectedPlacement, menu.GetAttribute("data-popper-placement"));
    }
}
