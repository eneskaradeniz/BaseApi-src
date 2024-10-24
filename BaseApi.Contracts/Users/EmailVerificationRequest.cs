namespace BaseApi.Contracts.Users;

public sealed record EmailVerificationRequest(string Email, string VerificationCode);