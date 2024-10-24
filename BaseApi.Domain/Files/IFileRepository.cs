using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Domain.Files;

public interface IFileRepository
{
    Task<Maybe<File>> GetByIdAsync(FileId id, CancellationToken cancellationToken = default);

    void Insert(File file);

    void Remove(File file);
}