namespace BaseApi.Contracts.Roles;

public sealed class RoleWithPermissionsResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public List<PermissionResponse>? Permissions { get; set; }
}
