using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<Maybe<AdminUserResponse>>;