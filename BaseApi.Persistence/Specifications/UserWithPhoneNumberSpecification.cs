using System.Linq.Expressions;
using BaseApi.Domain.Users;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithPhoneNumberSpecification : Specification<User>
{
    private readonly PhoneNumber _phoneNumber;

    internal UserWithPhoneNumberSpecification(PhoneNumber phoneNumber) => _phoneNumber = phoneNumber;

    internal override Expression<Func<User, bool>> ToExpression() => user => user.PhoneNumber.Value == _phoneNumber;
}