using BaseApi.Api.Contracts;
using BaseApi.Api.Infrastructure;
using BaseApi.Application.Authentication.Commands.EmailVerification;
using BaseApi.Application.Authentication.Commands.ForgotPassword;
using BaseApi.Application.Authentication.Commands.Login;
using BaseApi.Application.Authentication.Commands.Logout;
using BaseApi.Application.Authentication.Commands.PhoneNumberVerification;
using BaseApi.Application.Authentication.Commands.RefreshToken;
using BaseApi.Application.Authentication.Commands.Register;
using BaseApi.Application.Authentication.Commands.ResetPassword;
using BaseApi.Application.Authentication.Commands.SendEmailVerification;
using BaseApi.Application.Authentication.Commands.SendPhoneNumberVerification;
using BaseApi.Contracts.Authentication;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Errors;
using BaseApi.Domain.Core.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Api.Controllers;

public class AuthenticationController(IMediator mediator) : ApiController(mediator)
{
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.Login)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) =>
            await Result.Create(loginRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new LoginCommand(request.EmailOrPhoneNumber, request.Password))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.Register)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest) =>
        await Result.Create(registerRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                request.Password,
                request.ConfirmPassword))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.RefreshToken)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromQuery] string refreshToken) =>
        await Result.Create(refreshToken, DomainErrors.General.UnProcessableRequest)
            .Map(request => new RefreshTokenCommand(request))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.ForgotPassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email) =>
        await Result.Create(email, DomainErrors.General.UnProcessableRequest)
            .Map(request => new ForgotPasswordCommand(request))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.ResetPassword)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] ResetPasswordRequest resetPasswordRequest) =>
        await Result.Create(resetPasswordRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new ResetPasswordCommand(token, request.Email, request.Password, request.ConfirmPassword))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.EmailVerification)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EmailVerification([FromBody] EmailVerificationRequest emailVerificationRequest) =>
        await Result.Create(emailVerificationRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new EmailVerificationCommand(request.Email, request.VerificationCode))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.SendEmailVerification)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendEmailVerification([FromQuery] string email) =>
        await Result.Create(email, DomainErrors.General.UnProcessableRequest)
            .Map(request => new SendEmailVerificationCommand(request))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.PhoneNumberVerification)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PhoneNumberVerification([FromBody] PhoneNumberVerificationRequest phoneNumberVerificationRequest) =>
        await Result.Create(phoneNumberVerificationRequest, DomainErrors.General.UnProcessableRequest)
            .Map(request => new PhoneNumberVerificationCommand(request.PhoneNumber, request.VerificationCode))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Authentication.SendPhoneNumberVerification)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendPhoneNumberVerification([FromQuery] string phoneNumber) =>
        await Result.Create(phoneNumber, DomainErrors.General.UnProcessableRequest)
            .Map(request => new SendPhoneNumberVerificationCommand(request))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.Identity.Authentication.Logout)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout() =>
        await Mediator.Send(new LogoutCommand())
            .Match(Ok, BadRequest);
}