using BitBlazor.Form;
using Bunit;
using System.Globalization;

namespace BitBlazor.Test.Form.Timepicker;

public class BitTimepickerTest
{
    [Fact]
    public void BitTimepicker_Should_Set_Timeonly_Value_Correctly()
    {
        // Set culture explicitly to avoid date format ambiguity
        var originalCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("en-US");

        try
        {
            TimeOnly? value = null;

            using var ctx = new TestContext();
            var component = ctx.RenderComponent<BitTimepicker<TimeOnly?>>(parameters => parameters
                .Add(p => p.Label, "Label")
                .Add(p => p.Id, "test-timepicker")
                .Bind(p => p.Value, value, v => value = v));

            var newValue = new TimeOnly(12, 0);

            var inputDate = component.Find("#test-timepicker");
            inputDate.Change(newValue);

            Assert.Equal(newValue, value);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [Fact]
    public void BitTimepicker_Should_Throw_NotSupportedException_If_Specified_Type_Is_Not_Supported()
    {
        TimeSpan? value = null;

        using var ctx = new TestContext();

        var ex = Assert.Throws<NotSupportedException>(() =>
        {
            ctx.RenderComponent<BitTimepicker<TimeSpan?>>(parameters => parameters
                .Add(p => p.Label, "Label")
                .Add(p => p.Id, "test-timepicker")
                .Bind(p => p.Value, value, v => value = v));
        });

        Assert.Equal("Type not supported", ex.Message);
    }
}
