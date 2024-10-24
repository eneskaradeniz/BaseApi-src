using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Files.Commands.UploadFile;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithError(ValidationErrors.Files.FileIsRequired);

        RuleFor(x => x.Path)
            .NotEmpty().WithError(ValidationErrors.Files.PathIsRequired);
    }
}