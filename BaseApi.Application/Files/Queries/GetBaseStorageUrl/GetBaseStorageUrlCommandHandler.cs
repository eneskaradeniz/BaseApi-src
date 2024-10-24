using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Application.Infrastructure;
using BaseApi.Domain.Core.Primitives.Maybe;
using Microsoft.Extensions.Options;

namespace BaseApi.Application.Files.Queries.GetBaseStorageUrl;

internal sealed class GetBaseStorageUrlCommandHandler(IOptions<BaseUrlsSettings> baseUrlsSettings) : IQueryHandler<GetBaseStorageUrlCommand, Maybe<string>>
{
    private readonly BaseUrlsSettings _baseUrlsSettings = baseUrlsSettings.Value;

    public Task<Maybe<string>> Handle(GetBaseStorageUrlCommand request, CancellationToken cancellationToken)
    {
        Maybe<string> baseStorageUrl = _baseUrlsSettings.Storage;

        return Task.FromResult(baseStorageUrl);
    }
}
