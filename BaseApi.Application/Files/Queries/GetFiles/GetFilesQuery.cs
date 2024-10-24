using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Common;
using BaseApi.Contracts.Files;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Files.Queries.GetFiles;

public sealed record GetFilesQuery(int Page, int PageSize) 
    : IQuery<Maybe<PagedList<AdminFileResponse>>>;