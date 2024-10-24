namespace BaseApi.Contracts.Users;

public sealed record PhoneNumberVerificationRequest(string PhoneNumber, string VerificationCode);