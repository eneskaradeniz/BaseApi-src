using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Roles;

namespace BaseApi.Application.Roles.Commands.AssignPermission;

internal sealed class AssignPermissionCommandHandler(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AssignPermissionCommand, Result>
{
    public async Task<Result> Handle(AssignPermissionCommand request, CancellationToken cancellationToken)
    {
        Maybe<Role> maybeRole = await roleRepository.GetByIdWithRolePermissionsAsync(new RoleId(request.RoleId), cancellationToken);

        if (maybeRole.HasNoValue)
        {
            return Result.Failure(DomainErrors.Role.RoleNotFound);
        }

        Role role = maybeRole.Value;

        Result assignPermissionResult = role.AssignPermission(new PermissionId(request.PermissionId));

        if (assignPermissionResult.IsFailure)
        {
            return assignPermissionResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}