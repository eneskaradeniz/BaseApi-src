using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Users.Commands.UpdatePhoneNumber;

public sealed record UpdatePhoneNumberCommand(string PhoneNumber) : ICommand<Result>;
