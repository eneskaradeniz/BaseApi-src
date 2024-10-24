using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Roles.Commands.UpdateRole;

public sealed record UpdateRoleCommand(Guid RoleId, string Name) : ICommand<Result>;