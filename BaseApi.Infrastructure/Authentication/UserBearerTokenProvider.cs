using BaseApi.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace BaseApi.Infrastructure.Authentication;

internal class UserBearerTokenProvider : IUserBearerTokenProvider
{
    public UserBearerTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        string? accessToken = httpContextAccessor
            .HttpContext?
            .Request
            .Headers["Authorization"].ToString()
            .Replace("Bearer ", "");

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            throw new ArgumentException("The bearer token is required.", nameof(httpContextAccessor));
        }

        BearerToken = accessToken;
    }

    public string BearerToken { get; }
}