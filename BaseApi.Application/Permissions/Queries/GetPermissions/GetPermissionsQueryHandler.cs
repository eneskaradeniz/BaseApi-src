using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Permissions.Queries.GetPermissions;

internal sealed class GetPermissionsQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetPermissionsQuery, Maybe<List<PermissionResponse>>>
{
    public async Task<Maybe<List<PermissionResponse>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Permission>()
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Select(x => new PermissionResponse
            {
                Id = x.Id.Value,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);
    }
}
