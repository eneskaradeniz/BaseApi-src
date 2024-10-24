using BaseApi.Application.Core.Errors;
using BaseApi.Application.Core.Extensions;
using FluentValidation;

namespace BaseApi.Application.Files.Commands.RemoveFile;

public class RemoveFileCommandValidator : AbstractValidator<RemoveFileCommand>
{
    public RemoveFileCommandValidator()
    {
        RuleFor(x => x.FileId)
            .NotEmpty().WithError(ValidationErrors.Files.FileIdIsRequired);
    }
}