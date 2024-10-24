using BaseApi.Application.Abstractions.Storage;
using BaseApi.Contracts.Files;
using BaseApi.Domain.Enums;
using BaseApi.Domain.Files;

namespace BaseApi.Infrastructure.Storage;

internal sealed class StorageService(IStorage storage) : IStorageService
{
    public StorageType StorageType => storage.StorageType;

    public async Task<FileId> UploadAsync(Stream stream, string contentType, string path, CancellationToken cancellationToken = default)
        => await storage.UploadAsync(stream, contentType, path, cancellationToken);

    public async Task<FileResponse> DownloadAsync(FileId fileId, string path, CancellationToken cancellationToken = default)
        => await storage.DownloadAsync(fileId, path, cancellationToken);

    public async Task DeleteAsync(FileId fileId, string path, CancellationToken cancellationToken = default)
        => await storage.DeleteAsync(fileId, path, cancellationToken);
}