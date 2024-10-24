using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithError(ValidationErrors.Users.UserIdIsRequired);

        RuleFor(x => x.FirstName)
            .NotEmpty().WithError(ValidationErrors.Users.FirstNameIsRequired);

        RuleFor(x => x.LastName)
            .NotEmpty().WithError(ValidationErrors.Users.LastNameIsRequired);
    }
}