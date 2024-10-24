using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Authentication;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.Login;

internal class LoginCommandHandler(
    IUserRepository userRepository,
    IPasswordHashChecker passwordHashChecker,
    IDateTime dateTime,
    IUnitOfWork unitOfWork,
    ITokenProvider tokenProvider)
    : ICommandHandler<LoginCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.EmailOrPhoneNumber);
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.EmailOrPhoneNumber);

        if (emailResult.IsFailure && phoneNumberResult.IsFailure)
        {
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidCredentials);
        }

        Maybe<User> maybeUser;
        if (emailResult.IsSuccess)
        {
            maybeUser = await userRepository.GetByEmailWithActiveRefreshTokensAsync(emailResult.Value, cancellationToken);
        }
        else
        {
            maybeUser = await userRepository.GetByPhoneNumberWithActiveRefreshTokensAsync(phoneNumberResult.Value, cancellationToken);
        }

        if (maybeUser.HasNoValue)
        {
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidCredentials);
        }

        User user = maybeUser.Value;

        bool passwordValid = user.VerifyPasswordHash(request.Password, passwordHashChecker);

        if (!passwordValid)
        {
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidCredentials);
        }

        Result ensureUserIsVerifiedResult = user.EnsureUserIsVerified();

        if (ensureUserIsVerifiedResult.IsFailure)
        {
            return Result.Failure<TokenResponse>(ensureUserIsVerifiedResult.Error);
        }

        user.RevokeOldRefreshTokens(dateTime.UtcNow);

        string refreshToken = tokenProvider.CreateBase64Token();

        user.AddRefreshToken(refreshToken, dateTime.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        string accessToken = await tokenProvider.CreateAccessTokenAsync(user.Id);

        return Result.Success(new TokenResponse(accessToken, refreshToken));
    }
}