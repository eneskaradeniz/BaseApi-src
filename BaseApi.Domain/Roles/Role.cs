using BaseApi.Domain.Core.Abstractions;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Domain.Roles;

public class Role : AggregateRoot<RoleId, Guid>, IAuditableEntity
{
    private readonly HashSet<RolePermission> _rolePermissions = [];

    public Name Name { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.ToList();

    private Role()
    {
    }

    private Role(Name name) : base(new RoleId(Guid.NewGuid()))
    {
        Name = name;
    }

    public static Role Create(Name name)
    {
        return new Role(name);
    }

    public void Update(Name name)
    {
        Name = name;
    }

    public void AddPermission(PermissionId permissionId)
    {
        var rolePermission = RolePermission.Create(Id, permissionId);

        _rolePermissions.Add(rolePermission);
    }

    public void RemovePermission(PermissionId permissionId)
    {
        RolePermission? rolePermission = _rolePermissions
            .SingleOrDefault(rp => rp.PermissionId == permissionId);

        if (rolePermission is not null)
        {
            _rolePermissions.Remove(rolePermission);
        }
    }

    public Result AssignPermission(PermissionId permissionId)
    {
        if (!Enum.IsDefined(typeof(Enums.Permission), permissionId.Value))
        {
            return Result.Failure(DomainErrors.Permission.PermissionNotFound);
        }

        bool permissionExists = _rolePermissions.Any(rp => rp.PermissionId == permissionId);

        if (permissionExists)
        {
            RemovePermission(permissionId);
        }
        else
        {
            AddPermission(permissionId);
        }

        return Result.Success();
    }
}