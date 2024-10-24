using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetUserByIdQuery, Maybe<AdminUserResponse>>
{
    public async Task<Maybe<AdminUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        UserId userId = new(request.UserId);

        if (userId.IsDefault())
        {
            return Maybe<AdminUserResponse>.None;
        }

        return await dbContext.Set<User>()
            .AsNoTracking()
            .Where(u => u.Id == userId)
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
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}