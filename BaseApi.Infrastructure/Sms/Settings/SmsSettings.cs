namespace BaseApi.Infrastructure.Sms.Settings;

public class SmsSettings
{
    public const string SettingsKey = "Sms";

    public string AccessKey { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;
}