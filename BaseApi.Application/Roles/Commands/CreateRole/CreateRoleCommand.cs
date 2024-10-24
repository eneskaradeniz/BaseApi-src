using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand(string Name) : ICommand<Result>;