using BaseApi.Application.Abstractions.Messaging;
using BaseApi.Contracts.Common;
using BaseApi.Contracts.Users;
using BaseApi.Domain.Core.Primitives.Maybe;

namespace BaseApi.Application.Users.Queries.GetUsers;

public sealed record GetUsersQuery(int Page, int PageSize) 
    : IQuery<Maybe<PagedList<AdminUserResponse>>>;