using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : ICommand<Result>;