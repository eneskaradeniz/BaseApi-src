using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.PhoneNumberVerification;

internal sealed class PhoneNumberVerificationCommandHandler(
    IUserRepository userRepository,
    IDateTime dateTime,
    IUnitOfWork unitOfWork)
    : ICommandHandler<PhoneNumberVerificationCommand, Result>
{
    public async Task<Result> Handle(PhoneNumberVerificationCommand request, CancellationToken cancellationToken)
    {
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        Result<VerificationCode> verificationCodeResult = VerificationCode.Create(request.VerificationCode);

        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(phoneNumberResult, verificationCodeResult);

        if (firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure(firstFailureOrSuccess.Error);
        }

        Maybe<User> maybeUser = await userRepository.GetByPhoneNumberWithLastPhoneNumberVerificationCodeAsync(phoneNumberResult.Value, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        Result phoneNumberVerificationResult = user.VerifyPhoneNumber(verificationCodeResult.Value, dateTime.UtcNow);

        if (phoneNumberVerificationResult.IsFailure)
        {
            return phoneNumberVerificationResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}