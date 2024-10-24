using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Users.Commands.AssignRole;

public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
{
    public AssignRoleCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithError(ValidationErrors.Roles.RoleIdIsRequired);

        RuleFor(x => x.UserId)
            .NotEmpty().WithError(ValidationErrors.Roles.UserIdIsRequired);
    }
}
