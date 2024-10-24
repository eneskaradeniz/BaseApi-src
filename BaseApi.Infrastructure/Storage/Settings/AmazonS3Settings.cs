namespace BaseApi.Infrastructure.Storage.Settings;

public class AmazonS3Settings
{
    public const string SettingsKey = "AmazonS3";

    public string AccessKey { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public string ServiceURL { get; set; } = string.Empty;
}
