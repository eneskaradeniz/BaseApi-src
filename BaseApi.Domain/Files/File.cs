using BaseApi.Domain.Core.Abstractions;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApi.Domain.Files;

public class File : Entity<FileId, Guid>, IAuditableEntity
{
    public string Name { get; private set; }

    public string Path { get; private set; }

    public string ContentType { get; private set; }

    public long Size { get; private set; }

    public StorageType StorageType { get; private set; }

    public DateTime CreatedOnUtc { get; }

    [NotMapped]
    public DateTime? ModifiedOnUtc { get; }

    private File()
    {
    }

    private File(FileId id, string name, string path, string contentType, long size, StorageType storageType) : base(id)
    {
        Name = name;
        Path = path;
        ContentType = contentType;
        Size = size;
        StorageType = storageType;
    }
    
    public static File Create(FileId id, string name, string path, string contentType, long size, StorageType storageType)
    {
        return new File(id, name, path, contentType, size, storageType);
    }
}