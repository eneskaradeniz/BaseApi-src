using BaseApi.Domain.Core.Events;
using MediatR;

namespace BaseApi.Application.Abstractions.Messaging;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;