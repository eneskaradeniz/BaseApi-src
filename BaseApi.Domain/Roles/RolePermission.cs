using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Roles;

public class RolePermission : BaseEntity
{
    public RoleId RoleId { get; private set; }

    public Role Role { get; init; }

    public PermissionId PermissionId { get; private set; }

    public Permission Permission { get; init; }

    private RolePermission()
    {
    }

    private RolePermission(RoleId roleId, PermissionId permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public static RolePermission Create(RoleId roleId, PermissionId permissionId)
    {
        return new RolePermission(roleId, permissionId);
    }
}