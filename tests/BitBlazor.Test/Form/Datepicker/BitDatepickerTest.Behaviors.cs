using BitBlazor.Form;
using Bunit;
using System.Globalization;

namespace BitBlazor.Test.Form.Datepicker;

public class BitDatepickerTest
{
    [Fact]
    public void BitDatepicker_Should_Set_Datetime_Value_Correctly()
    {
        // Set culture explicitly to avoid date format ambiguity
        var originalCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        
        try
        {
            DateTime? value = null;

            using var ctx = new BunitContext();
            var component = ctx.Render<BitDatepicker<DateTime?>>(parameters => parameters
                .Add(p => p.Label, "Label")
                .Add(p => p.Id, "test-datepicker")
                .Bind(p => p.Value, value, v => value = v));

            var newValue = new DateTime(2025, 12, 1);

            var inputDate = component.Find("#test-datepicker");
            inputDate.Change(newValue);

            Assert.Equal(newValue, value);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [Fact]
    public void BitDatepicker_Should_Set_Dateonly_Value_Correctly()
    {
        // Set culture explicitly to avoid date format ambiguity
        var originalCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        
        try
        {
            DateOnly? value = null;

            using var ctx = new BunitContext();
            var component = ctx.Render<BitDatepicker<DateOnly?>>(parameters => parameters
                .Add(p => p.Label, "Label")
                .Add(p => p.Id, "test-datepicker")
                .Bind(p => p.Value, value, v => value = v));

            var newValue = new DateOnly(2025, 12, 1);

            var inputDate = component.Find("#test-datepicker");
            inputDate.Change(newValue);

            Assert.Equal(newValue, value);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [Fact]
    public void BitDatepicker_Should_Throw_NotSupportedException_If_Specified_Type_Is_Not_Supported()
    {
        string? value = null;

        using var ctx = new BunitContext();

        var ex = Assert.Throws<NotSupportedException>(() =>
        {
            ctx.Render<BitDatepicker<string?>>(parameters => parameters
                .Add(p => p.Label, "Label")
                .Add(p => p.Id, "test-datepicker")
                .Bind(p => p.Value, value, v => value = v));
        });

        Assert.Equal("Type not supported", ex.Message);
    }
}
