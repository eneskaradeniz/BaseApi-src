using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Authentication.Commands.SendPhoneNumberVerification;

public class SendPhoneNumberVerificationCommandValidator : AbstractValidator<SendPhoneNumberVerificationCommand>
{
    public SendPhoneNumberVerificationCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithError(ValidationErrors.Users.PhoneNumberIsRequired);
    }
}
