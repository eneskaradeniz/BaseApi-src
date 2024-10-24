using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.EmailOrPhoneNumber)
            .NotEmpty().WithError(ValidationErrors.Users.EmailOrPhoneNumberIsRequired);

        RuleFor(x => x.Password)
            .NotEmpty().WithError(ValidationErrors.Users.PasswordIsRequired);
    }
}
