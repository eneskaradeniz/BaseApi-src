using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Core.Guards;

public static class Ensure
{
    public static void NotEmpty(string value, string message, string argumentName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void NotEmpty(Guid value, string message, string argumentName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void NotEmpty(DateTime value, string message, string argumentName)
    {
        if (value == default)
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void NotEmpty<T>(T value, string message, string argumentName)
    {
        if (value is null || EqualityComparer<T>.Default.Equals(value, default!))
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void NotEmpty<TValue>(StronglyTypedId<TValue> value, string message, string argumentName)
    {
        if (value is null || EqualityComparer<TValue>.Default.Equals(value.Value, default!))
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void NotNull<T>(T value, string message, string argumentName)
    {
        if (value is null)
        {
            throw new ArgumentNullException(argumentName, message);
        }
    }
}