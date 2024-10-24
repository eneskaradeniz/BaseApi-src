using BaseApi.Application.Abstractions.Cryptography;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Authentication.Commands.Register;

internal sealed class RegisterCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<RegisterCommand, Result>
{
    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        Result<Email> emailResult = Email.Create(request.Email);
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        Result<Password> passwordResult = Password.Create(request.Password);
        Result<Password> confirmPasswordResult = Password.Create(request.ConfirmPassword);

        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(
            firstNameResult,
            lastNameResult,
            emailResult,
            phoneNumberResult,
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

        if (!await userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure(DomainErrors.User.DuplicateEmail);
        }

        if (!await userRepository.IsPhoneNumberUniqueAsync(phoneNumberResult.Value, cancellationToken))
        {
            return Result.Failure(DomainErrors.User.DuplicatePhoneNumber);
        }

        var passwordHash = passwordHasher.HashPassword(passwordResult.Value);

        var user = User.Create(
            firstNameResult.Value,
            lastNameResult.Value,
            emailResult.Value,
            phoneNumberResult.Value,
            passwordHash);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
