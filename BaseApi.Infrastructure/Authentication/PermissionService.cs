using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Infrastructure.Authentication;

public class PermissionService(IApplicationDbContext dbContext) : IPermissionService
{
    public async Task<HashSet<string>> GetPermissionsAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        var permissionsArray = await dbContext.Set<User>()
            .AsNoTracking()
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.UserRoles)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission.Name.Value)
            .ToArrayAsync(cancellationToken);

        var permissions = permissionsArray.ToHashSet();

        return permissions;
    }
}