using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithPhoneNumberAndActiveRefreshTokensSpecification : Specification<User>
{
    private readonly PhoneNumber _phoneNumber;

    internal UserWithPhoneNumberAndActiveRefreshTokensSpecification(PhoneNumber phoneNumber) => _phoneNumber = phoneNumber;

    internal override IQueryable<User> Apply(IQueryable<User> query) => query
        .Include(u => u.RefreshTokens.Where(rt => !rt.IsRevoked))
        .Where(user => user.PhoneNumber.Value == _phoneNumber);
}
