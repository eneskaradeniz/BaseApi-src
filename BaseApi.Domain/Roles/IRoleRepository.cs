using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Domain.Roles;

public interface IRoleRepository
{
    Task<bool> IsNameUniqueAsync(Name name, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(RoleId id, CancellationToken cancellationToken = default);

    Task<Maybe<Role>> GetByIdAsync(RoleId id, CancellationToken cancellationToken = default);
    
    Task<Maybe<Role>> GetByIdWithRolePermissionsAsync(RoleId id, CancellationToken cancellationToken = default);

    void Insert(Role role);

    void Update(Role role);

    void Remove(Role role);
}