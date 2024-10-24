using BaseApi.Api.Contracts;
using BaseApi.Api.Infrastructure;
using BaseApi.Application.Permissions.Queries.GetPermissions;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Enums;
using BaseApi.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Api.Controllers;

public class PermissionsController(IMediator mediator) : ApiController(mediator)
{
    [HasPermission(Permission.GetPermissions)]
    [HttpGet(ApiRoutes.Admin.Permissions.GetAll)]
    [ProducesResponseType(typeof(List<PermissionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get() =>
        await Maybe<GetPermissionsQuery>
            .From(new GetPermissionsQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);
}
