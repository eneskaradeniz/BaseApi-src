namespace BaseApi.Contracts.Sms;

public sealed record PhoneNumberVerificationSms(string PhoneNumber, string VerificationCode);