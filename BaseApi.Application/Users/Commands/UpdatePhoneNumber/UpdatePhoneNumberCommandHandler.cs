﻿using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Users.Commands.UpdatePhoneNumber;

internal sealed class UpdatePhoneNumberCommandHandler(
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdatePhoneNumberCommand, Result>
{
    public async Task<Result> Handle(UpdatePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);

        if (phoneNumberResult.IsFailure)
        {
            return Result.Failure(phoneNumberResult.Error);
        }

        Maybe<User> maybeUser = await userRepository.GetByIdAsync(userIdentifierProvider.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        if (!await userRepository.IsPhoneNumberUniqueAsync(phoneNumberResult.Value, cancellationToken))
        {
            return Result.Failure(DomainErrors.User.DuplicatePhoneNumber);
        }

        Result changePhoneNumberResult = user.ChangePhoneNumber(phoneNumberResult.Value);

        if (changePhoneNumberResult.IsFailure)
        {
            return Result.Failure(changePhoneNumberResult.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
