using BitBlazor.Components;
using Bunit;

namespace BitBlazor.Test.Components.Alert;

public class BitAlertTest
{
    [Fact]
    public void BitAlert_Should_Call_OnClose_Event_When_Close_Button_Is_Clicked()
    {
        using var ctx = new TestContext();

        bool closingAlert = false;
        Action onAlertClose = () => closingAlert = true;

        var component = ctx.RenderComponent<BitAlert>(
            parameters => parameters
                .Add(a => a.Type, AlertType.Primary)
                .Add(a => a.ChildContent, "Dismissible alert")
                .Add(a => a.Dismissible, true)
                .Add(a => a.OnClose, onAlertClose));

        var closeButton = component.Find("button.btn-close");
        closeButton.Click();

        Assert.True(closingAlert);
    }

    [Fact]
    public void BitAlert_Should_Call_OnClosed_Event_When_Close_Button_Is_Clicked()
    {
        using var ctx = new TestContext();

        bool closedAlert = false;
        Action onAlertClosed = () => closedAlert = true;

        var component = ctx.RenderComponent<BitAlert>(
            parameters => parameters
                .Add(a => a.Type, AlertType.Primary)
                .Add(a => a.ChildContent, "Dismissible alert")
                .Add(a => a.Dismissible, true)
                .Add(a => a.OnClosed, onAlertClosed));

        var closeButton = component.Find("button.btn-close");
        closeButton.Click();

        Assert.True(closedAlert);
    }
}
