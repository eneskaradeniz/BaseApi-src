using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Roles;

public sealed class PermissionId(int value) : StronglyTypedId<int>(value);

