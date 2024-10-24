using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using BaseApi.Application.Abstractions.Sms;
using BaseApi.Contracts.Sms;
using BaseApi.Infrastructure.Sms.Settings;
using Microsoft.Extensions.Options;

namespace BaseApi.Infrastructure.Sms;

internal sealed class SmsService : ISmsService
{
    private readonly SmsSettings _smsSettings;

    private readonly IAmazonSimpleNotificationService _snsClient;

    public SmsService(IOptions<SmsSettings> awsSettingsOptions)
    {
        _smsSettings = awsSettingsOptions.Value;

        var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(_smsSettings.AccessKey, _smsSettings.SecretKey);

        _snsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, RegionEndpoint.GetBySystemName(_smsSettings.Region));
    }

    public async Task SendSmsAsync(SmsRequest smsRequest)
    {
        var request = new PublishRequest
        {
            Message = smsRequest.Message,
            PhoneNumber = smsRequest.PhoneNumber
        };

        var response = await _snsClient.PublishAsync(request);

        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            // Hata yönetimi
            throw new Exception("SMS gönderimi başarısız oldu.");
        }
    }
}