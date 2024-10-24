using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Core.Exceptions;

public class DomainException : Exception
{
    public DomainException(Error error)
        : base(error.Message)
        => Error = error;

    public Error Error { get; }
}