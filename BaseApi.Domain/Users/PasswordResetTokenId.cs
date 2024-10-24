using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Users;

public sealed class PasswordResetTokenId(Guid value) : StronglyTypedId<Guid>(value);

