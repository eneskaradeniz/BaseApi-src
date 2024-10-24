using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BaseApi.Application.Abstractions.Storage;
using BaseApi.Contracts.Files;
using BaseApi.Domain.Enums;
using BaseApi.Domain.Files;

namespace BaseApi.Infrastructure.Storage;

internal sealed class AzureBlobStorage(BlobServiceClient blobServiceClient) : IAzureBlobStorage
{
    public StorageType StorageType => StorageType.Blob;
    
    public async Task<FileId> UploadAsync(Stream stream, string contentType, string path, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(path);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        Guid fileId = Guid.NewGuid();
        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        await blobClient.UploadAsync(
            stream,
            new BlobHttpHeaders { ContentType = contentType },
            cancellationToken: cancellationToken);

        return new FileId(fileId);
    }

    public async Task<FileResponse> DownloadAsync(FileId fileId, string path, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(path);

        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

        return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }
    
    public async Task DeleteAsync(FileId fileId, string path, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(path);

        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}