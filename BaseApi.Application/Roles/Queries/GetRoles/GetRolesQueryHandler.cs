using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;
using BaseApi.Contracts.Roles;

namespace BaseApi.Application.Roles.Queries.GetRoles;

internal sealed class GetRolesQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetRolesQuery, Maybe<List<RoleResponse>>>
{
    public async Task<Maybe<List<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Role>()
            .AsNoTracking()
            .Select(x => new RoleResponse
            {
                Id = x.Id.Value,
                Name = x.Name,
                CreatedOnUtc = x.CreatedOnUtc
            })
            .ToListAsync(cancellationToken);
    }
}