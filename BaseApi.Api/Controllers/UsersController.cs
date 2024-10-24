using BaseApi.Api.Contracts;
using BaseApi.Api.Infrastructure;
using BaseApi.Application.Users.Commands.AssignRole;
using BaseApi.Application.Users.Commands.DeleteAccount;
using BaseApi.Application.Users.Commands.RemoveUser;
using BaseApi.Application.Users.Commands.UpdateEmail;
using BaseApi.Application.Users.Commands.UpdateMe;
using BaseApi.Application.Users.Commands.UpdatePassword;
using BaseApi.Application.Users.Commands.UpdatePhoneNumber;
using BaseApi.Application.Users.Commands.UpdateUser;
using BaseApi.Application.Users.Queries.GetMe;
using BaseApi.Application.Users.Queries.GetUserById;
using BaseApi.Application.Users.Queries.GetUserByIdWithRoles;
using BaseApi.Application.Users.Queries.GetUsers;
using BaseApi.Application.Users.Queries.GetUsersWithRoles;
using BaseApi.Contracts.Common;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Enums;
using BaseApi.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Api.Controllers;

public class UsersController(IMediator mediator) : ApiController(mediator)
{
    [HasPermission(Permission.GetUserById)]
    [HttpGet(ApiRoutes.Admin.Users.GetById)]
    [ProducesResponseType(typeof(AdminUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid userId) =>
        await Maybe<GetUserByIdQuery>
            .From(new GetUserByIdQuery(userId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HasPermission(Permission.GetUsers)]
    [HttpGet(ApiRoutes.Admin.Users.GetAll)]
    [ProducesResponseType(typeof(PagedList<AdminUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] PaginationRequest paginationRequest) =>
        await Maybe<GetUsersQuery>
            .From(new GetUsersQuery(paginationRequest.Page, paginationRequest.PageSize))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HasPermission(Permission.UpdateUser)]
    [HttpPut(ApiRoutes.Admin.Users.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid userId, [FromBody] UpdateUserRequest updateUserRequest) =>
        await Result.Create(updateUserRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateUserCommand(userId, request.FirstName, request.LastName))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HasPermission(Permission.RemoveUser)]
    [HttpDelete(ApiRoutes.Admin.Users.Remove)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Remove(Guid userId) =>
        await Result.Success(new RemoveUserCommand(userId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.Identity.Users.GetMe)]
    [ProducesResponseType(typeof(MeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMe() =>
        await Maybe<GetMeQuery>
            .From(new GetMeQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HttpPut(ApiRoutes.Identity.Users.UpdateMe)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateMeRequest updateMeRequest) =>
        await Result.Create(updateMeRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateMeCommand(request.FirstName, request.LastName))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpPatch(ApiRoutes.Identity.Users.UpdateEmail)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmail([FromQuery] string email) =>
        await Result.Create(email, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdateEmailCommand(request))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpPatch(ApiRoutes.Identity.Users.UpdatePhoneNumber)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePhoneNumber([FromQuery] string phoneNumber) =>
        await Result.Create(phoneNumber, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdatePhoneNumberCommand(request))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpPatch(ApiRoutes.Identity.Users.UpdatePassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest updatePasswordRequest) =>
        await Result.Create(updatePasswordRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UpdatePasswordCommand(
                request.CurrentPassword,
                request.NewPassword,
                request.ConfirmNewPassword))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpDelete(ApiRoutes.Identity.Users.DeleteAccount)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAccount() =>
        await Result.Success(new DeleteAccountCommand())
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HasPermission(Permission.AssignRole)]
    [HttpPut(ApiRoutes.Admin.Users.AssignRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignRole(Guid userId, Guid roleId) =>
        await Result.Success(new AssignRoleCommand(userId, roleId))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HasPermission(Permission.GetUserByIdWithRoles)]
    [HttpGet(ApiRoutes.Admin.Users.GetByIdWithRoles)]
    [ProducesResponseType(typeof(AdminUserWithRolesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdWithRoles(Guid userId) =>
        await Maybe<GetUserByIdWithRolesQuery>
            .From(new GetUserByIdWithRolesQuery(userId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HasPermission(Permission.GetUsersWithRoles)]
    [HttpGet(ApiRoutes.Admin.Users.GetAllWithRoles)]
    [ProducesResponseType(typeof(PagedList<AdminUserWithRolesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithRoles([FromQuery] PaginationRequest paginationRequest) =>
        await Maybe<GetUsersWithRolesQuery>
            .From(new GetUsersWithRolesQuery(paginationRequest.Page, paginationRequest.PageSize))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);
}