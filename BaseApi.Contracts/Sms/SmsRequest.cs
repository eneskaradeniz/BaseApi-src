namespace BaseApi.Contracts.Sms;

public sealed record SmsRequest(string PhoneNumber, string Message);