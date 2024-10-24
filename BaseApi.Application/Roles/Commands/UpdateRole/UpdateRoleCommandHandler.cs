using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Roles.Commands.UpdateRole;

internal sealed class UpdateRoleCommandHandler(
    IRoleRepository roleRepository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRoleCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Name);

        if (nameResult.IsFailure)
        {
            return Result.Failure(nameResult.Error);
        }

        Maybe<Role> maybeRole = await roleRepository.GetByIdAsync(new RoleId(request.RoleId), cancellationToken);

        if (maybeRole.HasNoValue)
        {
            return Result.Failure(DomainErrors.Role.RoleNotFound);
        }

        Role role = maybeRole.Value;

        if (!await roleRepository.IsNameUniqueAsync(nameResult.Value, cancellationToken))
        {
            return Result.Failure(DomainErrors.Role.DuplicateName);
        }

        role.Update(nameResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}