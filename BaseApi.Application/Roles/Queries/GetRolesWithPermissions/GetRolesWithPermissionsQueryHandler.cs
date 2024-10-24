using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Roles.Queries.GetRolesWithPermissions;

internal sealed class GetRolesWithPermissionsQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetRolesWithPermissionsQuery, Maybe<List<RoleWithPermissionsResponse>>>
{
    public async Task<Maybe<List<RoleWithPermissionsResponse>>> Handle(GetRolesWithPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Role>()
            .AsNoTracking()
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .Select(r => new RoleWithPermissionsResponse
            {
                Id = r.Id.Value,
                Name = r.Name.Value,
                CreatedOnUtc = r.CreatedOnUtc,
                Permissions = r.RolePermissions.Select(rp => new PermissionResponse
                {
                    Id = rp.Permission.Id.Value,
                    Name = rp.Permission.Name.Value
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}