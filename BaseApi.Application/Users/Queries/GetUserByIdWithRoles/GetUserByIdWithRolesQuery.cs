using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Users.Queries.GetUserByIdWithRoles;

public sealed record GetUserByIdWithRolesQuery(Guid UserId)
    : IQuery<Maybe<AdminUserWithRolesResponse>>;
