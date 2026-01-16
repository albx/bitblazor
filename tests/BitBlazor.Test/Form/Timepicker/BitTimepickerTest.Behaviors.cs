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

            using var ctx = new BunitContext();
            var component = ctx.Render<BitTimepicker>(parameters => parameters
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
}
