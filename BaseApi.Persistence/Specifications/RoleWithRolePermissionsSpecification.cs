using BaseApi.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Specifications;

internal sealed class RoleWithRolePermissionsSpecification : Specification<Role>
{
    private readonly RoleId _id;

    internal RoleWithRolePermissionsSpecification(RoleId id) => _id = id;

    internal override IQueryable<Role> Apply(IQueryable<Role> query) => 
        query
            .Include(r => r.RolePermissions)
            .Where(r => r.Id == _id);
}
