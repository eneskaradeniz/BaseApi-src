using BaseApi.Api.Contracts;
using BaseApi.Domain.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Api.Infrastructure;

[Authorize]
[Route("api")]
public class ApiController : ControllerBase
{
    protected ApiController(IMediator mediator) => Mediator = mediator;

    protected IMediator Mediator { get; }

    protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse([error]));

    protected new IActionResult Ok(object value) => base.Ok(value);

    protected IActionResult Ok(Guid value) => base.Ok(value);

    protected new IActionResult NotFound() => base.NotFound();
}