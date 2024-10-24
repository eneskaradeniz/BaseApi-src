using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Users.Commands.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithError(ValidationErrors.Users.CurrentPasswordIsRequired);

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithError(ValidationErrors.Users.NewPasswordIsRequired);

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty().WithError(ValidationErrors.Users.ConfirmNewPasswordIsRequired)
            .Equal(x => x.NewPassword).WithError(ValidationErrors.Users.ConfirmNewPasswordIsDifferent);
    }
}
