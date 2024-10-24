using BaseApi.Domain.Core.Guards;

namespace BaseApi.Domain.Core.Primitives;

public abstract class BaseEntity;

public abstract class Entity<TId, TValue> : BaseEntity,
    IEquatable<Entity<TId, TValue>> where TId : StronglyTypedId<TValue>
{
    protected Entity(TId id)
    {
        Ensure.NotEmpty<TValue>(id, "The identifier is required.", nameof(id));

        Id = id;
    }

    protected Entity()
    {
    }

    public TId Id { get; }

    public static bool operator ==(Entity<TId, TValue> a, Entity<TId, TValue> b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId, TValue> a, Entity<TId, TValue> b) => !(a == b);

    public bool Equals(Entity<TId, TValue>? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (!(obj is Entity<TId, TValue> other))
        {
            return false;
        }

        if (Id.Equals(default(TId)) || other.Id.Equals(default(TId)))
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;
}
