using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Roles.Queries.GetRolesWithPermissions;

public sealed record GetRolesWithPermissionsQuery : IQuery<Maybe<List<RoleWithPermissionsResponse>>>;
