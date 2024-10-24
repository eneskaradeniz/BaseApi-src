using BaseApi.Contracts.Sms;

namespace BaseApi.Application.Abstractions.Sms;

public interface ISmsService
{
    Task SendSmsAsync(SmsRequest smsRequest);
}