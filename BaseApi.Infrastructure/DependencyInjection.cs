using BaseApi.Application.Abstractions.Authentication;
using BaseApi.Application.Abstractions.Caching;
using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Cryptography;
using BaseApi.Application.Abstractions.Emails;
using BaseApi.Application.Abstractions.EventBus;
using BaseApi.Application.Abstractions.Notifications;
using BaseApi.Application.Abstractions.Sms;
using BaseApi.Application.Infrastructure;
using BaseApi.Domain.Enums;
using BaseApi.Domain.Users;
using BaseApi.Infrastructure.Authentication;
using BaseApi.Infrastructure.Authentication.Settings;
using BaseApi.Infrastructure.Caching;
using BaseApi.Infrastructure.Common;
using BaseApi.Infrastructure.Cryptography;
using BaseApi.Infrastructure.Emails;
using BaseApi.Infrastructure.Emails.Settings;
using BaseApi.Infrastructure.Extensions;
using BaseApi.Infrastructure.Infrastructure;
using BaseApi.Infrastructure.Messaging;
using BaseApi.Infrastructure.Messaging.Settings;
using BaseApi.Infrastructure.Notifications;
using BaseApi.Infrastructure.Sms;
using BaseApi.Infrastructure.Sms.Settings;
using BaseApi.Infrastructure.Storage.Settings;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BaseApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterSettings(services, configuration);

            string redisConnectionString = configuration.GetConnectionString(RedisConnectionString.SettingsKey)!;

            services.AddSingleton(new RedisConnectionString(redisConnectionString));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    ServiceProvider serviceProvider = services.BuildServiceProvider();

                    JwtSettings jwtSettings =
                        serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;

                    ITokenLifetimeManager tokenLifetimeManager =
                        serviceProvider.GetRequiredService<ITokenLifetimeManager>();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
                        LifetimeValidator = tokenLifetimeManager.ValidateTokenLifetime
                    };
                });

            services.AddAuthorization();

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            services.AddScoped<IPermissionService, PermissionService>();

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                // Add consumers

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    ServiceProvider serviceProvider = services.BuildServiceProvider();

                    MessageBrokerSettings messageBrokerSettings = serviceProvider.GetRequiredService<IOptions<MessageBrokerSettings>>().Value;

                    configurator.Host(new Uri(messageBrokerSettings.HostName), h =>
                    {
                        h.Username(messageBrokerSettings.UserName);
                        h.Password(messageBrokerSettings.Password);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

            services.AddScoped<IUserBearerTokenProvider, UserBearerTokenProvider>();

            services.AddSingleton<JwtSecurityTokenHandler>();

            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddTransient<ICacheService, CacheService>();

            services.AddSingleton<ITokenLifetimeManager, JwtTokenLifetimeManager>();

            services.AddScoped<IVerificationCodeGenerator, VerificationCodeGenerator>();

            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IPasswordHashChecker, PasswordHasher>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IEmailNotificationService, EmailNotificationService>();

            services.AddTransient<ISmsService, SmsService>();

            services.AddTransient<ISmsNotificationService, SmsNotificationService>();

            services.AddTransient<IEventBus, EventBus>();

            services.AddStorage(StorageType.S3, configuration);

            return services;
        }

        private static void RegisterSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection
                (JwtSettings.SettingsKey));

            services.Configure<AmazonS3Settings>(configuration.GetSection
                (AmazonS3Settings.SettingsKey));

            services.Configure<MessageBrokerSettings>(configuration.GetSection
                (MessageBrokerSettings.SettingsKey));

            services.Configure<MailSettings>(configuration.GetSection
                (MailSettings.SettingsKey));

            services.Configure<SmsSettings>(configuration.GetSection
                (SmsSettings.SettingsKey));

            services.Configure<BaseUrlsSettings>(configuration.GetSection
                (BaseUrlsSettings.SettingsKey));
        }
    }
}
