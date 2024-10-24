using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithEmailAndLastPasswordResetTokenSpecification : Specification<User>
{
    private readonly Email _email;
    
    internal UserWithEmailAndLastPasswordResetTokenSpecification(Email email) => _email = email;

    internal override IQueryable<User> Apply(IQueryable<User> query) => query
        .Include(u => u.PasswordResetTokens
            .OrderByDescending(prt => prt.CreatedOnUtc)
            .Take(1))
        .Where(u => u.Email.Value == _email);
}
