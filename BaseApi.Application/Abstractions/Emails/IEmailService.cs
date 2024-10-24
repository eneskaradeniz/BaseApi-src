using BaseApi.Contracts.Emails;

namespace BaseApi.Application.Abstractions.Emails;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}