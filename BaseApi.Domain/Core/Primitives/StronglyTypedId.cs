namespace BaseApi.Domain.Core.Primitives;

public abstract class StronglyTypedId<TValue> : IEquatable<StronglyTypedId<TValue>>
{
    public TValue Value { get; }

    protected StronglyTypedId(TValue value)
    {
        if (value == null || value.Equals(default(TValue)))
        {
            throw new ArgumentException("The ID value cannot be null or the default value.", nameof(value));
        }

        Value = value;
    }

    private static bool IsDefaultValue(TValue value)
    {
        return EqualityComparer<TValue>.Default.Equals(value, default!);
    }

    public bool IsDefault() => IsDefaultValue(Value);

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((StronglyTypedId<TValue>)obj);
    }

    public bool Equals(StronglyTypedId<TValue>? other)
    {
        if (other is null)
        {
            return false;
        }

        return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
    }

    public override int GetHashCode() => Value!.GetHashCode();

    public static bool operator ==(StronglyTypedId<TValue> a, StronglyTypedId<TValue> b) =>
        a is null && b is null || a?.Equals(b) == true;

    public static bool operator !=(StronglyTypedId<TValue> a, StronglyTypedId<TValue> b) => !(a == b);

    public override string ToString() => Value?.ToString() ?? string.Empty;
}