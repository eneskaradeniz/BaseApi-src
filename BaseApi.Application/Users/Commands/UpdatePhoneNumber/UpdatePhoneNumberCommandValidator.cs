using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Users.Commands.UpdatePhoneNumber;

public class UpdatePhoneNumberCommandValidator : AbstractValidator<UpdatePhoneNumberCommand>
{
    public UpdatePhoneNumberCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithError(ValidationErrors.Users.PhoneNumberIsRequired);
    }
}
