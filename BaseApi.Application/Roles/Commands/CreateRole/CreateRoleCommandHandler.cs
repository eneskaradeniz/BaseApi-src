using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Roles.Commands.CreateRole;

internal sealed class CreateRoleCommandHandler(
    IRoleRepository roleRepository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRoleCommand, Result>
{
    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return Result.Failure(nameResult.Error);
        }

        if (!await roleRepository.IsNameUniqueAsync(nameResult.Value, cancellationToken))
        {
            return Result.Failure(DomainErrors.Role.DuplicateName);
        }

        Role role = Role.Create(nameResult.Value);

        roleRepository.Insert(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}