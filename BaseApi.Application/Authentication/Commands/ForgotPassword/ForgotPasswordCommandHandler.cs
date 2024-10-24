using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Application.Abstractions.Notifications;
using BaseApi.Contracts.Emails;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.ForgotPassword;

internal class ForgotPasswordCommandHandler(
    IUserRepository userRepository,
    ITokenProvider tokenProvider,
    IDateTime dateTime,
    IUnitOfWork unitOfWork,
    IEmailNotificationService emailNotificationService)
    : ICommandHandler<ForgotPasswordCommand, Result>
{
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }

        Maybe<User> maybeUser = await userRepository.GetByEmailWithLastPasswordResetTokenAsync(emailResult.Value, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        string token = tokenProvider.CreateBase64Token();

        Result passwordResetTokenResult = user.GeneratePasswordResetToken(token, dateTime.UtcNow);

        if (passwordResetTokenResult.IsFailure)
        {
            return Result.Failure(passwordResetTokenResult.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        PasswordResetEmail passwordResetEmail = new PasswordResetEmail(user.Email, token);

        await emailNotificationService.SendPasswordResetEmail(passwordResetEmail);

        return Result.Success();
    }
}
