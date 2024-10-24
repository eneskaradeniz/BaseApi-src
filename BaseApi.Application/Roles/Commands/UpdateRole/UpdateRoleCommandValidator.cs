using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithError(ValidationErrors.Roles.RoleIdIsRequired);

        RuleFor(x => x.Name)
            .NotEmpty().WithError(ValidationErrors.Roles.NameIsRequired);
    }
}