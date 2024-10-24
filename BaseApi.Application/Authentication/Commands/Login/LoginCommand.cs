using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Authentication;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.Login;

public sealed record LoginCommand(string EmailOrPhoneNumber, string Password)
    : ICommand<Result<TokenResponse>>;