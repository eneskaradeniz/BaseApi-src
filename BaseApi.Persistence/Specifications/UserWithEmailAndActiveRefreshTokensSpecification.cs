using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithEmailAndActiveRefreshTokensSpecification : Specification<User>
{
    private readonly Email _email;

    internal UserWithEmailAndActiveRefreshTokensSpecification(Email email) => _email = email;

    internal override IQueryable<User> Apply(IQueryable<User> query) => query
        .Include(u => u.RefreshTokens.Where(rt => !rt.IsRevoked))
        .Where(user => user.Email.Value == _email);
}