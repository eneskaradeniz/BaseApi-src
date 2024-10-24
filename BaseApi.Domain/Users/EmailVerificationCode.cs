using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Users;

public sealed class EmailVerificationCode : Entity<EmailVerificationCodeId, Guid>
{
    public const int ExpiryMinutes = 2;

    public UserId UserId { get; private set; }
    
    public VerificationCode VerificationCode { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime ExpiresOnUtc { get; }

    public bool IsUsed { get; private set; }

    private EmailVerificationCode()
    {
    }
    
    private EmailVerificationCode(
        UserId userId,
        VerificationCode verificationCode, 
        DateTime createdOnUtc, 
        DateTime expiresOnUtc) : base(new EmailVerificationCodeId(Guid.NewGuid()))
    {
        UserId = userId;
        VerificationCode = verificationCode;
        CreatedOnUtc = createdOnUtc;
        ExpiresOnUtc = expiresOnUtc;
        IsUsed = false;
    }
    
    public static EmailVerificationCode Create(
        UserId userId,
        VerificationCode verificationCode, 
        DateTime utcNow)
    {
        return new EmailVerificationCode(
            userId, 
            verificationCode, 
            utcNow,
            utcNow.AddMinutes(ExpiryMinutes));
    }

    public bool IsExpired(DateTime utcNow) => utcNow > ExpiresOnUtc;

    public void MarkAsUsed() => IsUsed = true;
}
