using BaseApi.Domain.Users;

namespace BaseApi.Application.Abstractions.Authentication;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(UserId userId, CancellationToken cancellationToken = default);
}