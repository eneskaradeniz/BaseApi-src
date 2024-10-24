using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Users;

namespace BaseApi.Application.Users.Commands.AssignRole;

internal sealed class AssignRoleCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AssignRoleCommand, Result>
{
    public async Task<Result> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        RoleId roleId = new(request.RoleId);

        bool roleExists = await roleRepository.AnyAsync(roleId, cancellationToken);

        if (!roleExists)
        {
            return Result.Failure(DomainErrors.Role.RoleNotFound);
        }

        UserId userId = new(request.UserId);

        Maybe<User> maybeUser = await userRepository.GetByIdWithUserRole(userId, roleId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.UserNotFound);
        }

        User user = maybeUser.Value;

        user.AssignRole(roleId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
