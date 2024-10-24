using BaseApi.Application.Abstractions.Common;
using BaseApi.Domain.Core.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BaseApi.Application.Core.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger,
    IDateTime dateTime)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            dateTime.UtcNow);

        var result = await next();

        if (result.IsFailure)
        {
            logger.LogError(
                "Failed request {@RequestName}, {@Error}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.Error,
                dateTime.UtcNow);
        }

        logger.LogInformation(
            "Completed request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            dateTime.UtcNow);

        return result;
    }
}