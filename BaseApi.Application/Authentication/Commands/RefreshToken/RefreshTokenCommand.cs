using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Authentication;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<Result<TokenResponse>>;