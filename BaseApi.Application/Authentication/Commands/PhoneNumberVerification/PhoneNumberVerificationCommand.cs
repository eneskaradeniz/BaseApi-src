using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.PhoneNumberVerification;

public sealed record PhoneNumberVerificationCommand(string PhoneNumber, string VerificationCode) : ICommand<Result>;