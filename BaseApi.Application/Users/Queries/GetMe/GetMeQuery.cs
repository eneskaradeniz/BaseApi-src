using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Users.Queries.GetMe;

public sealed record GetMeQuery : IQuery<Maybe<MeResponse>>;