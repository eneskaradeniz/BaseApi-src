namespace BaseApi.Api.Contracts;

public static class ApiRoutes
{
    public static class Public
    {
        public static class Files
        {
            public const string GetBaseStorageUrl = "files/base-storage-url";
        }
    }

    public static class Identity
    {
        public static class Authentication
        {
            public const string Login = "authentication/login";

            public const string Register = "authentication/register";

            public const string RefreshToken = "authentication/refresh-token";

            public const string ForgotPassword = "authentication/forgot-password";

            public const string ResetPassword = "authentication/reset-password";

            public const string EmailVerification = "authentication/email-verification";

            public const string PhoneNumberVerification = "authentication/phone-number-verification";

            public const string SendEmailVerification = "authentication/email-verification/send";

            public const string SendPhoneNumberVerification = "authentication/phone-number-verification/send";

            public const string Logout = "authentication/logout";
        }
            
        public static class Users
        {
            public const string GetMe = "users/me";

            public const string UpdateMe = "users/me";

            public const string UpdateEmail = "users/me/email";

            public const string UpdatePhoneNumber = "users/me/phone-number";

            public const string UpdatePassword = "users/me/password";

            public const string DeleteAccount = "users";
        }
    }

    public static class Admin
    {
        public static class Users
        {
            public const string GetAll = "admin/users";

            public const string GetById = "admin/users/{userId:guid}";

            public const string Update = "admin/users/{userId:guid}";

            public const string Remove = "admin/users/{userId:guid}";

            public const string GetAllWithRoles = "admin/users/roles";

            public const string GetByIdWithRoles = "admin/users/{userId:guid}/roles";

            public const string AssignRole = "admin/users/{userId:guid}/roles/{roleId:guid}";
        }
            
        public static class Roles
        {
            public const string GetAll = "admin/roles";

            public const string GetAllWithPermissions = "admin/roles/permissions";

            public const string GetById = "admin/roles/{roleId:guid}";
            
            public const string GetByIdWithPermissions = "admin/roles/{roleId:guid}/permissions";

            public const string Create = "admin/roles";

            public const string Update = "admin/roles/{roleId:guid}";

            public const string Remove = "admin/roles/{roleId:guid}";

            public const string AssignPermission = "admin/roles/{roleId:guid}/permissions/{permissionId}";

            //public const string AssignUser = "admin/roles/{roleId:guid}/users/{userId:guid}";
        }

        public static class Permissions
        {
            public const string GetAll = "admin/permissions";
        }

        public static class Files
        {
            public const string GetAll = "admin/files";

            public const string Upload = "admin/files";

            public const string Remove = "admin/files/{fileId:guid}";
        }
    }
}