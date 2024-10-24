namespace BaseApi.Infrastructure.Emails.Settings;

public class MailSettings
{
    public const string SettingsKey = "Mail";

    public string SenderDisplayName { get; set; } = string.Empty;

    public string SenderEmail { get; set; } = string.Empty;

    public string SmtpPassword { get; set; } = string.Empty;

    public string SmtpServer { get; set; } = string.Empty;

    public int SmtpPort { get; set; }
}