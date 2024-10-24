using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithPhoneNumberAndLastPhoneNumberVerificationCodeSpecification : Specification<User>
{
    private readonly PhoneNumber _phoneNumber;
    
    internal UserWithPhoneNumberAndLastPhoneNumberVerificationCodeSpecification(PhoneNumber phoneNumber) => _phoneNumber = phoneNumber;

    internal override IQueryable<User> Apply(IQueryable<User> query) => query
        .Include(u => u.PhoneNumberVerificationCodes
            .OrderByDescending(pvc => pvc.CreatedOnUtc)
            .Take(1))
        .Where(u => u.PhoneNumber.Value == _phoneNumber);
}