namespace BaseApi.Domain.Core.Abstractions;

public interface ISoftDeletableEntity
{
    DateTime? DeletedOnUtc { get; }

    bool Deleted { get; }
}