namespace BaseApi.Domain.Enums;

public enum Permission
{
    All = 1,

    GetUsers = 2,
    GetUserById = 3,
    UpdateUser = 4,
    RemoveUser = 5,
    AssignRole = 6,
    GetUsersWithRoles = 7,
    GetUserByIdWithRoles = 8,

    GetRoles = 9,
    GetRoleById = 10,
    GetRolesWithPermissions = 11,
    GetRoleByIdWithPermissions = 12,
    CreateRole = 13,
    UpdateRole = 14,
    RemoveRole = 15,
    AssignPermission = 16,

    GetPermissions = 17,

    GetFiles = 18,
    UploadFile = 19,
    RemoveFile = 20
}