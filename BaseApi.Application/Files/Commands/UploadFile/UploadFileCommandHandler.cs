using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Application.Abstractions.Storage;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Files;

namespace BaseApi.Application.Files.Commands.UploadFile;

internal sealed class UploadFileCommandHandler(
    IFileRepository fileRepository,
    IUnitOfWork unitOfWork,
    IStorageService storageService)
    : ICommandHandler<UploadFileCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        using Stream stream = request.File.OpenReadStream();

        FileId fileId = await storageService.UploadAsync(
            stream, 
            request.File.ContentType, 
            request.Path, 
            cancellationToken);

        var file = Domain.Files.File.Create(
            fileId,
            request.File.FileName,
            request.Path,
            request.File.ContentType,
            request.File.Length,
            storageService.StorageType);

        fileRepository.Insert(file);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(fileId.Value);
    }
}