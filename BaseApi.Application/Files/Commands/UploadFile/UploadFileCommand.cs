using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Http;

namespace BaseApi.Application.Files.Commands.UploadFile;

public sealed record UploadFileCommand(IFormFile File, string Path) : ICommand<Result<Guid>>;