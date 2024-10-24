using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Users;

public sealed class RefreshTokenId(Guid value) : StronglyTypedId<Guid>(value);
