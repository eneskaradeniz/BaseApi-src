using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Result;
using System.Text.RegularExpressions;

namespace BaseApi.Domain.Users;

public sealed class VerificationCode : ValueObject
{
    public const int Length = 6;

    private static readonly Regex NumericRegex = new(@"^\d+$");

    private VerificationCode(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(VerificationCode verificationCode) => verificationCode.Value;

    public static Result<VerificationCode> Create(string verificationCode) =>
        Result.Create(verificationCode, DomainErrors.VerificationCode.NullOrEmpty)
            .Ensure(v => !string.IsNullOrWhiteSpace(v), DomainErrors.VerificationCode.NullOrEmpty)
            .Ensure(v => v.Length == Length, DomainErrors.VerificationCode.MustBe6Digits)
            .Ensure(v => NumericRegex.IsMatch(v), DomainErrors.VerificationCode.MustBeNumeric)
            .Map(v => new VerificationCode(v));

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
