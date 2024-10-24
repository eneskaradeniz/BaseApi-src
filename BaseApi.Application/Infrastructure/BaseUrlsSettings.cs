namespace BaseApi.Application.Infrastructure;

public class BaseUrlsSettings
{
    public const string SettingsKey = "BaseUrls";
    
    public string Web { get; set; } = string.Empty;

    public string Storage { get; set; } = string.Empty;
}
