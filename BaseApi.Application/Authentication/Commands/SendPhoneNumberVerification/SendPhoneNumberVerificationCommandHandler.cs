using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Cryptography;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Application.Abstractions.Notifications;
using BaseApi.Contracts.Sms;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.SendPhoneNumberVerification;

internal sealed class SendPhoneNumberVerificationCommandHandler(
    IUserRepository userRepository,
    IDateTime dateTime,
    IUnitOfWork unitOfWork,
    IVerificationCodeGenerator verificationCodeGenerator,
    ISmsNotificationService smsNotificationService)
    : ICommandHandler<SendPhoneNumberVerificationCommand, Result>
{
    public async Task<Result> Handle(SendPhoneNumberVerificationCommand request, CancellationToken cancellationToken)
    {
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);

        if (phoneNumberResult.IsFailure)
        {
            return Result.Failure(phoneNumberResult.Error);
        }

        Maybe<User> maybeUser = await userRepository.GetByPhoneNumberWithLastPhoneNumberVerificationCodeAsync(phoneNumberResult.Value, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;
        
        VerificationCode verificationCode = verificationCodeGenerator.Generate();

        Result phoneNumberVerificationResult = user.GeneratePhoneNumberVerificationCode(
            verificationCode, 
            dateTime.UtcNow);

        if (phoneNumberVerificationResult.IsFailure)
        {
            return phoneNumberVerificationResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        PhoneNumberVerificationSms phoneNumberVerificationSms = new PhoneNumberVerificationSms(user.PhoneNumber, verificationCode);

        await smsNotificationService.SendPhoneNumberVerificationSms(phoneNumberVerificationSms);

        return Result.Success();
    }
}