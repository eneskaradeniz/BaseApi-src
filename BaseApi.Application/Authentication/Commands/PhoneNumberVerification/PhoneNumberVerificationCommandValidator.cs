using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Authentication.Commands.PhoneNumberVerification;

public class PhoneNumberVerificationCommandValidator : AbstractValidator<PhoneNumberVerificationCommand>
{
    public PhoneNumberVerificationCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithError(ValidationErrors.Users.PhoneNumberIsRequired);

        RuleFor(x => x.VerificationCode)
            .NotEmpty().WithError(ValidationErrors.Users.VerificationCodeIsRequired);
    }
}
