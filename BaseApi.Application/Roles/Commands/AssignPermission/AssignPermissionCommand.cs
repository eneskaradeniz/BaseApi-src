using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Roles.Commands.AssignPermission;

public sealed record AssignPermissionCommand(Guid RoleId, int PermissionId) : ICommand<Result>;