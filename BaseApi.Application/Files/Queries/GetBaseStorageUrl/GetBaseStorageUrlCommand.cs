using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Files.Queries.GetBaseStorageUrl;

public sealed record GetBaseStorageUrlCommand : IQuery<Maybe<string>>;
