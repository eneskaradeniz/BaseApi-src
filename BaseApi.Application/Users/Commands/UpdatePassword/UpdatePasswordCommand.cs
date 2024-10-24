using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Users.Commands.UpdatePassword;

public sealed record UpdatePasswordCommand(
    string CurrentPassword, 
    string NewPassword, 
    string ConfirmNewPassword) : ICommand<Result>;