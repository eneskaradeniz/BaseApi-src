using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Users.Queries.GetUserByIdWithRoles;

internal sealed class GetUserByIdWithRolesQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetUserByIdWithRolesQuery, Maybe<AdminUserWithRolesResponse>>
{
    public async Task<Maybe<AdminUserWithRolesResponse>> Handle(GetUserByIdWithRolesQuery request, CancellationToken cancellationToken)
    {
        UserId userId = new(request.UserId);

        if (userId.IsDefault())
        {
            return Maybe<AdminUserWithRolesResponse>.None;
        }

        return await dbContext.Set<User>()
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new AdminUserWithRolesResponse
            {
                Id = u.Id.Value,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Roles = u.UserRoles
                    .Select(ur => new RoleResponse
                    {
                        Id = ur.Role.Id.Value,
                        Name = ur.Role.Name,
                        CreatedOnUtc = ur.Role.CreatedOnUtc
                    }).ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}
