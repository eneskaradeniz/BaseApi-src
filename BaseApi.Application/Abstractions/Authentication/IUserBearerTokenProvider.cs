namespace BaseApi.Application.Abstractions.Authentication;

public interface IUserBearerTokenProvider
{
    string BearerToken { get; }
}