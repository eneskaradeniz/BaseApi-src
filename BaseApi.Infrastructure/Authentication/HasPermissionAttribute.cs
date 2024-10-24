using BaseApi.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BaseApi.Infrastructure.Authentication;

public sealed class HasPermissionAttribute(Permission permission) : 
    AuthorizeAttribute(policy: permission.ToString());