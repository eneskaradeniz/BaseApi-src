using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Roles.Commands.RemoveRole;

public sealed record RemoveRoleCommand(Guid RoleId) : ICommand<Result>;