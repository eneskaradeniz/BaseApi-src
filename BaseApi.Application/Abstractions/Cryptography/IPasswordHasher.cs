using BaseApi.Domain.Users;

namespace BaseApi.Application.Abstractions.Cryptography;

public interface IPasswordHasher
{
    string HashPassword(Password password);
}