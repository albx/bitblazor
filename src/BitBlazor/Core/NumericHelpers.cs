using System.Globalization;

namespace BitBlazor.Core;

internal static class NumericHelpers<T>
{
    internal static T ChangeValue(T? value, T? min, T? max, T? step, int factor)
    {
        var valueType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

        if (!ValueChangers.TryGetValue(valueType, out var valueChanger))
        {
            throw new NotSupportedException($"Type {valueType} is not supported");
        }

        return valueChanger(value, min, max, step, factor);
    }

    internal static string? FormatValue(T? value)
    {
        if (value is IFormattable formattable)
        {
            return formattable.ToString("0.####", CultureInfo.InvariantCulture.NumberFormat);
        }

        return null;
    }

    private readonly static Dictionary<Type, Func<T?, T?, T?, T?, int, T>> ValueChangers = new()
    {
        [typeof(int)] = (value, min, max, step, factor) =>
        {
            int intValue = value is null ? 0 : Convert.ToInt32(value);
            int stepValue = step is null ? 1 : Convert.ToInt32(step);
            int newValue = intValue + factor * stepValue;

            if (min is not null && newValue < Convert.ToInt32(min))
            {
                newValue = Convert.ToInt32(min);
            }
            else if (max is not null && newValue > Convert.ToInt32(max))
            {
                newValue = Convert.ToInt32(max);
            }

            return (T)(object)newValue;
        },
        [typeof(long)] = (value, min, max, step, factor) =>
        {
            long longValue = value is null ? 0 : Convert.ToInt64(value);
            long stepValue = step is null ? 1 : Convert.ToInt64(step);
            long newValue = longValue + factor * stepValue;

            if (min is not null && newValue < Convert.ToInt64(min))
            {
                newValue = Convert.ToInt64(min);
            }
            else if (max is not null && newValue > Convert.ToInt64(max))
            {
                newValue = Convert.ToInt64(max);
            }

            return (T)(object)newValue;
        },
        [typeof(short)] = (value, min, max, step, factor) =>
        {
            short shortValue = value is null ? (short)0 : Convert.ToInt16(value);
            short stepValue = step is null ? (short)1 : Convert.ToInt16(step);
            short newValue = (short)(shortValue + factor * stepValue);

            if (min is not null && newValue < Convert.ToInt16(min))
            {
                newValue = Convert.ToInt16(min);
            }
            else if (max is not null && newValue > Convert.ToInt16(max))
            {
                newValue = Convert.ToInt16(max);
            }

            return (T)(object)newValue;
        },
        [typeof(float)] = (value, min, max, step, factor) =>
        {
            float floatValue = value is null ? 0f : Convert.ToSingle(value);
            float stepValue = step is null ? 1f : Convert.ToSingle(step);
            float newValue = floatValue + factor * stepValue;

            if (min is not null && newValue < Convert.ToSingle(min))
            {
                newValue = Convert.ToSingle(min);
            }
            else if (max is not null && newValue > Convert.ToSingle(max))
            {
                newValue = Convert.ToSingle(max);
            }

            return (T)(object)newValue;
        },
        [typeof(double)] = (value, min, max, step, factor) =>
        {
            double doubleValue = value is null ? 0 : Convert.ToDouble(value);
            double stepValue = step is null ? 1 : Convert.ToDouble(step);
            double newValue = doubleValue + factor * stepValue;

            if (min is not null && newValue < Convert.ToDouble(min))
            {
                newValue = Convert.ToDouble(min);
            }
            else if (max is not null && newValue > Convert.ToDouble(max))
            {
                newValue = Convert.ToDouble(max);
            }

            return (T)(object)newValue;
        },
        [typeof(decimal)] = (value, min, max, step, factor) =>
        {
            decimal decimalValue = value is null ? 0 : Convert.ToDecimal(value);
            decimal stepValue = step is null ? 1 : Convert.ToDecimal(step);
            decimal newValue = decimalValue + factor * stepValue;

            if (min is not null && newValue < Convert.ToDecimal(min))
            {
                newValue = Convert.ToDecimal(min);
            }
            else if (max is not null && newValue > Convert.ToDecimal(max))
            {
                newValue = Convert.ToDecimal(max);
            }

            return (T)(object)newValue;
        }
    };
}
