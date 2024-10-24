using BaseApi.Domain.Enums;

namespace BaseApi.Application.Abstractions.Storage;

public interface IStorageService : IStorage
{
    public StorageType StorageType { get; }
}