using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Result;

namespace BaseApi.Application.Authentication.Commands.Logout;

internal sealed class LogoutCommandHandler(
    IUserBearerTokenProvider userTokenProvider,
    ITokenLifetimeManager tokenLifetimeManager) : ICommandHandler<LogoutCommand, Result>
{
    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await tokenLifetimeManager.LogoutAsync(userTokenProvider.BearerToken);

        return Result.Success();
    }
}
