using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Common;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Users.Queries.GetUsers;

internal sealed class GetUsersQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetUsersQuery, Maybe<PagedList<AdminUserResponse>>>
{
    public async Task<Maybe<PagedList<AdminUserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<AdminUserResponse> query = dbContext.Set<User>()
            .AsNoTracking()
            .Select(u => new AdminUserResponse
            {
                Id = u.Id.Value,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                EmailVerified = u.EmailVerified,
                PhoneNumber = u.PhoneNumber,
                PhoneNumberVerified = u.PhoneNumberVerified,
                CreatedOnUtc = u.CreatedOnUtc,
                ModifiedOnUtc = u.ModifiedOnUtc
            });

        var totalCount = await query.CountAsync(cancellationToken);

        AdminUserResponse[] responseArray = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync(cancellationToken);

        return new PagedList<AdminUserResponse>(responseArray, request.Page, request.PageSize, totalCount);
    }
}