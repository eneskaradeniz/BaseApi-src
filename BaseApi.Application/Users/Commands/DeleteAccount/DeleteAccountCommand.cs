using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Users.Commands.DeleteAccount;

public sealed record DeleteAccountCommand : ICommand<Result>;
