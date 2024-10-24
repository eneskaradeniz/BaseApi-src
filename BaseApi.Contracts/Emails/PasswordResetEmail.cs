namespace BaseApi.Contracts.Emails;

public sealed record PasswordResetEmail(string EmailTo, string Token);