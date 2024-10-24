namespace BaseApi.Domain.Users;

public interface IPasswordHashChecker
{
    bool HashesMatch(string password, string passwordHash);
}