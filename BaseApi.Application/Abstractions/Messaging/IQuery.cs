using MediatR;

namespace BaseApi.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;