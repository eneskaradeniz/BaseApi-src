using BaseApi.Domain.Roles;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithUserRoleSpecification : Specification<User>
{
    private readonly UserId _userId;
    private readonly RoleId _roleId;

    internal UserWithUserRoleSpecification(UserId userId, RoleId roleId)
    {
        _userId = userId;
        _roleId = roleId;
    }

    internal override IQueryable<User> Apply(IQueryable<User> query) =>
        query
            .Include(u => u.UserRoles.Where(ur => ur.RoleId == _roleId))
            .Where(u => u.Id == _userId);
}
