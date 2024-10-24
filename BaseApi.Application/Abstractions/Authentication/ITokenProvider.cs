using BaseApi.Domain.Users;

namespace BaseApi.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    Task<string> CreateAccessTokenAsync(UserId userId);

    string CreateBase64Token();
}