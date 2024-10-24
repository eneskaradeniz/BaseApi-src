using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Cryptography;
using BaseApi.Application.Abstractions.Data;
using BaseApi.Domain.Roles;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence;

public class ApplicationDbSeedData(IPasswordHasher passwordHasher, IDateTime dateTime)
    : IApplicationSeedData
{
    public void Run(ModelBuilder builder)
    {
        RoleId adminRoleId = new RoleId(Guid.NewGuid());

        builder.Entity<Role>(r =>
        {
            r.HasData(new
            {
                Id = adminRoleId,
                CreatedOnUtc = dateTime.UtcNow
            });

            r.OwnsOne(role => role.Name)
                .HasData(new { RoleId = adminRoleId, Value = "Administrator" });
        });

        PermissionId allPermissionId = new PermissionId((int)Domain.Enums.Permission.All);

        builder.Entity<RolePermission>().HasData(new
        {
            RoleId = adminRoleId,
            PermissionId = allPermissionId
        });

        UserId adminUserId = new UserId(Guid.NewGuid());

        string passwordHash = passwordHasher.HashPassword
            (Password.Create("123456Admin!").Value);

        builder.Entity<User>(u =>
        {
            u.HasData(new
            {
                Id = adminUserId,
                EmailVerified = true,
                PhoneNumberVerified = true,
                _passwordHash = passwordHash,
                CreatedOnUtc = DateTime.UtcNow
            });

            u.OwnsOne(user => user.FirstName)
                .HasData(new { UserId = adminUserId, Value = "Admin" });

            u.OwnsOne(user => user.LastName)
                .HasData(new { UserId = adminUserId, Value = "User" });

            u.OwnsOne(user => user.Email)
                .HasData(new { UserId = adminUserId, Value = "admin@baseapi.com" });

            u.OwnsOne(user => user.PhoneNumber)
                .HasData(new { UserId = adminUserId, Value = "+1000000000" });
        });

        builder.Entity<UserRole>().HasData(new
        {
            UserId = adminUserId,
            RoleId = adminRoleId
        });
    }
}