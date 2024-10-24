using BaseApi.Contracts.Emails;

namespace BaseApi.Application.Abstractions.Notifications;

public interface IEmailNotificationService
{
    Task SendEmailVerificationEmail(EmailVerificationEmail emailVerificationEmail);

    Task SendPasswordResetEmail(PasswordResetEmail passwordResetEmail);
}