using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Caching;
using BaseApi.Application.Abstractions.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BaseApi.Infrastructure.Authentication;

public class JwtTokenLifetimeManager(
    ICacheService cacheService,
    IDateTime dateTime) : ITokenLifetimeManager
{
    private const string CachePrefix = "token:blacklist:";

    public bool ValidateTokenLifetime(
        DateTime? notBefore,
        DateTime? expires,
        SecurityToken securityToken,
        TokenValidationParameters validationParameters)
    {
        if (securityToken is JsonWebToken token)
        {
            var cacheKey = $"{CachePrefix}{token.EncodedSignature}";
            var cachedValue = cacheService.Get<string>(cacheKey);
            return token.ValidFrom <= dateTime.UtcNow &&
                   token.ValidTo >= dateTime.UtcNow &&
                   cachedValue == default;
        }

        return false;
    }
    
    public async Task LogoutAsync(string bearerToken)
    {
        JsonWebToken securityToken = new(bearerToken);

        var cacheKey = $"{CachePrefix}{securityToken.EncodedSignature}";
        var expirationTime = securityToken.ValidTo;

        await cacheService.SetAsync(
            cacheKey,
            string.Empty,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime - dateTime.UtcNow
            });
    }
}
