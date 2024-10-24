using System.Linq.Expressions;
using BaseApi.Domain.Users;

namespace BaseApi.Persistence.Specifications;

internal sealed class UserWithEmailSpecification : Specification<User>
{
    private readonly Email _email;

    internal UserWithEmailSpecification(Email email) => _email = email;

    internal override Expression<Func<User, bool>> ToExpression() => user => user.Email.Value == _email;
}