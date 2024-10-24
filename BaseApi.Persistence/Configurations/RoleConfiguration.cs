using BaseApi.Domain.Roles;
using BaseApi.Domain.Users;
using BaseApi.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasConversion(
                roleId => roleId.Value,
                value => new RoleId(value));

        builder.OwnsOne(r => r.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Role.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();

            nameBuilder.HasIndex(n => n.Value)
                .IsUnique();
        });

        builder.Property(r => r.CreatedOnUtc)
            .IsRequired();

        builder.Property(r => r.ModifiedOnUtc)
            .IsRequired(false);

        builder.HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId);
        
        builder.HasMany<UserRole>()
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId);
    }
}