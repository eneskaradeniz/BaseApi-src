using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Roles.Queries.GetRoleById;

internal sealed class GetRoleByIdQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetRoleByIdQuery, Maybe<RoleResponse>>
{
    public async Task<Maybe<RoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        RoleId roleId = new(request.RoleId);

        if (roleId.IsDefault())
        {
            return Maybe<RoleResponse>.None;
        }

        return await dbContext.Set<Role>()
            .AsNoTracking()
            .Where(x => x.Id == roleId)
            .Select(x => new RoleResponse
            {
                Id = x.Id.Value,
                Name = x.Name,
                CreatedOnUtc = x.CreatedOnUtc
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}
