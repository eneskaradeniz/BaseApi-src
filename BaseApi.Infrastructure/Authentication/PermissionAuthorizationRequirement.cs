using Microsoft.AspNetCore.Authorization;

namespace BaseApi.Infrastructure.Authentication;

public class PermissionAuthorizationRequirement(string permission) 
    : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}