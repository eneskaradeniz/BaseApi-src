using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Files.Commands.RemoveFile;

public sealed record RemoveFileCommand(Guid FileId) : ICommand<Result>;