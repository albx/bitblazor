using System.Text.Json;
using System.Text.Json.Nodes;
using BitBlazor.Components;
using Bunit;

namespace BitBlazor.Test.Components.Tooltip;

public class BitTooltipTest
{
    private const string ModulePath = "./_content/BitBlazor/Components/Tooltip/BitTooltip.razor.js";

    private static JsonNode CaptureOptions(Action<ComponentParameterCollectionBuilder<BitTooltip>> configure)
    {
        using var ctx = new BunitContext();
        var module = ctx.JSInterop.SetupModule(ModulePath);
        var handle = module.SetupModule("BitTooltip.create", _ => true);
        handle.SetupVoid("dispose").SetVoidResult();

        ctx.Render<BitTooltip>(parameters =>
        {
            parameters.Add(p => p.Text, "Text");
            configure(parameters);
        });

        var invocation = module.Invocations["BitTooltip.create"].Single();
        return JsonSerializer.SerializeToNode(invocation.Arguments[2])!;
    }

    [Fact]
    public async Task BitTooltip_Should_Dispose_Instance_Handle_When_Disposed()
    {
        await using var ctx = new BunitContext();
        var module = ctx.JSInterop.SetupModule(ModulePath);
        var handle = module.SetupModule("BitTooltip.create", _ => true);
        handle.SetupVoid("dispose").SetVoidResult();

        ctx.Render<BitTooltip>(p => p.Add(c => c.Text, "Text"));
        Assert.Empty(handle.Invocations["dispose"]);

        await ctx.DisposeComponentsAsync();

        Assert.Single(handle.Invocations["dispose"]);
    }

    [Fact]
    public void BitTooltip_Should_Send_Default_Options_To_Interop()
    {
        var options = CaptureOptions(_ => { });

        Assert.Equal("top", (string?)options["placement"]);
        Assert.Equal("hover", (string?)options["trigger"]);
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
    public void BitTooltip_Should_Map_Parameters_To_Interop_Options()
    {
        var options = CaptureOptions(p => p
            .Add(c => c.Placement, TooltipPlacement.Bottom)
            .Add(c => c.Trigger, TooltipTrigger.Click)
            .Add(c => c.HtmlEnabled, true)
            .Add(c => c.Animation, false)
            .Add(c => c.Sanitize, false)
            .Add(c => c.DelayShow, 200)
            .Add(c => c.DelayHide, 100)
            .Add(c => c.OffsetSkidding, 5)
            .Add(c => c.OffsetDistance, 10)
            .Add(c => c.FallbackPlacements, [TooltipPlacement.Left, TooltipPlacement.Auto])
            .Add(c => c.CssClass, "my-tooltip"));

        Assert.Equal("bottom", (string?)options["placement"]);
        Assert.Equal("click", (string?)options["trigger"]);
        Assert.True((bool)options["html"]!);
        Assert.False((bool)options["animation"]!);
        Assert.False((bool)options["sanitize"]!);
        Assert.Equal(200, (int)options["delay"]!["show"]!);
        Assert.Equal(100, (int)options["delay"]!["hide"]!);
        Assert.Equal("5,10", (string?)options["offset"]);
        Assert.Equal(["left", "auto"], options["fallbackPlacements"]!.AsArray().Select(n => (string?)n));
        Assert.Equal("my-tooltip", (string?)options["customClass"]);
    }
}
