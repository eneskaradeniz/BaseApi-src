using BaseApi.Application.Abstractions.Data;
using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Common;
using BaseApi.Contracts.Files;
using BaseApi.Domain.Core.Primitives.Maybe;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Files.Queries.GetFiles;

internal sealed class GetFilesQueryHandler(IApplicationDbContext dbContext) :
    IQueryHandler<GetFilesQuery, Maybe<PagedList<AdminFileResponse>>>
{
    public async Task<Maybe<PagedList<AdminFileResponse>>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<AdminFileResponse> query = dbContext.Set<Domain.Files.File>()
            .AsNoTracking()
            .Select(x => new AdminFileResponse()
            {
                Id = x.Id.Value,
                Name = x.Name,
                Path = x.Path,
                ContentType = x.ContentType,
                Size = x.Size,
                StorageType = x.StorageType.ToString(),
                CreatedOnUtc = x.CreatedOnUtc,
            });

        var totalCount = await query.CountAsync(cancellationToken);

        AdminFileResponse[] responseArray = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync(cancellationToken);

        return new PagedList<AdminFileResponse>(responseArray, request.Page, request.PageSize, totalCount);
    }
}
