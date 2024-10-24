using BaseApi.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.Property(rp => rp.RoleId).HasConversion(
            roleId => roleId.Value,
            value => new RoleId(value));

        builder.Property(rp => rp.PermissionId).HasConversion(
            permissionId => permissionId.Value,
            value => new PermissionId(value));
    }
}