using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithEmailAndLastEmailVerificationCodeSpecification : Specification<User>
{
    private readonly Email _email;
    
    internal UserWithEmailAndLastEmailVerificationCodeSpecification(Email email) => _email = email;

    internal override IQueryable<User> Apply(IQueryable<User> query) => query
        .Include(u => u.EmailVerificationCodes
            .OrderByDescending(pvc => pvc.CreatedOnUtc)
            .Take(1))
        .Where(u => u.Email.Value == _email);
}