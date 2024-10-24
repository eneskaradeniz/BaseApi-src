using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Abstractions.Data;

public interface IApplicationSeedData
{
    void Run(ModelBuilder builder);
}