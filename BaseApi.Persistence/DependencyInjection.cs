using BaseApi.Application.Abstractions.Data;
using BaseApi.Domain.Files;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Users;
using BaseApi.Persistence.Infrastructure;
using BaseApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseApi.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey)!;

        string dbPassword = Environment.GetEnvironmentVariable(DbPassword.SettingsKey)!;

        connectionString = string.Format(connectionString, dbPassword);

        services.AddSingleton(new ConnectionString(connectionString));

        services.AddSingleton(new DbPassword(dbPassword));

        services.AddDbContext<ApplicationDbContext>
            (options => options.UseSqlServer(connectionString));

        services.AddScoped<IApplicationDbContext>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IApplicationSeedData, ApplicationDbSeedData>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IFileRepository, FileRepository>();

        return services;
    }
}