using BaseApi.Contracts.Roles;

namespace BaseApi.Contracts.Users;

public sealed class AdminUserWithRolesResponse
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public List<RoleResponse> Roles { get; set; }
}
