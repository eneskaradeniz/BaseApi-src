using BaseApi.Api.Contracts;
using BaseApi.Api.Infrastructure;
using BaseApi.Application.Files.Commands.RemoveFile;
using BaseApi.Application.Files.Commands.UploadFile;
using BaseApi.Application.Files.Queries.GetBaseStorageUrl;
using BaseApi.Application.Files.Queries.GetFiles;
using BaseApi.Contracts.Common;
using BaseApi.Contracts.Files;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Domain.Core.Primitives.Result;
using BaseApi.Domain.Enums;
using BaseApi.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Api.Controllers;

public class FilesController(IMediator mediator) : ApiController(mediator)
{
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Public.Files.GetBaseStorageUrl)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBaseStorageUrl() =>
        await Maybe<GetBaseStorageUrlCommand>
            .From(new GetBaseStorageUrlCommand())
            .Bind(command => Mediator.Send(command))
            .Match(Ok, NotFound);

    [HasPermission(Permission.GetFiles)]
    [HttpGet(ApiRoutes.Admin.Files.GetAll)]
    [ProducesResponseType(typeof(PagedList<AdminFileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(PaginationRequest paginationRequest) =>
        await Maybe<GetFilesQuery>
            .From(new GetFilesQuery(paginationRequest.Page, paginationRequest.PageSize))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);

    [HasPermission(Permission.UploadFile)]
    [HttpPost(ApiRoutes.Admin.Files.Upload)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload(UploadFileRequest uploadFileRequest) =>
        await Result.Create(uploadFileRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new UploadFileCommand(request.File, request.Path))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HasPermission(Permission.RemoveFile)]
    [HttpDelete(ApiRoutes.Admin.Files.Remove)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Remove(Guid fileId) =>
        await Result.Success(new RemoveFileCommand(fileId))
            .Bind(command => Mediator.Send(command))
            .Match(NoContent, BadRequest);
}