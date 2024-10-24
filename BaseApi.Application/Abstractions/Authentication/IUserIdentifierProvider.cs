using BaseApi.Domain.Users;

namespace BaseApi.Application.Abstractions.Authentication;

public interface IUserIdentifierProvider
{
    UserId UserId { get; }
}