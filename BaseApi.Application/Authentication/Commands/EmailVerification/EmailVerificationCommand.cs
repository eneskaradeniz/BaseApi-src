using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.EmailVerification;

public sealed record EmailVerificationCommand(string Email, string VerificationCode) : ICommand<Result>;