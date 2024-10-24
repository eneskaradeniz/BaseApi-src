using BaseApi.Contracts.Sms;

namespace BaseApi.Application.Abstractions.Notifications;

public interface ISmsNotificationService
{
    Task SendPhoneNumberVerificationSms(PhoneNumberVerificationSms phoneNumberVerificationSms);
}