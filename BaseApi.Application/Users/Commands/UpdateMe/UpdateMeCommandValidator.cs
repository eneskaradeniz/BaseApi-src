using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Users.Commands.UpdateMe;

public class UpdateMeCommandValidator : AbstractValidator<UpdateMeCommand>
{
    public UpdateMeCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithError(ValidationErrors.Users.FirstNameIsRequired);

        RuleFor(x => x.LastName)
            .NotEmpty().WithError(ValidationErrors.Users.LastNameIsRequired);
    }
}