using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Permissions.Queries.GetPermissions;

public sealed record GetPermissionsQuery : IQuery<Maybe<List<PermissionResponse>>>;
