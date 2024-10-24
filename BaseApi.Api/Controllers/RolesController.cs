using BaseApi.Api.Contracts;
using BaseApi.Api.Infrastructure;
using BaseApi.Application.Roles.Commands.AssignPermission;
using BaseApi.Application.Roles.Commands.CreateRole;
using BaseApi.Application.Roles.Commands.RemoveRole;
using BaseApi.Application.Roles.Commands.UpdateRole;
using BaseApi.Application.Roles.Queries.GetRoleById;
using BaseApi.Application.Roles.Queries.GetRoleByIdWithPermissions;
using BaseApi.Application.Roles.Queries.GetRoles;
using BaseApi.Application.Roles.Queries.GetRolesWithPermissions;
using BaseApi.Contracts.Roles;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Enums;
using BaseApi.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Api.Controllers;

public class RolesController(IMediator mediator) : ApiController(mediator)
{
    [HasPermission(Permission.GetRoles)]
    [HttpGet(ApiRoutes.Admin.Roles.GetAll)]
    [ProducesResponseType(typeof(List<RoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get() =>
        await Maybe<GetRolesQuery>
            .From(new GetRolesQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HasPermission(Permission.GetRoleById)]
    [HttpGet(ApiRoutes.Admin.Roles.GetById)]
    [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid roleId) =>
        await Maybe<GetRoleByIdQuery>
            .From(new GetRoleByIdQuery(roleId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HasPermission(Permission.GetRolesWithPermissions)]
    [HttpGet(ApiRoutes.Admin.Roles.GetAllWithPermissions)]
    [ProducesResponseType(typeof(List<RoleWithPermissionsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithPermissions() =>
        await Maybe<GetRolesWithPermissionsQuery>
            .From(new GetRolesWithPermissionsQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HasPermission(Permission.GetRoleByIdWithPermissions)]
    [HttpGet(ApiRoutes.Admin.Roles.GetByIdWithPermissions)]
    [ProducesResponseType(typeof(RoleWithPermissionsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdWithPermissions(Guid roleId) =>
        await Maybe<GetRoleByIdWithPermissionsQuery>
            .From(new GetRoleByIdWithPermissionsQuery(roleId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HasPermission(Permission.CreateRole)]
    [HttpPost(ApiRoutes.Admin.Roles.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest createRoleRequest) =>
        await Result.Create(createRoleRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new CreateRoleCommand(request.Name))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HasPermission(Permission.UpdateRole)]
    [HttpPut(ApiRoutes.Admin.Roles.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid roleId, [FromBody] UpdateRoleRequest updateRoleRequest) =>
        await Result.Create(updateRoleRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateRoleCommand(roleId, request.Name))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HasPermission(Permission.RemoveRole)]
    [HttpDelete(ApiRoutes.Admin.Roles.Remove)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Remove(Guid roleId) =>
        await Result.Success(new RemoveRoleCommand(roleId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HasPermission(Permission.AssignPermission)]
    [HttpPut(ApiRoutes.Admin.Roles.AssignPermission)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignPermission(Guid roleId, int permissionId) =>
        await Result.Success(new AssignPermissionCommand(roleId, permissionId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}