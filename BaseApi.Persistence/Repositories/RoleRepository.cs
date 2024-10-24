using BaseApi.Application.Abstractions.Data;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Persistence.Specifications;

namespace BaseApi.Persistence.Repositories
{
    internal sealed class RoleRepository(IApplicationDbContext dbContext)
        : GenericRepository<Role, RoleId, Guid>(dbContext), IRoleRepository
    {
        public async Task<bool> IsNameUniqueAsync(Name name, CancellationToken cancellationToken = default) => 
            !await AnyAsync(new RoleWithNameSpecification(name), cancellationToken);
        
        public Task<Maybe<Role>> GetByIdWithRolePermissionsAsync(RoleId id, CancellationToken cancellationToken = default) => 
            FirstOrDefaultAsync(new RoleWithRolePermissionsSpecification(id), cancellationToken);
    }
}
