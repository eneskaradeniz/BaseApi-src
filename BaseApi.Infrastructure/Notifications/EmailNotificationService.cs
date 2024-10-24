using BaseApi.Application.Abstractions.Emails;
using BaseApi.Application.Abstractions.Notifications;
using BaseApi.Application.Infrastructure;
using BaseApi.Contracts.Emails;
using Microsoft.Extensions.Options;

namespace BaseApi.Infrastructure.Notifications;

internal sealed class EmailNotificationService(
    IEmailService emailService, 
    IOptions<BaseUrlsSettings> baseUrlsSettings) : IEmailNotificationService
{
    private readonly BaseUrlsSettings _baseUrlsSettings = baseUrlsSettings.Value;

    public async Task SendEmailVerificationEmail(EmailVerificationEmail emailVerificationEmail)
    {
        var mailRequest = new MailRequest(
            emailVerificationEmail.EmailTo,
            "Email Verification",
            $"Your email verification code is: {emailVerificationEmail.VerificationCode}",
            false);

        await emailService.SendEmailAsync(mailRequest);
    }

    public async Task SendPasswordResetEmail(PasswordResetEmail passwordResetEmail)
    {
        var mailRequest = new MailRequest(
            passwordResetEmail.EmailTo,
            "Password Reset",
            $"Click here to reset your password: {_baseUrlsSettings.Web}/reset-password?token={passwordResetEmail.Token}",
            false);

        await emailService.SendEmailAsync(mailRequest);
    }
}