using BaseApi.Application.Abstractions.Data;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Users;
using BaseApi.Persistence.Specifications;

namespace BaseApi.Persistence.Repositories;

internal sealed class UserRepository(IApplicationDbContext dbContext)
    : GenericRepository<User, UserId, Guid>(dbContext), IUserRepository
{
    public async Task<Maybe<User>> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        => await FirstOrDefaultAsync(new UserWithEmailSpecification(email), cancellationToken);

    public async Task<Maybe<User>> GetByEmailWithActiveRefreshTokensAsync(Email email, CancellationToken cancellationToken = default) => await FirstOrDefaultAsync(new UserWithEmailAndActiveRefreshTokensSpecification(email), cancellationToken);

    public Task<Maybe<User>> GetByEmailWithLastEmailVerificationCodeAsync(Email email,
        CancellationToken cancellationToken = default)
        => FirstOrDefaultAsync(new UserWithEmailAndLastEmailVerificationCodeSpecification(email), cancellationToken);

    public async Task<Maybe<User>> GetByEmailWithLastPasswordResetTokenAsync(Email email, CancellationToken cancellationToken = default)
        => await FirstOrDefaultAsync(new UserWithEmailAndLastPasswordResetTokenSpecification(email), cancellationToken);

    public async Task<Maybe<User>> GetByPhoneNumberAsync(PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default)
        => await FirstOrDefaultAsync(new UserWithPhoneNumberSpecification(phoneNumber), cancellationToken);

    public Task<Maybe<User>> GetByPhoneNumberWithActiveRefreshTokensAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default) => FirstOrDefaultAsync(new UserWithPhoneNumberAndActiveRefreshTokensSpecification(phoneNumber), cancellationToken);

    public Task<Maybe<User>> GetByPhoneNumberWithLastPhoneNumberVerificationCodeAsync(PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default)
        => FirstOrDefaultAsync(new UserWithPhoneNumberAndLastPhoneNumberVerificationCodeSpecification(phoneNumber), cancellationToken);

    public async Task<Maybe<User>> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        => await FirstOrDefaultAsync(new UserWithRefreshToken(refreshToken), cancellationToken);

    public Task<Maybe<User>> GetByIdWithUserRole(UserId userId, RoleId roleId, CancellationToken cancellationToken = default) => FirstOrDefaultAsync(new UserWithUserRoleSpecification(userId, roleId), cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        => !await AnyAsync(new UserWithEmailSpecification(email), cancellationToken);

    public async Task<bool> IsPhoneNumberUniqueAsync(PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default)
        => !await AnyAsync(new UserWithPhoneNumberSpecification(phoneNumber), cancellationToken);
}
