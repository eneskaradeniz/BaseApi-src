using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Roles.Queries.GetRoleByIdWithPermissions;

internal sealed class GetRoleByIdWithPermissionsQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetRoleByIdWithPermissionsQuery, Maybe<RoleWithPermissionsResponse>>
{
    public async Task<Maybe<RoleWithPermissionsResponse>> Handle(GetRoleByIdWithPermissionsQuery request, CancellationToken cancellationToken)
    {
        RoleId roleId = new(request.RoleId);

        if (roleId.IsDefault())
        {
            return Maybe<RoleWithPermissionsResponse>.None;
        }

        return await dbContext.Set<Role>()
            .AsNoTracking()
            .Where(r => r.Id == roleId)
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .Select(r => new RoleWithPermissionsResponse
            {
                Id = r.Id.Value,
                Name = r.Name,
                CreatedOnUtc = r.CreatedOnUtc,
                Permissions = r.RolePermissions.Select(rp => new PermissionResponse
                {
                    Id = rp.Permission.Id.Value,
                    Name = rp.Permission.Name
                }).ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}
