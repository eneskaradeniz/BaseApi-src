using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Roles;

public sealed class Name : ValueObject
{
    public const int MaxLength = 100;

    private Name(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(Name name) => name.Value;

    public static Result<Name> Create(string name) =>
        Result.Create(name, DomainErrors.Name.NullOrEmpty)
            .Ensure(f => !string.IsNullOrWhiteSpace(f), DomainErrors.Name.NullOrEmpty)
            .Ensure(f => f.Length <= MaxLength, DomainErrors.Name.LongerThanAllowed)
            .Map(f => new Name(f));

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
