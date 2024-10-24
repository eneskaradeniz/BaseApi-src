using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Roles;

public sealed class RoleId(Guid value) : StronglyTypedId<Guid>(value);
