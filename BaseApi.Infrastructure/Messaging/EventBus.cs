using BaseApi.Application.Abstractions.EventBus;
using MassTransit;

namespace BaseApi.Infrastructure.Messaging;

internal sealed class EventBus(IPublishEndpoint publishEndpoint) : IEventBus
{
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class =>
        publishEndpoint.Publish(message, cancellationToken);
}