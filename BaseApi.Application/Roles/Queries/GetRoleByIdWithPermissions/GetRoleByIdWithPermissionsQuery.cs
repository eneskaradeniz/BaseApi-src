using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Roles.Queries.GetRoleByIdWithPermissions;

public sealed record GetRoleByIdWithPermissionsQuery(Guid RoleId) : IQuery<Maybe<RoleWithPermissionsResponse>>;
