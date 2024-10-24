using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Users;

public sealed class EmailVerificationCodeId(Guid value) : StronglyTypedId<Guid>(value);
