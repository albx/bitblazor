using BitBlazor.Components;
using BitBlazor.Utilities;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace BitBlazor.Test.Components.Toolbar;

public class BitToolbarTest
{
    [Fact]
    public void BitToolbar_Should_Handle_Item_Click_Event()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

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

    [Fact]
    public void BitToolbar_Should_Not_Fire_OnClick_When_Item_Is_Disabled()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        bool clicked = false;
        Action onItemClick = () => clicked = true;

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarItem>(childParameters =>
            {
                childParameters.Add(p => p.Label, "Item 1");
                childParameters.Add(p => p.IconName, Icons.ItComment);
                childParameters.Add(p => p.Disabled, true);
                childParameters.Add(p => p.OnClick, onItemClick);
            })
        );

        cut.Find("a").Click();

        Assert.False(clicked);
    }

    [Fact]
    public void BitToolbar_Should_Navigate_When_Href_Is_Provided_And_No_OnClick()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarItem>(childParameters =>
            {
                childParameters.Add(p => p.Label, "Item 1");
                childParameters.Add(p => p.IconName, Icons.ItComment);
                childParameters.Add(p => p.Href, "/settings");
            })
        );

        cut.Find("a").Click();

        var navManager = ctx.Services.GetRequiredService<NavigationManager>();
        Assert.Equal("http://localhost/settings", navManager.Uri);
    }

    [Fact]
    public void BitToolbar_Should_Not_Navigate_When_Neither_Href_Nor_OnClick_Is_Set()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var navManager = ctx.Services.GetRequiredService<NavigationManager>();
        var initialUri = navManager.Uri;

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarItem>(childParameters =>
            {
                childParameters.Add(p => p.Label, "Item 1");
                childParameters.Add(p => p.IconName, Icons.ItComment);
            })
        );

        cut.Find("a").Click();

        Assert.Equal(initialUri, navManager.Uri);
    }

    [Fact]
    public void BitToolbar_Should_Prefer_OnClick_Over_Href_When_Both_Are_Set()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var navManager = ctx.Services.GetRequiredService<NavigationManager>();
        var initialUri = navManager.Uri;

        bool clicked = false;
        Action onItemClick = () => clicked = true;

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarItem>(childParameters =>
            {
                childParameters.Add(p => p.Label, "Item 1");
                childParameters.Add(p => p.IconName, Icons.ItComment);
                childParameters.Add(p => p.Href, "/settings");
                childParameters.Add(p => p.OnClick, onItemClick);
            })
        );

        cut.Find("a").Click();

        Assert.True(clicked);
        Assert.Equal(initialUri, navManager.Uri);
    }

    [Fact]
    public void BitToolbar_Should_Activate_Item_On_Enter_Key()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        bool clicked = false;
        Action onItemClick = () => clicked = true;

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarItem>(childParameters =>
            {
                childParameters.Add(p => p.Label, "Item 1");
                childParameters.Add(p => p.IconName, Icons.ItComment);
                childParameters.Add(p => p.OnClick, onItemClick);
            })
        );

        cut.Find("a").KeyDown(new KeyboardEventArgs { Key = "Enter" });

        Assert.True(clicked);
    }

    [Fact]
    public void BitToolbar_Should_Activate_Item_On_Space_Key()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        bool clicked = false;
        Action onItemClick = () => clicked = true;

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarItem>(childParameters =>
            {
                childParameters.Add(p => p.Label, "Item 1");
                childParameters.Add(p => p.IconName, Icons.ItComment);
                childParameters.Add(p => p.OnClick, onItemClick);
            })
        );

        cut.Find("a").KeyDown(new KeyboardEventArgs { Key = " " });

        Assert.True(clicked);
    }

    [Fact]
    public void BitToolbar_Should_Not_Activate_Item_On_Key_When_Disabled()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        bool clicked = false;
        Action onItemClick = () => clicked = true;

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarItem>(childParameters =>
            {
                childParameters.Add(p => p.Label, "Item 1");
                childParameters.Add(p => p.IconName, Icons.ItComment);
                childParameters.Add(p => p.Disabled, true);
                childParameters.Add(p => p.OnClick, onItemClick);
            })
        );

        cut.Find("a").KeyDown(new KeyboardEventArgs { Key = "Enter" });

        Assert.False(clicked);
    }

    [Fact]
    public void BitToolbarItem_Should_Throw_InvalidOperationException_When_Used_Outside_BitToolbar()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ctx.Render<BitToolbarItem>(parameters => parameters
                .Add(p => p.Label, "Item 1")
                .Add(p => p.IconName, Icons.ItComment)));

        Assert.Equal("A toolbar item must be only used inside a BitToolbar component", exception.Message);
    }

    [Fact]
    public void BitToolbarDivider_Should_Throw_InvalidOperationException_When_Used_Outside_BitToolbar()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ctx.Render<BitToolbarDivider>());

        Assert.Equal("BitToolbarDivider component must be used inside a BitToolbar component", exception.Message);
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Open_On_Activator_Click()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>")))
        );

        var button = cut.Find("button");
        button.Click();

        var menu = cut.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
        Assert.Equal("true", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Close_On_Activator_Click_Again()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>")))
        );

        var button = cut.Find("button");
        button.Click();
        button.Click();

        var menu = cut.Find("div.dropdown-menu");
        Assert.False(menu.ClassList.Contains("show"));
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Open_And_Focus_First_Item_On_ArrowDown_Key()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>")))
        );

        var button = cut.Find("button");
        button.KeyDown(new KeyboardEventArgs { Key = "ArrowDown" });

        var menu = cut.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
        Assert.Equal("true", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Open_On_ArrowUp_Key()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>")))
        );

        var button = cut.Find("button");
        button.KeyDown(new KeyboardEventArgs { Key = "ArrowUp" });

        var menu = cut.Find("div.dropdown-menu");
        Assert.Contains("show", menu.ClassList);
        Assert.Equal("true", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Toggle_Independently_When_Multiple_Present()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>")))
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Images")
                .Add(p => p.IconName, Icons.ItCamera)
                .Add(p => p.Id, "dropdown2")
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 2</span>")))
        );

        cut.FindAll("button")[0].Click();

        var buttons = cut.FindAll("button");
        var menus = cut.FindAll("div.dropdown-menu");
        Assert.Contains("show", menus[0].ClassList);
        Assert.False(menus[1].ClassList.Contains("show"));
        Assert.Equal("true", buttons[0].GetAttribute("aria-expanded"));
        Assert.Equal("false", buttons[1].GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Throw_InvalidOperationException_When_Used_Outside_BitToolbar()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var exception = Assert.Throws<InvalidOperationException>(() =>
            ctx.Render<BitToolbarDropdownItem>(parameters => parameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>"))));

        Assert.Equal("A toolbar item must be only used inside a BitToolbar component", exception.Message);
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Render_Native_Disabled_State_On_Activator()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .Add(p => p.Disabled, true)
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>")))
        );

        var button = cut.Find("button");

        Assert.True(button.HasAttribute("disabled"));
        Assert.Equal("true", button.GetAttribute("aria-disabled"));
        Assert.Contains("disabled", button.ClassList);
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Not_Open_On_Activator_Click_When_Disabled()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .Add(p => p.Disabled, true)
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>")))
        );

        var button = cut.Find("button");
        button.Click();

        var menu = cut.Find("div.dropdown-menu");
        Assert.False(menu.ClassList.Contains("show"));
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void BitToolbarDropdownItem_Should_Not_Open_On_ArrowDown_Key_When_Disabled()
    {
        using var ctx = new BunitContext();
        ctx.SetRendererInfo(new RendererInfo("InteractiveServer", isInteractive: true));

        var cut = ctx.Render<BitToolbar>(parameters => parameters
            .AddChildContent<BitToolbarDropdownItem>(itemParameters => itemParameters
                .Add(p => p.Label, "Messages")
                .Add(p => p.IconName, Icons.ItComment)
                .Add(p => p.Id, "dropdown1")
                .Add(p => p.Disabled, true)
                .AddChildContent<BitDropdownItem>(dropdownItemParameters =>
                    dropdownItemParameters.AddChildContent("<span>Label 1</span>")))
        );

        var button = cut.Find("button");
        button.KeyDown(new KeyboardEventArgs { Key = "ArrowDown" });

        var menu = cut.Find("div.dropdown-menu");
        Assert.False(menu.ClassList.Contains("show"));
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
    }
}
