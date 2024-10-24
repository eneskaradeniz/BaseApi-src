using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Common;
using BaseApi.Domain.Users;
using BaseApi.Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BaseApi.Infrastructure.Authentication;

internal class TokenProvider(
    IOptions<JwtSettings> jwtOptions, 
    IDateTime dateTime, 
    IPermissionService permissionService) : ITokenProvider
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<string> CreateAccessTokenAsync(UserId userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString())
        };

        HashSet<string> permissions = await permissionService
            .GetPermissionsAsync(userId);

        claims.AddRange(permissions.Select(permission => new Claim(CustomClaimTypes.Permissions, permission)));

        DateTime tokenExpirationTime = dateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string CreateBase64Token()
    {
        byte[] number = new byte[16];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return BitConverter.ToString(number).Replace("-", "").ToLower();
    }
}