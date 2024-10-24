using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Users.Commands.RemoveUser;

public sealed record RemoveUserCommand(Guid UserId) : ICommand<Result>;