using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Users.Commands.UpdateEmail;

public sealed record UpdateEmailCommand(string Email) : ICommand<Result>;