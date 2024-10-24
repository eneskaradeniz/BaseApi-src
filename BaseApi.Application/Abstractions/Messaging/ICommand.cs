using MediatR;

namespace BaseApi.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;