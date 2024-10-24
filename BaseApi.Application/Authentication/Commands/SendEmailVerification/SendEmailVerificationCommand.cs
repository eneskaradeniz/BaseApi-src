using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.SendEmailVerification;

public sealed record SendEmailVerificationCommand(string Email) : ICommand<Result>;