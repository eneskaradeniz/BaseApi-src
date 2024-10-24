using BaseApi.Application.Abstractions.Emails;
using BaseApi.Contracts.Emails;
using BaseApi.Infrastructure.Emails.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace BaseApi.Infrastructure.Emails;

internal class EmailService(IOptions<MailSettings> mailSettingsOptions) : IEmailService
{
    private readonly MailSettings _mailSettings = mailSettingsOptions.Value;

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        TextFormat textFormat = mailRequest.IsHtml ? TextFormat.Html : TextFormat.Text;

        var email = new MimeMessage
        {
            From =
            {
                new MailboxAddress(_mailSettings.SenderDisplayName, _mailSettings.SenderEmail)
            },
            To =
            {
                MailboxAddress.Parse(mailRequest.EmailTo)
            },
            Subject = mailRequest.Subject,
            Body = new TextPart(textFormat)
            {
                Text = mailRequest.Body
            }
        };

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpPort, SecureSocketOptions.SslOnConnect);

        await smtpClient.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.SmtpPassword);

        await smtpClient.SendAsync(email);

        await smtpClient.DisconnectAsync(true);
    }
}