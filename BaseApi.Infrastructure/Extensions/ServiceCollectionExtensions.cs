using Amazon.S3;
using Azure.Storage.Blobs;
using BaseApi.Application.Abstractions.Storage;
using BaseApi.Domain.Enums;
using BaseApi.Infrastructure.Infrastructure;
using BaseApi.Infrastructure.Storage;
using BaseApi.Infrastructure.Storage.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BaseApi.Infrastructure.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(
        this IServiceCollection services,
        StorageType storageType,
        IConfiguration configuration)
    {
        switch (storageType)
        {
            case StorageType.Blob:
                string azureBlobConnectionString = configuration.GetConnectionString(AzureBlobConnectionString.SettingsKey)!;

                services.AddSingleton(new AzureBlobConnectionString(azureBlobConnectionString));

                services.AddSingleton(_ => new BlobServiceClient
                    (azureBlobConnectionString));

                services.AddSingleton<IStorage, AzureBlobStorage>();
                break;

            case StorageType.S3:
                services.AddSingleton<IAmazonS3>(serviceProvider =>
                {
                    AmazonS3Settings s3Settings = serviceProvider.GetRequiredService<IOptions<AmazonS3Settings>>().Value;

                    return new AmazonS3Client(
                        s3Settings.AccessKey,
                        s3Settings.SecretKey,
                        new AmazonS3Config
                        {
                            ServiceURL = s3Settings.ServiceURL,
                            ForcePathStyle = true,
                            UseHttp = true
                        });
                });

                services.AddSingleton<IStorage, AmazonS3Storage>();
                break;

            default:
                throw new NotImplementedException($"Storage type {storageType} is not implemented.");
        }

        services.AddSingleton<IStorageService, StorageService>();

        return services;
    }
}