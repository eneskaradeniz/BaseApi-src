using BaseApi.Domain.Core.Primitives;

namespace BaseApi.Domain.Files;

public sealed class FileId(Guid value) : StronglyTypedId<Guid>(value);

