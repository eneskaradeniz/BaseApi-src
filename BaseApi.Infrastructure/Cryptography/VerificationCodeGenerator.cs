using BaseApi.Application.Abstractions.Cryptography;
using BaseApi.Domain.Users;

namespace BaseApi.Infrastructure.Cryptography;

internal sealed class VerificationCodeGenerator : IVerificationCodeGenerator
{
    public VerificationCode Generate()
    {
        string verificationCode = new Random().Next(100000, 999999).ToString();

        return VerificationCode.Create(verificationCode).Value;
    }
}