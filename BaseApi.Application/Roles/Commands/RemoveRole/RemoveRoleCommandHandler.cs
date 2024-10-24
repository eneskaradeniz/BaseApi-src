using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Roles.Commands.RemoveRole;

internal sealed class RemoveRoleCommandHandler(
    IRoleRepository roleRepository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveRoleCommand, Result>
{
    public async Task<Result> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        Maybe<Role> maybeRole = await roleRepository.GetByIdAsync(new RoleId(request.RoleId), cancellationToken);

        if (maybeRole.HasNoValue)
        {
            return Result.Failure(DomainErrors.Role.RoleNotFound);
        }

        Role role = maybeRole.Value;
        
        roleRepository.Remove(role);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}