using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Api.Contracts;

public class ApiErrorResponse(IReadOnlyCollection<Error> errors)
{
    public IReadOnlyCollection<Error> Errors { get; set; } = errors;
}