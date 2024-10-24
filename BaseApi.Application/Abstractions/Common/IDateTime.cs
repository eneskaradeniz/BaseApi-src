namespace BaseApi.Application.Abstractions.Common;

public interface IDateTime
{
    DateTime UtcNow { get; }
}