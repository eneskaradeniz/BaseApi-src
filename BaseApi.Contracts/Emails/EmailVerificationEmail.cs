namespace BaseApi.Contracts.Emails;

public sealed record EmailVerificationEmail(string EmailTo, string VerificationCode);