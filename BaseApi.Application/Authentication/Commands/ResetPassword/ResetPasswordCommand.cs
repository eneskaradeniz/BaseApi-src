using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.ResetPassword;

public sealed record ResetPasswordCommand(string Token, string Email, string Password, string ConfirmPassword) : ICommand<Result>;