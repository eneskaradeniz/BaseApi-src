using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Domain.Users;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 100;

    private LastName(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(LastName lastName) => lastName.Value;

    public static Result<LastName> Create(string lastName) =>
        Result.Create(lastName, DomainErrors.LastName.NullOrEmpty)
            .Ensure(f => !string.IsNullOrWhiteSpace(f), DomainErrors.LastName.NullOrEmpty)
            .Ensure(f => f.Length <= MaxLength, DomainErrors.LastName.LongerThanAllowed)
            .Map(f => new LastName(f));

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
