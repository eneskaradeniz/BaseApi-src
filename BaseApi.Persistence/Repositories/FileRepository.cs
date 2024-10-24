using BaseApi.Application.Abstractions.Data;
using BaseApi.Domain.Files;

namespace BaseApi.Persistence.Repositories
{
    internal sealed class FileRepository(IApplicationDbContext dbContext)
        : GenericRepository<Domain.Files.File, FileId, Guid>(dbContext), IFileRepository;
}
