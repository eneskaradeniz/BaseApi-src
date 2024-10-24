using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Users.Commands.UpdateMe;

internal sealed class UpdateMeCommandHandler(
    IUserIdentifierProvider userIdentifierProvider,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    : ICommandHandler<UpdateMeCommand, Result>
{
    public async Task<Result> Handle(UpdateMeCommand request, CancellationToken cancellationToken)
    {
        Result<FirstName> firstNameRequest = FirstName.Create(request.FirstName);
        Result<LastName> lastNameRequest = LastName.Create(request.LastName);

        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameRequest, lastNameRequest);

        if (firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure(firstFailureOrSuccess.Error);
        }

        Maybe<User> maybeUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        user.ChangeName(firstNameRequest.Value, lastNameRequest.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}