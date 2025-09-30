using BitBlazor.Form;
using Bunit;

namespace BitBlazor.Test.Form.NumberField;

public class BitNumberFieldTest
{
    #region Increment tests
    [Fact]
    public void BitNumberField_Should_Increment_Int_Value_As_Expected()
    {
        int? value = 0;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<int?>>(parameters => parameters
            .Add(p => p.Label, "Int value")
            .Bind(p => p.Value, value, v => value = v));

        var incrementButton = component.Find("button.input-number-add");
        incrementButton.Click();

        Assert.Equal(1, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Increment_Long_Value_As_Expected()
    {
        long? value = 0;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<long?>>(parameters => parameters
            .Add(p => p.Label, "Long value")
            .Bind(p => p.Value, value, v => value = v));

        var incrementButton = component.Find("button.input-number-add");
        incrementButton.Click();

        Assert.Equal(1, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Increment_Short_Value_As_Expected()
    {
        short? value = 0;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<short?>>(parameters => parameters
            .Add(p => p.Label, "Short value")
            .Bind(p => p.Value, value, v => value = v));

        var incrementButton = component.Find("button.input-number-add");
        incrementButton.Click();

        Assert.Equal((short)1, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Increment_Float_Value_As_Expected()
    {
        float? value = 0.0f;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<float?>>(parameters => parameters
            .Add(p => p.Label, "Float value")
            .Bind(p => p.Value, value, v => value = v));

        var incrementButton = component.Find("button.input-number-add");
        incrementButton.Click();

        Assert.Equal(1.0f, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Increment_Double_Value_As_Expected()
    {
        double? value = 0.0;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<double?>>(parameters => parameters
            .Add(p => p.Label, "Double value")
            .Bind(p => p.Value, value, v => value = v));

        var incrementButton = component.Find("button.input-number-add");
        incrementButton.Click();

        Assert.Equal(1.0, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Increment_Decimal_Value_As_Expected()
    {
        decimal? value = 0;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<decimal?>>(parameters => parameters
            .Add(p => p.Label, "Decimal value")
            .Bind(p => p.Value, value, v => value = v));

        var incrementButton = component.Find("button.input-number-add");
        incrementButton.Click();

        Assert.Equal((decimal)1.0, component.Instance.Value);
    }
    #endregion

    #region Decrement tests
    [Fact]
    public void BitNumberField_Should_Decrement_Int_Value_As_Expected()
    {
        int? value = 1;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<int?>>(parameters => parameters
            .Add(p => p.Label, "Int value")
            .Bind(p => p.Value, value, v => value = v));

        var decrementButton = component.Find("button.input-number-sub");
        decrementButton.Click();

        Assert.Equal(0, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Decrement_Long_Value_As_Expected()
    {
        long? value = 1;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<long?>>(parameters => parameters
            .Add(p => p.Label, "Long value")
            .Bind(p => p.Value, value, v => value = v));

        var decrementButton = component.Find("button.input-number-sub");
        decrementButton.Click();

        Assert.Equal(0, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Decrement_Short_Value_As_Expected()
    {
        short? value = 1;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<short?>>(parameters => parameters
            .Add(p => p.Label, "Short value")
            .Bind(p => p.Value, value, v => value = v));

        var decrementButton = component.Find("button.input-number-sub");
        decrementButton.Click();

        Assert.Equal((short)0, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Decrement_Float_Value_As_Expected()
    {
        float? value = 1.0f;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<float?>>(parameters => parameters
            .Add(p => p.Label, "Float value")
            .Bind(p => p.Value, value, v => value = v));

        var decrementButton = component.Find("button.input-number-sub");
        decrementButton.Click();

        Assert.Equal(0.0f, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Decrement_Double_Value_As_Expected()
    {
        double? value = 1.0;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<double?>>(parameters => parameters
            .Add(p => p.Label, "Double value")
            .Bind(p => p.Value, value, v => value = v));

        var decrementButton = component.Find("button.input-number-sub");
        decrementButton.Click();

        Assert.Equal(0.0, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Decrement_Decimal_Value_As_Expected()
    {
        decimal? value = 1;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<decimal?>>(parameters => parameters
            .Add(p => p.Label, "Decimal value")
            .Bind(p => p.Value, value, v => value = v));

        var decrementButton = component.Find("button.input-number-sub");
        decrementButton.Click();

        Assert.Equal((decimal)0.0, component.Instance.Value);
    }
    #endregion

    #region Min, Max & step tests
    [Fact]
    public void BitNumberField_Should_Return_Max_Value_If_Incremented_Value_Is_Greater_Than_Max()
    {
        int? value = 10;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<int?>>(parameters => parameters
            .Add(p => p.Label, "Test")
            .Add(p => p.Max, 10)
            .Bind(p => p.Value, value, v => value = v));

        var incrementButton = component.Find("button.input-number-add");
        incrementButton.Click();

        Assert.Equal(10, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Return_Min_Value_If_Decremented_Value_Is_Less_Than_Min()
    {
        int? value = 1;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<int?>>(parameters => parameters
            .Add(p => p.Label, "Test")
            .Add(p => p.Min, 1)
            .Bind(p => p.Value, value, v => value = v));

        var decrementButton = component.Find("button.input-number-sub");
        decrementButton.Click();

        Assert.Equal(1, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Increment_Value_Based_On_Specified_Step()
    {
        int? value = 0;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<int?>>(parameters => parameters
            .Add(p => p.Label, "Test")
            .Add(p => p.Step, 5)
            .Bind(p => p.Value, value, v => value = v));

        var incrementButton = component.Find("button.input-number-add");
        incrementButton.Click();

        Assert.Equal(5, component.Instance.Value);
    }

    [Fact]
    public void BitNumberField_Should_Decrement_Value_Based_On_Specified_Step()
    {
        int? value = 10;
        using var ctx = new TestContext();

        var component = ctx.RenderComponent<BitNumberField<int?>>(parameters => parameters
            .Add(p => p.Label, "Test")
            .Add(p => p.Step, 5)
            .Bind(p => p.Value, value, v => value = v));

        var decrementButton = component.Find("button.input-number-sub");
        decrementButton.Click();

        Assert.Equal(5, component.Instance.Value);
    }
    #endregion
}
