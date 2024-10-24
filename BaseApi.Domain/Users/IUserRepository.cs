using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Roles;

namespace BaseApi.Domain.Users;

public interface IUserRepository
{
    Task<Maybe<User>> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByEmailWithActiveRefreshTokensAsync(Email email, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByEmailWithLastEmailVerificationCodeAsync(Email email, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByEmailWithLastPasswordResetTokenAsync(Email email, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByPhoneNumberAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByPhoneNumberWithLastPhoneNumberVerificationCodeAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByPhoneNumberWithActiveRefreshTokensAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<Maybe<User>> GetByIdWithUserRole(UserId userId, RoleId roleId, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);

    Task<bool> IsPhoneNumberUniqueAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);

    void Insert(User user);

    void Update(User user);

    void Remove(User user);
}
