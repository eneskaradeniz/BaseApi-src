using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.EmailVerification;

internal sealed class EmailVerificationCommandHandler(
    IUserRepository userRepository,
    IDateTime dateTime,
    IUnitOfWork unitOfWork)
    : ICommandHandler<EmailVerificationCommand, Result>
{
    public async Task<Result> Handle(EmailVerificationCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        Result<VerificationCode> verificationCodeResult = VerificationCode.Create(request.VerificationCode);

        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(emailResult, verificationCodeResult);

        if (firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure(firstFailureOrSuccess.Error);
        }

        Maybe<User> maybeUser = await userRepository.GetByEmailWithLastEmailVerificationCodeAsync(emailResult.Value, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        Result emailVerificationResult = user.VerifyEmail(verificationCodeResult.Value, dateTime.UtcNow);

        if (emailVerificationResult.IsFailure)
        {
            return emailVerificationResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}