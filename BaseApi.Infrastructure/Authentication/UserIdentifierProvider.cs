using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Domain.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BaseApi.Infrastructure.Authentication;

internal class UserIdentifierProvider : IUserIdentifierProvider
{
    public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
    {
        string? userIdClaim = httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));
        }

        UserId = new UserId(Guid.Parse(userIdClaim));
    }

    public UserId UserId { get; }
}