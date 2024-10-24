using BaseApi.Contracts.Files;
using BaseApi.Domain.Enums;
using BaseApi.Domain.Files;

namespace BaseApi.Application.Abstractions.Storage;

public interface IStorage
{
    StorageType StorageType { get; }
    
    Task<FileId> UploadAsync(Stream stream, string contentType, string path, CancellationToken cancellationToken = default);

    Task<FileResponse> DownloadAsync(FileId fileId, string path, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(FileId fileId, string path, CancellationToken cancellationToken = default);
}