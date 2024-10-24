using Microsoft.AspNetCore.Http;

namespace BaseApi.Contracts.Files;

public sealed record UploadFileRequest(IFormFile File, string Path);
