# Testing with bUnit

Every component must have a corresponding test file. Tests live in `tests/BitBlazor.Test/` mirroring the `src/` structure.

## File Location and Naming

```
src/BitBlazor/Components/Alert/BitAlert.razor
→ tests/BitBlazor.Test/Components/Alert/BitAlertTest.cs
```

Split tests by concern using multiple files per component when needed:

- `BitXxxTest.Rendering.cs` — CSS class output, ARIA attribute, and markup tests
- `BitXxxTest.Behaviors.cs` — click, change, and event callback tests

## Example

```csharp
// tests/BitBlazor.Test/Components/Alert/BitAlertTest.cs
using BitBlazor.Components;
using Bunit;

namespace BitBlazor.Test.Components.Alert;

public class BitAlertTest
{
    [Fact]
    public void BitAlert_Should_Render_With_Correct_Color_Class()
    {
        using var ctx = new BunitContext();

        var component = ctx.Render<BitAlert>(
            parameters => parameters
                .Add(p => p.ChildContent, "This is an alert")
                .Add(p => p.Color, Color.Danger));

        Assert.True(component.Find("div").ClassList.Contains("alert-danger"));
    }

    [Fact]
    public void BitAlert_Should_Forward_Additional_Attributes()
    {
        using var ctx = new BunitContext();

        var component = ctx.Render<BitAlert>(
            parameters => parameters
                .Add(p => p.ChildContent, "Alert")
                .Add(p => p.Color, Color.Primary)
                .AddUnmatched("data-testid", "my-alert"));

        Assert.Equal("my-alert", component.Find("div").GetAttribute("data-testid"));
    }

    [Fact]
    public void BitAlert_Should_Apply_Custom_CssClass()
    {
        using var ctx = new BunitContext();

        var component = ctx.Render<BitAlert>(
            parameters => parameters
                .Add(p => p.ChildContent, "Alert")
                .Add(p => p.Color, Color.Primary)
                .Add(p => p.CssClass, "my-custom-class"));

        Assert.True(component.Find("div").ClassList.Contains("my-custom-class"));
    }
}
```

## Form Component ARIA Test

```csharp
[Fact]
public void BitCheckbox_Should_Set_AriaInvalid_When_Validation_Fails()
{
    using var ctx = new BunitContext();

    // Arrange: put the field into an invalid state via EditContext
    var model = new TestModel();
    var editContext = new EditContext(model);

    var component = ctx.Render<BitCheckbox>(
        parameters => parameters
            .Add(p => p.Label, "Accept terms")
            .Add(p => p.For, () => model.Accepted)
            .CascadeValue(editContext));

    // Simulate validation failure
    editContext.NotifyValidationStateChanged();

    // Assert
    var input = component.Find("input");
    Assert.Equal("true", input.GetAttribute("aria-invalid"));
    Assert.NotNull(input.GetAttribute("aria-describedby"));
}
```

## Conventions

- Use `BunitContext` (bUnit v2 API) — not the v1 `TestContext`
- Always test:
  - CSS class output for each relevant parameter combination
  - ARIA attributes (`aria-invalid`, `aria-label`, `role`, etc.)
  - `EventCallback` invocation (click, change, etc.)
  - `AdditionalAttributes` forwarding (`data-testid` pattern)
  - `CssClass` injection (consumer custom class appears on root element)
- Name tests: `BitXxx_Should_<expected>_When_<condition>`
- One test class per component; split into multiple files by concern when a component has many tests
