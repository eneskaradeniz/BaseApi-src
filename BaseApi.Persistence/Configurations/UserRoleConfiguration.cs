using BaseApi.Domain.Roles;
using BaseApi.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Property(ur => ur.UserId).HasConversion(
            userId => userId.Value,
            value => new UserId(value));
            
        builder.Property(ur => ur.RoleId).HasConversion(
            roleId => roleId.Value,
            value => new RoleId(value));

        builder.HasQueryFilter(ur => !ur.User.Deleted);
    }  
}