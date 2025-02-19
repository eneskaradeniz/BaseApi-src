using Microsoft.IdentityModel.Tokens;

namespace BaseApi.Application.Abstractions.Authentication;

public interface ITokenLifetimeManager
{
    bool ValidateTokenLifetime(DateTime? notBefore,
        DateTime? expires,
        SecurityToken securityToken,
        TokenValidationParameters validationParameters);

    Task LogoutAsync(string securityToken);
}
