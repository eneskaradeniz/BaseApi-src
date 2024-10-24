using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Users.Queries.GetMe;

internal sealed class GetMeQueryHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IApplicationDbContext dbContext)
    : IQueryHandler<GetMeQuery, Maybe<MeResponse>>
{
    public async Task<Maybe<MeResponse>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        UserId userId = userIdentifierProvider.UserId;

        if (userId.IsDefault())
        {
            return Maybe<MeResponse>.None;
        }

        return await dbContext.Set<User>()
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new MeResponse
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}