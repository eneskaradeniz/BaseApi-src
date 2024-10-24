using System.Text.RegularExpressions;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Domain.Users;
public sealed class PhoneNumber : ValueObject
{
    public const int MaxLength = 15;

    private static readonly Lazy<Regex> PhoneNumberRegex =
        new Lazy<Regex>(() => new Regex(@"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled));

    private PhoneNumber(string value) => Value = value;

    public string Value { get; }

    public static Result<PhoneNumber> Create(string phoneNumber) =>
        Result.Create(phoneNumber, DomainErrors.PhoneNumber.NullOrEmpty)
            .Ensure(p => !string.IsNullOrWhiteSpace(p), DomainErrors.PhoneNumber.NullOrEmpty)
            .Ensure(p => p.Length <= MaxLength, DomainErrors.PhoneNumber.LongerThanAllowed)
            .Ensure(p => PhoneNumberRegex.Value.IsMatch(p), DomainErrors.PhoneNumber.InvalidFormat)
            .Map(p => new PhoneNumber(p));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
}
