using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithRefreshToken : Specification<User>
{
    private readonly string _refreshToken;

    internal UserWithRefreshToken(string refreshToken) => _refreshToken = refreshToken;
        
    internal override IQueryable<User> Apply(IQueryable<User> query) => query
        .Where(u => u.RefreshTokens.Any(rt => rt.Token == _refreshToken))
        .Include(u => u.RefreshTokens);
}