using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.SendPhoneNumberVerification;

public sealed record SendPhoneNumberVerificationCommand(string PhoneNumber) : ICommand<Result>;