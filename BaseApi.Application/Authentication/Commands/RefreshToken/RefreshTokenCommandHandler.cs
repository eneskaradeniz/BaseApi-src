using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Authentication;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.RefreshToken;

internal sealed class RefreshTokenCommandHandler(
    IUserRepository userRepository,
    IDateTime dateTime,
    IUnitOfWork unitOfWork,
    ITokenProvider tokenProvider)
    : ICommandHandler<RefreshTokenCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await userRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure<TokenResponse>(DomainErrors.RefreshToken.InvalidRefreshToken);
        }

        User user = maybeUser.Value;

        Result resultRevokeToken = user.RevokeRefreshToken(request.RefreshToken, dateTime.UtcNow);

        if (resultRevokeToken.IsFailure)
        {
            return Result.Failure<TokenResponse>(resultRevokeToken.Error);
        }

        string newRefreshToken = tokenProvider.CreateBase64Token();

        user.AddRefreshToken(newRefreshToken, dateTime.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        string accessToken = await tokenProvider.CreateAccessTokenAsync(user.Id);

        return Result.Success(new TokenResponse(accessToken, newRefreshToken));
    }
}