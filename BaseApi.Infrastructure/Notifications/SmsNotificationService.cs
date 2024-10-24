using BaseApi.Application.Abstractions.Notifications;
using BaseApi.Application.Abstractions.Sms;
using BaseApi.Contracts.Sms;

namespace BaseApi.Infrastructure.Notifications;

internal sealed class SmsNotificationService(ISmsService smsService) : ISmsNotificationService
{
    public async Task SendPhoneNumberVerificationSms(PhoneNumberVerificationSms phoneNumberVerificationSms)
    {
        var message = new SmsRequest(
            phoneNumberVerificationSms.PhoneNumber,
            $"Your verification code for the BaseApi application is {phoneNumberVerificationSms.VerificationCode}");

        await smsService.SendSmsAsync(message);
    }
}