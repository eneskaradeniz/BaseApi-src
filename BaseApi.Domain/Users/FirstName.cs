using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Domain.Users;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 100;

    private FirstName(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(FirstName firstName) => firstName.Value;

    public static Result<FirstName> Create(string firstName) =>
        Result.Create(firstName, DomainErrors.FirstName.NullOrEmpty)
            .Ensure(f => !string.IsNullOrWhiteSpace(f), DomainErrors.FirstName.NullOrEmpty)
            .Ensure(f => f.Length <= MaxLength, DomainErrors.FirstName.LongerThanAllowed)
            .Map(f => new FirstName(f));

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
