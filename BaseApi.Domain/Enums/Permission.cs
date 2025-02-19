namespace BaseApi.Domain.Enums;

public enum Permission
{
    All = 1,

    GetUsers,
    GetUserById,
    UpdateUser,
    RemoveUser,
    AssignRole,
    GetUsersWithRoles,
    GetUserByIdWithRoles,

    GetRoles,
    GetRoleById,
    GetRolesWithPermissions,
    GetRoleByIdWithPermissions,
    CreateRole,
    UpdateRole,
    RemoveRole,
    AssignPermission,

    GetPermissions,

    GetFiles,
    UploadFile,
    RemoveFile
}