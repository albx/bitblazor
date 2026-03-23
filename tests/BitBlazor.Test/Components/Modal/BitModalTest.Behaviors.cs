using BitBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace BitBlazor.Test.Components.Modal;

public class BitModalTest
{
    private static RenderFragment SimpleBody =>
        builder => builder.AddContent(0, "Body content");

    [Fact]
    public void BitModal_Should_Invoke_IsVisibleChanged_With_False_When_Close_Button_Is_Clicked()
    {
        using var ctx = new BunitContext();

        bool newVisibility = true;
        bool isVisibleChangedCalled = false;

        var component = ctx.Render<BitModal>(parameters => parameters
            .Bind(p => p.IsVisible, newVisibility, v => { isVisibleChangedCalled = true; newVisibility = v; })
            .Add(p => p.BodyContent, SimpleBody));

        component.Find("button.btn-close").Click();

        Assert.True(isVisibleChangedCalled);
        Assert.False(newVisibility);
    }

    [Fact]
    public void BitModal_Should_Invoke_OnClose_When_Close_Button_Is_Clicked()
    {
        using var ctx = new BunitContext();

        bool onCloseCalled = false;

        var component = ctx.Render<BitModal>(parameters => parameters
            .Bind(p => p.IsVisible, true, v => { })
            .Add(p => p.BodyContent, SimpleBody)
            .Add(p => p.OnClose, () => onCloseCalled = true));

        component.Find("button.btn-close").Click();

        Assert.True(onCloseCalled);
    }

    [Fact]
    public void BitModal_Should_Invoke_IsVisibleChanged_With_False_When_Backdrop_Is_Clicked()
    {
        using var ctx = new BunitContext();

        bool newVisibility = true;

        var component = ctx.Render<BitModal>(parameters => parameters
            .Bind(p => p.IsVisible, newVisibility, v => newVisibility = v)
            .Add(p => p.Backdrop, ModalBackdrop.Default)
            .Add(p => p.BodyContent, SimpleBody));

        component.Find(".modal-backdrop").Click();

        Assert.False(newVisibility);
    }

    [Fact]
    public void BitModal_Should_Not_Invoke_IsVisibleChanged_When_Static_Backdrop_Is_Clicked()
    {
        using var ctx = new BunitContext();

        bool isVisibleChangedCalled = false;

        var component = ctx.Render<BitModal>(parameters => parameters
            .Bind(p => p.IsVisible, true, v => isVisibleChangedCalled = true)
            .Add(p => p.Backdrop, ModalBackdrop.Static)
            .Add(p => p.BodyContent, SimpleBody));

        component.Find(".modal-backdrop").Click();

        Assert.False(isVisibleChangedCalled);
    }
}
