using BaseApi.Application.Abstractions.Common;

namespace BaseApi.Infrastructure.Common;

internal class MachineDateTime : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}