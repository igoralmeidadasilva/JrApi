using System.Numerics;
using System.Text.RegularExpressions;

namespace JrApi.Domain.Core;

public static class ArgumentValidator
{
    public static void ThrowIfNull(object value, string parameterName = null!)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);
    }
    public static void ThrowIfNullOrEmpty(string value, string parameterName = null!)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);
        if (value.Length == 0)
            throw new ArgumentException(string.Format("{0} cannot be an empty string.", parameterName ?? "Value"), parameterName);
    }

    public static void ThrowIfNullOrWhitespace(string value, string parameterName = null!)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(string.Format("{0} cannot be an empty string.", parameterName ?? "Value"), parameterName);
    }
    public static void ThrowIfNullOrDefault<T>(T obj, string? paramName = null)
    {
        if (obj == null || obj.Equals(default(T)))
            throw new ArgumentNullException($"{paramName ?? "Argument"} cannot be null or default");
    }

    public static void ThrowIfZero<T>(T argument, string? paramName = null) where T : INumber<T>
    {
        if (argument == T.Zero)
            throw new ArgumentException($"{paramName ?? "Argument"} cannot be zero");
    }

    public static void ThrowIfNegative<T>(T argument, string? paramName = null) where T : INumber<T>
    {
        if (argument < T.Zero)
            throw new ArgumentException($"{paramName ?? "Argument"} cannot be negative");
    }

    public static void ThrowIfZeroOrNegative<T>(T argument, string? paramName = null) where T : INumber<T>
    {
        if (argument <= T.Zero)
            throw new ArgumentException($"{paramName ?? "Argument"} cannot be zero or negative");
    }

    public static void ThrowIfOutOfRange(int value, string? paramName = null, int min = 0, int max = int.MaxValue)
    {
        if (value > max || value < min)
            throw new ArgumentOutOfRangeException(paramName, value, String.Format("{0} is out of range.", paramName ?? "Value"));
    }

    public static void ThrowIfOutOfRange(double value, string? paramName = null, double min = 0, double max = double.MaxValue)
    {
        if (value > max || value < min)
            throw new ArgumentOutOfRangeException(paramName, value, String.Format("{0} is out of range.", paramName ?? "Value"));
    }

    public static void ThrowIfValuesNotEquals(int valueX, int valueY, string? paramName = null)
    {
        if (valueX != valueY)
            throw new ArgumentException(String.Format("{0} does not match the expected values for {1} e {2}.", paramName ?? "Value", valueX, valueY), paramName);
    }

    public static void ThrowIfPatternFails(string value, Regex regex, string? parameterName = null)
    {
        if (value != null)
        {
            if (!regex.IsMatch(value))
            {
                throw new ArgumentException(String.Format("{0} does not match the expected format.", parameterName ?? "Value"), parameterName);
            }
        }
    }
    public static void ThrowIfPatternFails(string value, string regex, string? parameterName = null)
    {
        ThrowIfPatternFails(value, new Regex(regex), parameterName);
    }
}