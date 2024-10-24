using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Cryptography;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Application.Abstractions.Notifications;
using BaseApi.Contracts.Emails;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.SendEmailVerification;

internal sealed class SendEmailVerificationCommandHandler(
    IUserRepository userRepository,
    IDateTime dateTime,
    IUnitOfWork unitOfWork,
    IVerificationCodeGenerator verificationCodeGenerator,
    IEmailNotificationService emailNotificationService) : ICommandHandler<SendEmailVerificationCommand, Result>
{

    public async Task<Result> Handle(SendEmailVerificationCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }

        Maybe<User> maybeUser = await userRepository.GetByEmailWithLastEmailVerificationCodeAsync(emailResult.Value, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }
        
        User user = maybeUser.Value;
        
        VerificationCode verificationCode = verificationCodeGenerator.Generate();

        Result emailVerificationResult = user.GenerateEmailVerificationCode(verificationCode, dateTime.UtcNow);

        if (emailVerificationResult.IsFailure)
        {
            return emailVerificationResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        EmailVerificationEmail emailVerificationEmail = new EmailVerificationEmail(user.Email, verificationCode);
        
        await emailNotificationService.SendEmailVerificationEmail(emailVerificationEmail);

        return Result.Success();
    }
}
