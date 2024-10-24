using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Roles.Queries.GetRoleById;

public sealed record GetRoleByIdQuery(Guid RoleId) : IQuery<Maybe<RoleResponse>>;