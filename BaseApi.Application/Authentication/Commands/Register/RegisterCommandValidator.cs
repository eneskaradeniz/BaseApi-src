using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithError(ValidationErrors.Users.FirstNameIsRequired);

        RuleFor(x => x.LastName)
            .NotEmpty().WithError(ValidationErrors.Users.LastNameIsRequired);

        RuleFor(x => x.Email)
            .NotEmpty().WithError(ValidationErrors.Users.EmailIsRequired);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithError(ValidationErrors.Users.PhoneNumberIsRequired);

        RuleFor(x => x.Password)
            .NotEmpty().WithError(ValidationErrors.Users.PasswordIsRequired);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithError(ValidationErrors.Users.ConfirmPasswordIsRequired)
            .Equal(x => x.Password).WithError(ValidationErrors.Users.ConfirmPasswordIsDifferent);
    }
}
