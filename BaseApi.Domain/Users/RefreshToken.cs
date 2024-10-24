using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Users;

public sealed class RefreshToken : Entity<RefreshTokenId, Guid>
{
    public const int ExpiryDays = 7;

    public UserId UserId { get; private set; }

    public string Token { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime ExpiresOnUtc { get; }

    public bool IsRevoked { get; private set; }

    public DateTime? RevokedOnUtc { get; private set; }

    private RefreshToken()
    {
    }

    private RefreshToken(
        UserId userId,
        string token,
        DateTime createdOnUtc,
        DateTime expiresOnUtc) : base(new RefreshTokenId(Guid.NewGuid()))
    {
        UserId = userId;
        Token = token;
        CreatedOnUtc = createdOnUtc;
        ExpiresOnUtc = expiresOnUtc;
        IsRevoked = false;
    }
    
    public static RefreshToken Create(
        UserId userId,
        string token,
        DateTime utcNow)
    {
        return new RefreshToken(
            userId,
            token,
            utcNow, 
            utcNow.AddDays(ExpiryDays));
    }

    public bool IsExpired(DateTime utcNow) => utcNow > ExpiresOnUtc;

    public void Revoke(DateTime utcNow)
    {
        IsRevoked = true;
        RevokedOnUtc = utcNow;
    }
}