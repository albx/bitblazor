using BitBlazor.Form;
using Bunit;

namespace BitBlazor.Test.Form.SelectField;

public class BitSelectFieldTest
{
    enum TestEnum
    {
        Option1,
        Option2,
        Option3
    }

    [Fact]
    public void BitSelectField_Should_Change_String_Value_Correctly()
    {
        string? value = string.Empty;

        using var ctx = new BunitContext();

        var component = ctx.Render<BitSelectField<string>>(parameters => parameters
            .Add(p => p.Label, "Select a value")
            .Add(p => p.Id, "test-select")
            .Bind(p => p.Value, value!, v => value = v)
            .AddChildContent<BitSelectItem<string>>(child => child
                .Add(p => p.Value, string.Empty)
                .AddChildContent("Choose an option"))
            .AddChildContent<BitSelectItem<string>>(child => child
                .Add(p => p.Value, "value1")
                .AddChildContent("Value 1"))
            .AddChildContent<BitSelectItem<string>>(child => child
                .Add(p => p.Value, "value2")
                .AddChildContent("Value 2")));

        var select = component.Find("select");
        select.Change("value1");

        Assert.Equal("value1", value);
    }

    [Fact]
    public void BitSelectField_Should_Change_Int_Value_Correctly()
    {
        int value = 0;

        using var ctx = new BunitContext();

        var component = ctx.Render<BitSelectField<int>>(parameters => parameters
            .Add(p => p.Label, "Select a number")
            .Add(p => p.Id, "test-select")
            .Bind(p => p.Value, value, v => value = v)
            .AddChildContent<BitSelectItem<int>>(child => child
                .Add(p => p.Value, 0)
                .AddChildContent("Choose a number"))
            .AddChildContent<BitSelectItem<int>>(child => child
                .Add(p => p.Value, 1)
                .AddChildContent("One"))
            .AddChildContent<BitSelectItem<int>>(child => child
                .Add(p => p.Value, 2)
                .AddChildContent("Two"))
            .AddChildContent<BitSelectItem<int>>(child => child
                .Add(p => p.Value, 3)
                .AddChildContent("Three")));

        var select = component.Find("select");
        select.Change(2);

        Assert.Equal(2, value);
    }

    [Fact]
    public void BitSelectField_Should_Change_Enum_Value_Correctly()
    {
        TestEnum value = TestEnum.Option1;

        using var ctx = new BunitContext();

        var component = ctx.Render<BitSelectField<TestEnum>>(parameters => parameters
            .Add(p => p.Label, "Select an option")
            .Add(p => p.Id, "test-select")
            .Bind(p => p.Value, value, v => value = v)
            .AddChildContent<BitSelectItem<TestEnum>>(child => child
                .Add(p => p.Value, TestEnum.Option1)
                .AddChildContent("Option 1"))
            .AddChildContent<BitSelectItem<TestEnum>>(child => child
                .Add(p => p.Value, TestEnum.Option2)
                .AddChildContent("Option 2"))
            .AddChildContent<BitSelectItem<TestEnum>>(child => child
                .Add(p => p.Value, TestEnum.Option3)
                .AddChildContent("Option 3")));

        var select = component.Find("select");
        select.Change(TestEnum.Option3);

        Assert.Equal(TestEnum.Option3, value);
    }

    [Fact]
    public void BitSelectField_Should_Update_Bound_Value_When_Selection_Changes()
    {
        string? value = "initial";

        using var ctx = new BunitContext();

        var component = ctx.Render<BitSelectField<string>>(parameters => parameters
            .Add(p => p.Label, "Select a value")
            .Add(p => p.Id, "test-select")
            .Bind(p => p.Value, value!, v => value = v)
            .AddChildContent<BitSelectItem<string>>(child => child
                .Add(p => p.Value, "initial")
                .AddChildContent("Initial Value"))
            .AddChildContent<BitSelectItem<string>>(child => child
                .Add(p => p.Value, "changed")
                .AddChildContent("Changed Value")));

        Assert.Equal("initial", value);

        var select = component.Find("select");
        select.Change("changed");

        Assert.Equal("changed", value);
    }

    [Fact]
    public void BitSelectField_Should_Set_Selected_Attribute_On_Correct_Option()
    {
        string? value = "value2";

        using var ctx = new BunitContext();

        var component = ctx.Render<BitSelectField<string>>(parameters => parameters
            .Add(p => p.Label, "Select a value")
            .Add(p => p.Id, "test-select")
            .Bind(p => p.Value, value!, v => value = v)
            .AddChildContent<BitSelectItem<string>>(child => child
                .Add(p => p.Value, "value1")
                .AddChildContent("Value 1"))
            .AddChildContent<BitSelectItem<string>>(child => child
                .Add(p => p.Value, "value2")
                .AddChildContent("Value 2"))
            .AddChildContent<BitSelectItem<string>>(child => child
                .Add(p => p.Value, "value3")
                .AddChildContent("Value 3")));

        var options = component.FindAll("option");
        var selectedOption = options.FirstOrDefault(o => o.HasAttribute("selected"));

        Assert.NotNull(selectedOption);
        Assert.Equal("value2", selectedOption.GetAttribute("value"));
    }

    [Fact]
    public void BitSelectField_Should_Update_Selected_Attribute_When_Value_Changes()
    {
        int value = 1;

        using var ctx = new BunitContext();

        var component = ctx.Render<BitSelectField<int>>(parameters => parameters
            .Add(p => p.Label, "Select a number")
            .Add(p => p.Id, "test-select")
            .Bind(p => p.Value, value, v => value = v)
            .AddChildContent<BitSelectItem<int>>(child => child
                .Add(p => p.Value, 1)
                .AddChildContent("One"))
            .AddChildContent<BitSelectItem<int>>(child => child
                .Add(p => p.Value, 2)
                .AddChildContent("Two"))
            .AddChildContent<BitSelectItem<int>>(child => child
                .Add(p => p.Value, 3)
                .AddChildContent("Three")));

        var select = component.Find("select");
        select.Change(3);

        var options = component.FindAll("option");
        var selectedOption = options.FirstOrDefault(o => o.HasAttribute("selected"));

        Assert.NotNull(selectedOption);
        Assert.Equal("3", selectedOption.GetAttribute("value"));
    }

    [Fact]
    public void BitSelectField_Should_Handle_Nullable_Int_Value_Changes()
    {
        int? value = null;

        using var ctx = new BunitContext();

        var component = ctx.Render<BitSelectField<int?>>(parameters => parameters
            .Add(p => p.Label, "Select a number")
            .Add(p => p.Id, "test-select")
            .Bind(p => p.Value, value, v => value = v)
            .AddChildContent<BitSelectItem<int?>>(child => child
                .Add(p => p.Value, (int?)null)
                .AddChildContent("None"))
            .AddChildContent<BitSelectItem<int?>>(child => child
                .Add(p => p.Value, 1)
                .AddChildContent("One"))
            .AddChildContent<BitSelectItem<int?>>(child => child
                .Add(p => p.Value, 2)
                .AddChildContent("Two")));

        var select = component.Find("select");
        select.Change(1);

        Assert.Equal(1, value);
    }

    [Fact]
    public void BitSelectField_Should_Handle_Nullable_Enum_Value_Changes()
    {
        TestEnum? value = null;

        using var ctx = new BunitContext();

        var component = ctx.Render<BitSelectField<TestEnum?>>(parameters => parameters
            .Add(p => p.Label, "Select an option")
            .Add(p => p.Id, "test-select")
            .Bind(p => p.Value, value, v => value = v)
            .AddChildContent<BitSelectItem<TestEnum?>>(child => child
                .Add(p => p.Value, (TestEnum?)null)
                .AddChildContent("None"))
            .AddChildContent<BitSelectItem<TestEnum?>>(child => child
                .Add(p => p.Value, TestEnum.Option1)
                .AddChildContent("Option 1"))
            .AddChildContent<BitSelectItem<TestEnum?>>(child => child
                .Add(p => p.Value, TestEnum.Option2)
                .AddChildContent("Option 2")));

        var select = component.Find("select");
        select.Change(TestEnum.Option2);

        Assert.Equal(TestEnum.Option2, value);
    }
}
