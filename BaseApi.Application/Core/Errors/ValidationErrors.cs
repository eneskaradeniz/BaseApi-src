using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Application.Core.Errors;

internal static class ValidationErrors
{
    internal static class Users
    {
        internal static Error UserIdIsRequired = new("Users.UserIdIsRequired", "The user id is required.");

        internal static Error FirstNameIsRequired = new("Users.FirstNameIsRequired", "The first name is required.");

        internal static Error LastNameIsRequired = new("Users.LastNameIsRequired", "The last name is required.");

        internal static Error EmailOrPhoneNumberIsRequired = new("Users.EmailOrPhoneNumberIsRequired", "The email or phone number is required.");

        internal static Error EmailIsRequired = new("Users.EmailIsRequired", "The email is required.");

        internal static Error PasswordIsRequired = new("Users.PasswordIsRequired", "The password is required.");

        internal static Error ConfirmPasswordIsRequired = new("Users.ConfirmPasswordIsRequired", "The confirm password is required.");

        internal static Error ConfirmPasswordIsDifferent = new("Users.ConfirmPasswordIsDifferent", "The confirm password is different from the password.");

        internal static Error RefreshTokenIsRequired = new("Users.RefreshTokenIsRequired", "The refresh token is required.");

        internal static Error TokenIsRequired = new("Users.TokenIsRequired", "The token is required.");

        internal static Error PhoneNumberIsRequired = new("Users.PhoneNumberIsRequired", "The phone number is required.");

        internal static Error VerificationCodeIsRequired = new("Users.VerificationCodeIsRequired", "The verification code is required.");

        internal static Error CurrentPasswordIsRequired = new("Users.CurrentPasswordIsRequired", "The current password is required.");

        internal static Error NewPasswordIsRequired = new("Users.NewPasswordIsRequired", "The new password is required.");

        internal static Error ConfirmNewPasswordIsRequired = new("Users.ConfirmNewPasswordIsRequired", "The confirm new password is required.");

        internal static Error ConfirmNewPasswordIsDifferent = new("Users.ConfirmNewPasswordIsDifferent", "The confirm new password is different from the new password.");
    }

    internal static class Roles
    {
        internal static Error RoleIdIsRequired = new("Roles.RoleIdIsRequired", "The role id is required.");

        internal static Error PermissionIdIsRequired = new("Roles.PermissionIdIsRequired", "The permission id is required.");

        internal static Error UserIdIsRequired = new("Roles.UserIdIsRequired", "The user id is required.");

        internal static Error NameIsRequired = new("Roles.NameIsRequired", "The name is required.");
    }
    
    internal static class Files
    {
        internal static Error FileIsRequired = new("Files.FileIsRequired", "The file is required.");

        internal static Error FileIdIsRequired = new("Files.FileIdIsRequired", "The file id is required.");

        internal static Error PathIsRequired = new("Files.PathIsRequired", "The path is required.");
    }
}