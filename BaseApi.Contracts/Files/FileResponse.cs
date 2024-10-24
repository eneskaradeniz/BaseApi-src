namespace BaseApi.Contracts.Files;

public sealed class FileResponse(Stream stream, string contentType)
{
    public Stream Stream { get; set; } = stream;

    public string ContentType { get; set; } = contentType;
}