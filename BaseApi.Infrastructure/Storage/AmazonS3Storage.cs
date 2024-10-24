using Amazon.S3;
using Amazon.S3.Model;
using BaseApi.Application.Abstractions.Storage;
using BaseApi.Contracts.Files;
using BaseApi.Domain.Enums;
using BaseApi.Domain.Files;
using System.Net;

namespace BaseApi.Infrastructure.Storage;

internal sealed class AmazonS3Storage(IAmazonS3 s3Client) : IAmazonS3Storage
{
    public StorageType StorageType => StorageType.S3;

    public async Task<FileId> UploadAsync(Stream stream, string contentType, string path, CancellationToken cancellationToken = default)
    {
        Guid fileId = Guid.NewGuid();

        if (!await BucketExistsAsync(path, cancellationToken))
        {
            var response = await s3Client.PutBucketAsync(path, cancellationToken);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Bucket could not be created.");
            }
        }

        PutObjectRequest putRequest = new()
        {
            BucketName = path,
            Key = fileId.ToString(),
            InputStream = stream,
            ContentType = contentType,
            CannedACL = S3CannedACL.PublicRead
        };

        await s3Client.PutObjectAsync(putRequest, cancellationToken);

        return new FileId(fileId);
    }

    public async Task<FileResponse> DownloadAsync(FileId fileId, string path, CancellationToken cancellationToken = default)
    {
        GetObjectRequest getRequest = new()
        {
            BucketName = path,
            Key = fileId.ToString()
        };

        using GetObjectResponse response = await s3Client.GetObjectAsync(getRequest, cancellationToken);

        return new FileResponse(response.ResponseStream, response.Headers.ContentType);
    }

    public async Task DeleteAsync(FileId fileId, string path, CancellationToken cancellationToken = default)
    {
        DeleteObjectRequest deleteRequest = new()
        {
            BucketName = path,
            Key = fileId.ToString()
        };

        await s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);
    }

    private async Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        ListBucketsResponse response = await s3Client.ListBucketsAsync(cancellationToken);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Could not list buckets.");
        }

        return response.Buckets.Any(b => b.BucketName == bucketName);
    }
}