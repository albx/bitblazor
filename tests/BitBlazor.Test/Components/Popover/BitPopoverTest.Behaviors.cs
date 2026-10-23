using System.Text.Json;
using System.Text.Json.Nodes;
using BitBlazor.Components;
using Bunit;

namespace BitBlazor.Test.Components.Popover;

public class BitPopoverTest
{
    private static JsonNode CaptureOptions(Action<ComponentParameterCollectionBuilder<BitPopover>> configure)
    {
        using var ctx = new BunitContext();
        var module = ctx.JSInterop.SetupModule("./_content/BitBlazor/js/bitblazor-interop.js");
        module.SetupVoid("initPopoverOnFirstChild", _ => true).SetVoidResult();
        module.SetupVoid("disposePopoverOnFirstChild", _ => true).SetVoidResult();

        ctx.Render<BitPopover>(parameters =>
        {
            parameters.Add(p => p.Content, "Content");
            configure(parameters);
        });

        var invocation = module.Invocations["initPopoverOnFirstChild"].Single();
        return JsonSerializer.SerializeToNode(invocation.Arguments[3])!;
    }

    [Fact]
    public void BitPopover_Should_Send_Default_Options_To_Interop()
    {
        var options = CaptureOptions(_ => { });

        Assert.Equal("top", (string?)options["placement"]);
        Assert.Equal("click", (string?)options["trigger"]);
        Assert.True((bool)options["animation"]!);
        Assert.True((bool)options["sanitize"]!);
        Assert.False((bool)options["html"]!);
        Assert.Equal("0,0", (string?)options["offset"]);
        Assert.Equal(0, (int)options["delay"]!["show"]!);
        Assert.Equal(0, (int)options["delay"]!["hide"]!);
        Assert.Equal(["top", "right", "bottom", "left"], options["fallbackPlacements"]!.AsArray().Select(n => (string?)n));
        Assert.Null(options["customClass"]);
    }

    [Fact]
    public void BitPopover_Should_Map_Parameters_To_Interop_Options()
    {
        var options = CaptureOptions(p => p
            .Add(c => c.Placement, PopoverPlacement.Bottom)
            .Add(c => c.Trigger, PopoverTrigger.Hover)
            .Add(c => c.HtmlEnabled, true)
            .Add(c => c.Animation, false)
            .Add(c => c.Sanitize, false)
            .Add(c => c.DelayShow, 200)
            .Add(c => c.DelayHide, 100)
            .Add(c => c.OffsetSkidding, 5)
            .Add(c => c.OffsetDistance, 10)
            .Add(c => c.FallbackPlacements, [PopoverPlacement.Left, PopoverPlacement.Auto])
            .Add(c => c.CssClass, "my-popover"));

        Assert.Equal("bottom", (string?)options["placement"]);
        Assert.Equal("hover", (string?)options["trigger"]);
        Assert.True((bool)options["html"]!);
        Assert.False((bool)options["animation"]!);
        Assert.False((bool)options["sanitize"]!);
        Assert.Equal(200, (int)options["delay"]!["show"]!);
        Assert.Equal(100, (int)options["delay"]!["hide"]!);
        Assert.Equal("5,10", (string?)options["offset"]);
        Assert.Equal(["left", "auto"], options["fallbackPlacements"]!.AsArray().Select(n => (string?)n));
        Assert.Equal("my-popover", (string?)options["customClass"]);
    }
}
