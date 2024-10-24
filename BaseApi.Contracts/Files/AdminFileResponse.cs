namespace BaseApi.Contracts.Files;

public sealed class AdminFileResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public string ContentType { get; set; }

    public long Size { get; set; }

    public string StorageType { get; set; }

    public DateTime CreatedOnUtc { get; set; }
}