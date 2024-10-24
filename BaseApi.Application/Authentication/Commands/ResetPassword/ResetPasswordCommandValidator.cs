using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithError(ValidationErrors.Users.TokenIsRequired);

        RuleFor(x => x.Password)
            .NotEmpty().WithError(ValidationErrors.Users.PasswordIsRequired);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithError(ValidationErrors.Users.ConfirmPasswordIsRequired)
            .Equal(x => x.Password).WithError(ValidationErrors.Users.ConfirmPasswordIsDifferent);
    }
}