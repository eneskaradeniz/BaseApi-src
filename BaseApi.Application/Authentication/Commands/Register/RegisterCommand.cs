using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword) : ICommand<Result>;
