using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Users.Commands.UpdateEmail;

internal sealed class UpdateEmailCommandHandler(
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateEmailCommand, Result>
{
    public async Task<Result> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }

        Maybe<User> maybeUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        if (!await userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure(DomainErrors.User.DuplicateEmail);
        }

        Result changeEmailResult = user.ChangeEmail(emailResult.Value);

        if (changeEmailResult.IsFailure)
        {
            return Result.Failure(changeEmailResult.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
