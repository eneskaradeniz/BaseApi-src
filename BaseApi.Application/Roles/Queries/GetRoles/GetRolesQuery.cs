using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Roles.Queries.GetRoles;

public sealed record GetRolesQuery : IQuery<Maybe<List<RoleResponse>>>;