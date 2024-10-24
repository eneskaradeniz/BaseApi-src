using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Users;

public sealed class UserId(Guid value) : StronglyTypedId<Guid>(value);
