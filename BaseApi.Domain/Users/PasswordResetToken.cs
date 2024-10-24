using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Users;

public sealed class PasswordResetToken : Entity<PasswordResetTokenId, Guid>
{
    public const int ExpiryHours = 1;

    public UserId UserId { get; private set; }

    public string Token { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime ExpiresOnUtc { get; }

    public bool IsUsed { get; private set; }

    private PasswordResetToken()
    {
    }

    private PasswordResetToken(
        UserId userId, 
        string token, 
        DateTime createdOnUtc,
        DateTime expiresOnUtc)
        : base(new PasswordResetTokenId(Guid.NewGuid()))
    {
        UserId = userId;
        Token = token;
        CreatedOnUtc = createdOnUtc;
        ExpiresOnUtc = expiresOnUtc;
        IsUsed = false;
    }
    
    public static PasswordResetToken Create(
        UserId userId, 
        string token, 
        DateTime utcNow)
    {
        return new PasswordResetToken(
            userId, 
            token, 
            utcNow, 
            utcNow.AddHours(ExpiryHours));
    }

    public bool IsExpired(DateTime utcNow) => utcNow > ExpiresOnUtc;

    public void MarkAsUsed() => IsUsed = true;
}