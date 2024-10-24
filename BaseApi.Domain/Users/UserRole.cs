using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Roles;

namespace BaseApi.Domain.Users;

public class UserRole : BaseEntity
{
    public UserId UserId { get; private set; }

    public User User { get; init; }

    public RoleId RoleId { get; private set; }

    public Role Role { get; init; }

    private UserRole()
    {
    }

    private UserRole(UserId userId, RoleId roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public static UserRole Create(UserId userId, RoleId roleId)
    {
        return new UserRole(userId, roleId);
    }
}