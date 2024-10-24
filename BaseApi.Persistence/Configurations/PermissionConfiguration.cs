using BaseApi.Domain.Roles;
using BaseApi.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            permissionId => permissionId.Value,
            value => new PermissionId(value));

        builder.OwnsOne(p => p.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Permission.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();

            nameBuilder.HasIndex(n => n.Value)
                .IsUnique();
        });

        builder.HasMany<RolePermission>()
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId);

        // Seed data

        IEnumerable<Permission> permissions = Enum
            .GetValues<Domain.Enums.Permission>()
            .Select(p => Permission.Create(
                new PermissionId((int)p),
                Name.Create(p.ToString()).Value));

        builder.HasData(permissions.Select(p => new { p.Id }));

        builder.OwnsOne(p => p.Name).HasData(permissions.Select(p => new
        {
            PermissionId = p.Id,
            Value = p.Name.Value
        }));
    }
}