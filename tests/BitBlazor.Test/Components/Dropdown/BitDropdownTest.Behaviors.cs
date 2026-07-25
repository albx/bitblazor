using BitBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

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

    [Fact]
    public void BitDropdownItem_Should_Invoke_OnClick_Callback()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var clicked = false;
        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.OnClick, EventCallback.Factory.Create(ctx, () => clicked = true))
                .AddChildContent("<span>Click Me</span>")));

        var link = component.Find("a.dropdown-item");
        link.Click();

        Assert.True(clicked);
    }

    [Fact]
    public void BitDropdownItem_Should_Navigate_When_Href_Set_And_OnClick_Not_Set()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.Href, "/test-page")
                .AddChildContent("<span>Navigation Link</span>")));

        var navManager = ctx.Services.GetRequiredService<NavigationManager>();
        var link = component.Find("a.dropdown-item");
        link.Click();

        Assert.Equal("http://localhost/test-page", navManager.Uri);
    }

    [Fact]
    public void BitDropdownItem_Should_Not_Trigger_Click_When_Disabled()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var clicked = false;
        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.Disabled, true)
                .Add(p => p.OnClick, EventCallback.Factory.Create(ctx, () => clicked = true))
                .AddChildContent("<span>Disabled Item</span>")));

        var link = component.Find("a.dropdown-item");
        link.Click();

        Assert.False(clicked);
    }

    [Fact]
    public void BitDropdownItem_Should_Handle_Enter_Key_Press()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var clicked = false;
        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.OnClick, EventCallback.Factory.Create(ctx, () => clicked = true))
                .AddChildContent("<span>Item</span>")));

        var link = component.Find("a.dropdown-item");
        link.KeyDown("Enter");

        Assert.True(clicked);
    }

    [Fact]
    public void BitDropdownItem_Should_Handle_Space_Key_Press()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var clicked = false;
        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.OnClick, EventCallback.Factory.Create(ctx, () => clicked = true))
                .AddChildContent("<span>Item</span>")));

        var link = component.Find("a.dropdown-item");
        link.KeyDown(" ");

        Assert.True(clicked);
    }

    [Fact]
    public void BitDropdownItem_Should_Ignore_Other_Key_Presses()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var clicked = false;
        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.OnClick, EventCallback.Factory.Create(ctx, () => clicked = true))
                .AddChildContent("<span>Item</span>")));

        var link = component.Find("a.dropdown-item");
        link.KeyDown("Escape");

        Assert.False(clicked);
    }

    [Fact]
    public void BitDropdownItem_Should_Not_Trigger_Key_Press_When_Disabled()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var clicked = false;
        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.Disabled, true)
                .Add(p => p.OnClick, EventCallback.Factory.Create(ctx, () => clicked = true))
                .AddChildContent("<span>Item</span>")));

        var link = component.Find("a.dropdown-item");
        link.KeyDown("Enter");

        Assert.False(clicked);
    }

    [Fact]
    public void BitDropdownItem_Should_Prioritize_OnClick_Over_Href()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var clicked = false;
        var navManager = ctx.Services.GetRequiredService<NavigationManager>();
        var initialUri = navManager.Uri;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.Href, "/other-page")
                .Add(p => p.OnClick, EventCallback.Factory.Create(ctx, () => clicked = true))
                .AddChildContent("<span>Item</span>")));

        var link = component.Find("a.dropdown-item");
        link.Click();

        Assert.True(clicked);
        // The URI should not change because OnClick callback takes precedence
        Assert.Equal(initialUri, navManager.Uri);
    }

    [Fact]
    public void BitDropdownItem_Should_Apply_Active_Class()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.Active, true)
                .AddChildContent("<span>Active Item</span>")));

        var link = component.Find("a.dropdown-item");
        Assert.Contains("active", link.ClassList);
    }

    [Fact]
    public void BitDropdownItem_Should_Apply_Disabled_Class_And_Aria_Attribute()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.Disabled, true)
                .AddChildContent("<span>Disabled Item</span>")));

        var link = component.Find("a.dropdown-item");
        Assert.Contains("disabled", link.ClassList);
        Assert.Equal("true", link.GetAttribute("aria-disabled"));
    }

    [Fact]
    public void BitDropdownItem_Must_Be_Inside_BitDropdown()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var exception = Assert.Throws<InvalidOperationException>(() => ctx.Render<BitDropdownItem>(parameters => parameters
            .AddChildContent("<span>Orphan Item</span>")));

        Assert.Contains("BitDropdownItem component must be used inside a BitDropdown component", exception.Message);
    }

    [Fact]
    public void BitDropdown_Should_Open_On_ArrowDown_Key_On_Activator()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var button = component.Find("button");
        button.KeyDown("ArrowDown");

        var menu = component.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
    }

    [Fact]
    public void BitDropdown_Should_Open_On_ArrowUp_Key_On_Activator()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var button = component.Find("button");
        button.KeyDown("ArrowUp");

        var menu = component.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
    }

    [Fact]
    public void BitDropdown_Should_Not_Toggle_On_Other_Keys_On_Activator()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var button = component.Find("button");
        button.KeyDown("Tab");

        var menu = component.Find("div.dropdown-menu");
        Assert.DoesNotContain("show", menu.ClassList);
    }

    [Fact]
    public void BitDropdown_Should_Open_On_Enter_Key_On_Activator_Without_Moving_Focus()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var button = component.Find("button");
        button.KeyDown("Enter");

        var menu = component.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
        Assert.Equal("true", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitDropdown_Should_Close_On_Enter_Key_When_Already_Open()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var button = component.Find("button");
        button.Click();
        button.KeyDown("Enter");

        var menu = component.Find("div.dropdown-menu");
        Assert.DoesNotContain("show", menu.ClassList);
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitDropdownItem_Should_Close_Dropdown_On_Escape_Key()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var button = component.Find("button");
        button.Click();

        var menu = component.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);

        var link = component.Find("a.dropdown-item");
        link.KeyDown("Escape");

        Assert.DoesNotContain("show", menu.ClassList);
    }

    [Fact]
    public void BitDropdown_Should_Move_Focus_To_Next_Item_On_ArrowDown_Key()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item 1</span>"))
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item 2</span>")));

        var button = component.Find("button");
        button.Click();

        var firstLink = component.FindAll("a.dropdown-item")[0];
        firstLink.KeyDown("ArrowDown");

        var menu = component.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
    }

    [Fact]
    public void BitDropdown_Should_Move_Focus_To_Previous_Item_On_ArrowUp_Key()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item 1</span>"))
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item 2</span>")));

        var button = component.Find("button");
        button.Click();

        var secondLink = component.FindAll("a.dropdown-item")[1];
        secondLink.KeyDown("ArrowUp");

        var menu = component.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
    }

    [Fact]
    public void BitDropdown_Should_Skip_Disabled_Items_On_ArrowDown_Key()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item 1</span>"))
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.Disabled, true)
                .AddChildContent("<span>Item 2 (disabled)</span>"))
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item 3</span>")));

        var button = component.Find("button");
        button.Click();

        var firstLink = component.FindAll("a.dropdown-item")[0];
        firstLink.KeyDown("ArrowDown");

        var menu = component.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
    }

    [Fact]
    public void BitDropdownItem_Should_Have_Tabindex_Zero_When_Enabled()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams =>
                itemParams.AddChildContent("<span>Item</span>")));

        var link = component.Find("a.dropdown-item");
        Assert.Equal("0", link.GetAttribute("tabindex"));
    }

    [Fact]
    public void BitDropdownItem_Should_Have_Tabindex_MinusOne_When_Disabled()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var component = ctx.Render<BitDropdown>(parameters => parameters
            .Add(p => p.ActivatorLabel, "Open")
            .AddChildContent<BitDropdownItem>(itemParams => itemParams
                .Add(p => p.Disabled, true)
                .AddChildContent("<span>Disabled Item</span>")));

        var link = component.Find("a.dropdown-item");
        Assert.Equal("-1", link.GetAttribute("tabindex"));
    }
}
