using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Roles.Commands.AssignPermission;

public class AssignPermissionCommandValidator : AbstractValidator<AssignPermissionCommand>
{
    public AssignPermissionCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithError(ValidationErrors.Roles.RoleIdIsRequired);

        RuleFor(x => x.PermissionId)
            .NotEmpty().WithError(ValidationErrors.Roles.PermissionIdIsRequired);
    }
}