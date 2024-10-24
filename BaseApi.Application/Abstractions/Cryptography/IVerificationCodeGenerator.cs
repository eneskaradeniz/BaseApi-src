using BaseApi.Domain.Users;

namespace BaseApi.Application.Abstractions.Cryptography;

public interface IVerificationCodeGenerator
{
    VerificationCode Generate();
}