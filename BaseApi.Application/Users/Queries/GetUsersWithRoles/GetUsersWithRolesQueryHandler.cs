using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Common;
using BaseApi.Contracts.Roles;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Users.Queries.GetUsersWithRoles;

internal sealed class GetUsersWithRolesQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetUsersWithRolesQuery, Maybe<PagedList<AdminUserWithRolesResponse>>>
{
    public async Task<Maybe<PagedList<AdminUserWithRolesResponse>>> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<AdminUserWithRolesResponse> query = dbContext.Set<User>()
            .AsNoTracking()
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
            });

        var totalCount = await query.CountAsync(cancellationToken);

        AdminUserWithRolesResponse[] responseArray = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync(cancellationToken);

        return new PagedList<AdminUserWithRolesResponse>(responseArray, request.Page, request.PageSize, totalCount);
    }
}
