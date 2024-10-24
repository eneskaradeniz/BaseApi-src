using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Users.Commands.UpdateMe;

public sealed record UpdateMeCommand(string FirstName, string LastName) : ICommand<Result>;