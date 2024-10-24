using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Authentication.Commands.EmailVerification;

public class EmailVerificationCommandValidator : AbstractValidator<EmailVerificationCommand>
{
    public EmailVerificationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithError(ValidationErrors.Users.EmailIsRequired);

        RuleFor(x => x.VerificationCode)
            .NotEmpty().WithError(ValidationErrors.Users.VerificationCodeIsRequired);
    }
}
