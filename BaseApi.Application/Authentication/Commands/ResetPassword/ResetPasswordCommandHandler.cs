using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Cryptography;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.ResetPassword;

internal sealed class ResetPasswordCommandHandler(
    IPasswordHasher passwordHasher,
    IPasswordHashChecker passwordHashChecker,
    IUserRepository userRepository,
    IDateTime dateTime,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        Result<Password> passwordResult = Password.Create(request.Password);
        Result<Password> confirmPasswordResult = Password.Create(request.ConfirmPassword);

        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(
            emailResult,
            passwordResult,
            confirmPasswordResult);

        if (firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure(firstFailureOrSuccess.Error);
        }

        if (!passwordResult.Value.Equals(confirmPasswordResult.Value))
        {
            return Result.Failure(DomainErrors.User.PasswordsDoNotMatch);
        }

        Maybe<User> maybeUser = await userRepository.GetByEmailWithLastPasswordResetTokenAsync(emailResult.Value, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        string passwordHash = passwordHasher.HashPassword(passwordResult.Value);

        Result resetPasswordResult = user.ResetPassword(request.Token, passwordResult.Value, passwordHash, dateTime.UtcNow, passwordHashChecker);

        if (resetPasswordResult.IsFailure)
        {
            return Result.Failure(resetPasswordResult.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}