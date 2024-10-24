using BaseApi.Domain.Core.Abstractions;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Guards;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Roles;

namespace BaseApi.Domain.Users;

public sealed class User : AggregateRoot<UserId, Guid>, IAuditableEntity, ISoftDeletableEntity
{
    private string _passwordHash;

    private readonly HashSet<UserRole> _userRoles = [];

    private readonly HashSet<EmailVerificationCode> _emailVerificationCodes = [];

    private readonly HashSet<PhoneNumberVerificationCode> _phoneNumberVerificationCodes = [];

    private readonly HashSet<RefreshToken> _refreshTokens = [];

    private readonly HashSet<PasswordResetToken> _passwordResetTokens = [];

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public Email Email { get; private set; }

    public bool EmailVerified { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public bool PhoneNumberVerified { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.ToList();

    public IReadOnlyCollection<EmailVerificationCode> EmailVerificationCodes => _emailVerificationCodes.ToList();

    public IReadOnlyCollection<PhoneNumberVerificationCode> PhoneNumberVerificationCodes => _phoneNumberVerificationCodes.ToList();

    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.ToList();

    public IReadOnlyCollection<PasswordResetToken> PasswordResetTokens => _passwordResetTokens.ToList();

    private User()
    {
    }

    private User(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        string passwordHash) : base(new UserId(Guid.NewGuid()))
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        _passwordHash = passwordHash;
    }

    public static User Create(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        string passwordHash)
    {
        return new User(firstName, lastName, email, phoneNumber, passwordHash);
    }

    public bool IsVerified => EmailVerified && PhoneNumberVerified;

    public Result EnsureUserIsVerified()
    {
        if (!EmailVerified)
        {
            return Result.Failure(DomainErrors.User.EmailNotVerified);
        }

        if (!PhoneNumberVerified)
        {
            return Result.Failure(DomainErrors.User.PhoneNumberNotVerified);
        }

        return Result.Success();
    }

    #region Roles

    public void AddRole(RoleId roleId)
    {
        var userRole = UserRole.Create(Id, roleId);

        _userRoles.Add(userRole);
    }

    public void RemoveRole(RoleId roleId)
    {
        UserRole? userRole = _userRoles
            .SingleOrDefault(ur => ur.RoleId == roleId);

        if (userRole is not null)
        {
            _userRoles.Remove(userRole);
        }
    }
    
    public void AssignRole(RoleId roleId)
    {
        bool roleExists = _userRoles.Any(ur => ur.RoleId == roleId);

        if (roleExists)
        {
            RemoveRole(roleId);
        }
        else
        {
            AddRole(roleId);
        }
    }

    #endregion

    #region Email Verification Codes

    public void AddEmailVerificationCode(VerificationCode verificationCode, DateTime utcNow)
    {
        var emailVerificationCode = EmailVerificationCode.Create(
            Id,
            verificationCode,
            utcNow);

        _emailVerificationCodes.Add(emailVerificationCode);
    }

    public Result VerifyEmail(VerificationCode verificationCode, DateTime utcNow)
    {
        if (EmailVerified)
        {
            return Result.Failure(DomainErrors.User.EmailAlreadyVerified);
        }

        var emailVerificationCode = _emailVerificationCodes
            .SingleOrDefault(evc => evc.VerificationCode == verificationCode);

        if (emailVerificationCode is null || emailVerificationCode.IsUsed)
        {
            return Result.Failure(DomainErrors.EmailVerificationCode.InvalidEmailVerificationCode);
        }

        if (emailVerificationCode.IsExpired(utcNow))
        {
            return Result.Failure(DomainErrors.EmailVerificationCode.EmailVerificationCodeExpired);
        }

        emailVerificationCode.MarkAsUsed();

        EmailVerified = true;

        return Result.Success();
    }

    public Result GenerateEmailVerificationCode(VerificationCode verificationCode, DateTime utcNow)
    {
        if (EmailVerified)
        {
            return Result.Failure(DomainErrors.User.EmailAlreadyVerified);
        }

        if (IsEmailVerificationCodeActive(utcNow))
        {
            return Result.Failure(DomainErrors.EmailVerificationCode.AlreadySentEmailVerificationCode);
        }

        AddEmailVerificationCode(verificationCode, utcNow);

        return Result.Success();
    }

    private bool IsEmailVerificationCodeActive(DateTime utcNow)
        => _emailVerificationCodes.Any(evc => !evc.IsUsed && !evc.IsExpired(utcNow));

    #endregion

    #region Phone Number Verification Codes

    public void AddPhoneNumberVerificationCode(VerificationCode verificationCode, DateTime utcNow)
    {
        var phoneNumberVerificationCode = PhoneNumberVerificationCode.Create(
            Id,
            verificationCode,
            utcNow);

        _phoneNumberVerificationCodes.Add(phoneNumberVerificationCode);
    }

    public Result VerifyPhoneNumber(VerificationCode verificationCode, DateTime utcNow)
    {
        if (PhoneNumberVerified)
        {
            return Result.Failure(DomainErrors.User.PhoneNumberAlreadyVerified);
        }

        PhoneNumberVerificationCode? phoneNumberVerificationCode = _phoneNumberVerificationCodes
            .SingleOrDefault(pvc => pvc.VerificationCode == verificationCode);

        if (phoneNumberVerificationCode is null || phoneNumberVerificationCode.IsUsed)
        {
            return Result.Failure(DomainErrors.PhoneNumberVerificationCode.InvalidPhoneNumberVerificationCode);
        }

        if (phoneNumberVerificationCode.IsExpired(utcNow))
        {
            return Result.Failure(DomainErrors.PhoneNumberVerificationCode.PhoneNumberVerificationCodeExpired);
        }

        phoneNumberVerificationCode.MarkAsUsed();

        PhoneNumberVerified = true;

        return Result.Success();
    }

    public Result GeneratePhoneNumberVerificationCode(VerificationCode verificationCode, DateTime utcNow)
    {
        if (PhoneNumberVerified)
        {
            return Result.Failure(DomainErrors.User.PhoneNumberAlreadyVerified);
        }

        if (IsPhoneNumberVerificationCodeActive(utcNow))
        {
            return Result.Failure(DomainErrors.PhoneNumberVerificationCode.AlreadySentPhoneNumberVerificationCode);
        }

        AddPhoneNumberVerificationCode(verificationCode, utcNow);

        return Result.Success();
    }

    private bool IsPhoneNumberVerificationCodeActive(DateTime utcNow)
        => _phoneNumberVerificationCodes.Any(pvc => !pvc.IsUsed && !pvc.IsExpired(utcNow));

    #endregion

    #region Refresh Tokens

    public void AddRefreshToken(string token, DateTime utcNow)
    {
        var refreshToken = RefreshToken.Create(
            Id,
            token,
            utcNow);

        _refreshTokens.Add(refreshToken);
    }

    public Result RevokeRefreshToken(string token, DateTime utcNow)
    {
        RefreshToken? refreshToken = _refreshTokens.SingleOrDefault(rt => rt.Token == token);

        if (refreshToken is null || refreshToken.IsRevoked)
        {
            return Result.Failure(DomainErrors.RefreshToken.InvalidRefreshToken);
        }

        if (refreshToken.IsExpired(utcNow))
        {
            return Result.Failure(DomainErrors.RefreshToken.RefreshTokenExpired);
        }

        refreshToken.Revoke(utcNow);

        return Result.Success();
    }

    public void RevokeOldRefreshTokens(DateTime utcNow)
    {
        foreach (var token in RefreshTokens)
        {
            token.Revoke(utcNow);
        }
    }

    #endregion

    #region Password Reset Tokens

    public void AddPasswordResetToken(string token, DateTime utcNow)
    {
        var passwordResetToken = PasswordResetToken.Create(
            Id,
            token,
            utcNow);

        _passwordResetTokens.Add(passwordResetToken);
    }

    public Result GeneratePasswordResetToken(string token, DateTime utcNow)
    {
        if (!IsVerified)
        {
            return Result.Failure<PasswordResetToken>(DomainErrors.User.UserNotVerified);
        }

        if (IsPasswordResetTokenActive(utcNow))
        {
            return Result.Failure<PasswordResetToken>(DomainErrors.PasswordResetToken.AlreadySentPasswordResetToken);
        }

        AddPasswordResetToken(token, utcNow);

        return Result.Success();
    }

    public Result ResetPassword(
        string token,
        Password newPassword,
        string newPasswordHash,
        DateTime utcNow,
        IPasswordHashChecker passwordHashChecker)
    {
        if (!IsVerified)
        {
            return Result.Failure<PasswordResetToken>(DomainErrors.User.UserNotVerified);
        }

        PasswordResetToken? passwordResetToken = _passwordResetTokens.SingleOrDefault(prt => prt.Token == token);

        if (passwordResetToken is null || passwordResetToken.IsUsed)
        {
            return Result.Failure(DomainErrors.PasswordResetToken.InvalidPasswordResetToken);
        }

        if (passwordResetToken.IsExpired(utcNow))
        {
            return Result.Failure(DomainErrors.PasswordResetToken.PasswordResetTokenExpired);
        }

        Result changePasswordResult = ChangePassword(newPassword, newPasswordHash, passwordHashChecker);

        if (changePasswordResult.IsFailure)
        {
            return changePasswordResult;
        }

        passwordResetToken.MarkAsUsed();

        return Result.Success();
    }

    private bool IsPasswordResetTokenActive(DateTime utcNow)
        => _passwordResetTokens.Any(prt => !prt.IsUsed && !prt.IsExpired(utcNow));

    #endregion

    public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
        => !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(password, _passwordHash);

    public Result ChangePassword(
        Password newPassword,
        string newPasswordHash,
        IPasswordHashChecker passwordHashChecker)
    {
        if (passwordHashChecker.HashesMatch(newPassword, _passwordHash))
        {
            return Result.Failure(DomainErrors.User.CannotChangePassword);
        }

        _passwordHash = newPasswordHash;

        return Result.Success();
    }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        Ensure.NotEmpty(firstName, "The first name is required.", nameof(firstName));
        Ensure.NotEmpty(lastName, "The last name is required.", nameof(lastName));

        FirstName = firstName;
        LastName = lastName;
    }

    public Result ChangeEmail(Email email)
    {
        Ensure.NotEmpty(email.Value, "The email is required.", nameof(email));

        if (Email.Equals(email))
        {
            return Result.Failure(DomainErrors.User.EmailSameAsOld);
        }

        Email = email;
        EmailVerified = false;

        return Result.Success();
    }

    public Result ChangePhoneNumber(PhoneNumber phoneNumber)
    {
        Ensure.NotEmpty(phoneNumber.Value, "The phone number is required.", nameof(phoneNumber));

        if (PhoneNumber.Equals(phoneNumber))
        {
            return Result.Failure(DomainErrors.User.PhoneNumberSameAsOld);
        }

        PhoneNumber = phoneNumber;
        PhoneNumberVerified = false;

        return Result.Success();
    }
}
