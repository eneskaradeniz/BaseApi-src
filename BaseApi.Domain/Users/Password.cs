using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Domain.Users
{
    public sealed class Password : ValueObject
    {
        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 100;
        private static readonly Func<char, bool> IsLower = c => c >= 'a' && c <= 'z';
        private static readonly Func<char, bool> IsUpper = c => c >= 'A' && c <= 'Z';
        private static readonly Func<char, bool> IsDigit = c => c >= '0' && c <= '9';
        private static readonly Func<char, bool> IsNonAlphaNumeric = c => !(IsLower(c) || IsUpper(c) || IsDigit(c));

        private Password(string value) => Value = value;

        public string Value { get; }

        public static implicit operator string(Password password) => password?.Value ?? string.Empty;

        public static Result<Password> Create(string password) =>
            Result.Create(password, DomainErrors.Password.NullOrEmpty)
                .Ensure(p => !string.IsNullOrWhiteSpace(p), DomainErrors.Password.NullOrEmpty)
                .Ensure(p => p.Length >= MinPasswordLength, DomainErrors.Password.TooShort)
                .Ensure(p => p.Length <= MaxPasswordLength, DomainErrors.Password.TooLong)
                .Ensure(p => p.Any(IsLower), DomainErrors.Password.MissingLowercaseLetter)
                .Ensure(p => p.Any(IsUpper), DomainErrors.Password.MissingUppercaseLetter)
                .Ensure(p => p.Any(IsDigit), DomainErrors.Password.MissingDigit)
                .Ensure(p => p.Any(IsNonAlphaNumeric), DomainErrors.Password.MissingNonAlphaNumeric)
                .Map(p => new Password(p));

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
