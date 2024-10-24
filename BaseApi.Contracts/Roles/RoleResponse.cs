namespace BaseApi.Contracts.Roles;

public sealed class RoleResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedOnUtc { get; set; }
}