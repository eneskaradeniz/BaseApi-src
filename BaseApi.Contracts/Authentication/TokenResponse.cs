namespace BaseApi.Contracts.Authentication;

public sealed class TokenResponse(string accessToken, string refreshToken)
{
    public string AccessToken { get; private set; } = accessToken;

    public string RefreshToken { get; private set; } = refreshToken;
}