using BaseApi.Domain.Roles;
using System.Linq.Expressions;

namespace BaseApi.Persistence.Specifications;

internal sealed class RoleWithNameSpecification : Specification<Role>
{
    private readonly Name _name;

    internal RoleWithNameSpecification(Name name) => _name = name;

    internal override Expression<Func<Role, bool>> ToExpression() => role => role.Name.Value == _name;
}