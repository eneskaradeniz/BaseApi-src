using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Cryptography;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Authentication;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Users.Commands.UpdatePassword;

internal sealed class UpdatePasswordCommandHandler(
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider,
    IPasswordHasher passwordHasher,
    IPasswordHashChecker passwordHashChecker,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdatePasswordCommand, Result>
{
    public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        Result<Password> currentPasswordResult = Password.Create(request.CurrentPassword);
        Result<Password> newPasswordResult = Password.Create(request.NewPassword);
        Result<Password> confirmNewPasswordResult = Password.Create(request.ConfirmNewPassword);

        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(
            currentPasswordResult,
            newPasswordResult,
            confirmNewPasswordResult);

        if (firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure(firstFailureOrSuccess.Error);
        }

        if (!newPasswordResult.Value.Equals(confirmNewPasswordResult.Value))
        {
            return Result.Failure(DomainErrors.User.PasswordsDoNotMatch);
        }

        if (currentPasswordResult.Value.Equals(newPasswordResult.Value))
        {
            return Result.Failure(DomainErrors.User.PasswordSameAsOld);
        }

        Maybe<User> maybeUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        bool passwordValid = user.VerifyPasswordHash(request.CurrentPassword, passwordHashChecker);

        if (!passwordValid)
        {
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidCredentials);
        }

        var newPasswordHash = passwordHasher.HashPassword(newPasswordResult.Value);

        Result changePasswordResult = user.ChangePassword(newPasswordResult.Value, newPasswordHash, passwordHashChecker);

        if (changePasswordResult.IsFailure)
        {
            return Result.Failure(changePasswordResult.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
