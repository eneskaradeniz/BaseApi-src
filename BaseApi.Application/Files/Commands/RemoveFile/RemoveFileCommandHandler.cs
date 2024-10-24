using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Application.Abstractions.Storage;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Files;

namespace BaseApi.Application.Files.Commands.RemoveFile;

internal sealed class RemoveFileCommandHandler(
    IFileRepository fileRepository,
    IUnitOfWork unitOfWork,
    IStorageService storageService) 
    : ICommandHandler<RemoveFileCommand, Result>
{
    public async Task<Result> Handle(RemoveFileCommand request, CancellationToken cancellationToken)
    {
        FileId fileId = new(request.FileId);

        Maybe<Domain.Files.File> maybeFile = await fileRepository.GetByIdAsync(fileId, cancellationToken);

        if (maybeFile.HasNoValue)
        {
            return Result.Failure(DomainErrors.File.FileNotFound);
        }

        Domain.Files.File file = maybeFile.Value;
        
        await storageService.DeleteAsync(fileId, file.Path, cancellationToken);

        fileRepository.Remove(file);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}