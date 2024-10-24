using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Roles.Commands.RemoveRole;

public class RemoveRoleCommandValidator : AbstractValidator<RemoveRoleCommand>
{
    public RemoveRoleCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithError(ValidationErrors.Roles.RoleIdIsRequired);
    }
}