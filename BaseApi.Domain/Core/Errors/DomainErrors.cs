using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Core.Errors;

public static class DomainErrors
{
    public static class General
    {
        public static Error UnProcessableRequest => new(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");
    }
        
    public static class Authentication
    {
        public static Error InvalidCredentials => new("Authentication.InvalidCredentials", "Invalid credentials provided.");
    }

    public static class User
    {
        public static Error UserNotFound => new("User.UserNotFound", "User not found.");

        public static Error UserNotVerified => new("User.UserNotVerified", "User is not verified.");

        public static Error DuplicateEmail => new("User.DuplicateEmail", "The specified email is already in use.");

        public static Error DuplicatePhoneNumber => new("User.DuplicatePhoneNumber", "The specified phone number is already in use.");

        public static Error PasswordsDoNotMatch => new("User.PasswordsDoNotMatch", "Passwords do not match.");

        public static Error PasswordSameAsOld => new("User.PasswordSameAsOld", "New password cannot be the same as the old password.");

        public static Error CannotChangePassword => new(
            "User.CannotChangePassword",
            "The password cannot be changed to the specified password.");

        public static Error EmailAlreadyVerified => new("User.EmailAlreadyVerified", "Email is already verified.");

        public static Error EmailNotVerified => new("User.EmailNotVerified", "Email is not verified.");

        public static Error PhoneNumberAlreadyVerified => new("User.PhoneNumberAlreadyVerified", "Phone number is already verified.");

        public static Error PhoneNumberNotVerified => new("User.PhoneNumberNotVerified", "Phone number is not verified.");

        public static Error EmailSameAsOld => new("User.EmailSameAsOld", "New email cannot be the same as the old email.");
        
        public static Error PhoneNumberSameAsOld => new("User.PhoneNumberSameAsOld", "New phone number cannot be the same as the old phone number.");
    }

    public static class RefreshToken
    {
        public static Error InvalidRefreshToken => new("RefreshToken.InvalidRefreshToken", "Invalid refresh token.");

        public static Error RefreshTokenExpired => new("RefreshToken.RefreshTokenExpired", "Refresh token has expired.");
    }

    public static class PasswordResetToken
    {
        public static Error InvalidPasswordResetToken => new("PasswordResetToken.InvalidPasswordResetToken", "Invalid password reset token.");

        public static Error PasswordResetTokenExpired => new("PasswordResetToken.PasswordResetTokenExpired", "Password reset token has expired.");

        public static Error AlreadySentPasswordResetToken => new("PasswordResetToken.AlreadySentPasswordResetToken", "Password reset token has already been sent.");
    }

    public static class PhoneNumberVerificationCode
    {
        public static Error InvalidPhoneNumberVerificationCode => new("PhoneNumberVerificationCode.InvalidPhoneNumberVerificationCode", "Invalid phone number verification code.");

        public static Error PhoneNumberVerificationCodeExpired => new("PhoneNumberVerificationCode.PhoneNumberVerificationCodeExpired", "Phone number verification code has expired.");

        public static Error AlreadySentPhoneNumberVerificationCode => new("PhoneNumberVerificationCode.AlreadySentPhoneNumberVerificationCode", "Phone number verification code has already been sent.");
    }

    public static class EmailVerificationCode
    {
        public static Error InvalidEmailVerificationCode => new("EmailVerificationCode.InvalidEmailVerificationCode", "Invalid email verification code.");

        public static Error EmailVerificationCodeExpired => new("EmailVerificationCode.EmailVerificationCodeExpired", "Email verification code has expired.");

        public static Error AlreadySentEmailVerificationCode => new("EmailVerificationCode.AlreadySentEmailVerificationCode", "Email verification code has already been sent.");
    }

    public static class FirstName
    {
        public static Error NullOrEmpty => new("FirstName.NullOrEmpty", "The first name is required.");

        public static Error LongerThanAllowed => new("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
    }

    public static class LastName
    {
        public static Error NullOrEmpty => new("LastName.NullOrEmpty", "The last name is required.");

        public static Error LongerThanAllowed => new("LastName.LongerThanAllowed", "The last name is longer than allowed.");
    }

    public static class Email
    {
        public static Error NullOrEmpty => new("Email.NullOrEmpty", "The email is required.");

        public static Error LongerThanAllowed => new("Email.LongerThanAllowed", "The email is longer than allowed.");

        public static Error InvalidFormat => new("Email.InvalidFormat", "The email format is invalid.");
    }

    public static class PhoneNumber
    {
        public static Error NullOrEmpty => new("PhoneNumber.NullOrEmpty", "The phone number is required.");

        public static Error LongerThanAllowed => new("PhoneNumber.LongerThanAllowed", "The phone number is longer than allowed.");

        public static Error InvalidFormat => new("PhoneNumber.InvalidFormat", "The phone number format is invalid.");
    }

    public static class Password
    {
        public static Error NullOrEmpty => new("Password.NullOrEmpty", "The password is required.");

        public static Error TooShort => new("Password.TooShort", "The password is too short.");

        public static Error TooLong => new("Password.TooLong", "The password is too long.");

        public static Error MissingUppercaseLetter => new(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.");

        public static Error MissingLowercaseLetter => new(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.");

        public static Error MissingDigit => new(
            "Password.MissingDigit",
            "The password requires at least one digit.");

        public static Error MissingNonAlphaNumeric => new(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.");
    }

    public static class Name
    {
        public static Error NullOrEmpty => new("Name.NullOrEmpty", "The name is required.");

        public static Error LongerThanAllowed => new("Name.LongerThanAllowed", "The name is longer than allowed.");
    }

    public static class VerificationCode
    {
        public static Error NullOrEmpty => new("VerificationCode.NullOrEmpty", "The verification code is required.");

        public static Error MustBe6Digits => new("VerificationCode.MustBe6Digits", "The verification code must be 6 digits.");

        public static Error MustBeNumeric => new("VerificationCode.MustBeNumeric", "The verification code must be numeric.");
    }

    public static class Role
    {
        public static Error RoleNotFound => new("Role.RoleNotFound", "Role not found.");

        public static Error DuplicateName => new("Role.DuplicateName", "The specified name is already in use.");
    }

    public static class Permission
    {
        public static Error PermissionNotFound => new("Permission.PermissionNotFound", "Permission not found.");
    }

    public static class File
    {
        public static Error FileNotFound => new("File.FileNotFound", "File not found.");
    }
}