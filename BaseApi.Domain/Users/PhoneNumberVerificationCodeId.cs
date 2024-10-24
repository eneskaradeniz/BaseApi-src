using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Users;

public sealed class PhoneNumberVerificationCodeId(Guid value) : StronglyTypedId<Guid>(value);
