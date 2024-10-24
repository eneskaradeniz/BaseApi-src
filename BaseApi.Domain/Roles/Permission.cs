using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Roles;

public class Permission : Entity<PermissionId, int>
{
    public Name Name { get; private set; }

    private Permission()
    {
    }

    private Permission(PermissionId id, Name name) : base(id)
    {
        Name = name;
    }
    
    public static Permission Create(PermissionId id, Name name)
    {
        return new Permission(id, name);
    }
}