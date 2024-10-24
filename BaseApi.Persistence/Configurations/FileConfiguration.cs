using BaseApi.Domain.Files;
using BaseApi.Domain.Users;
using BaseApi.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<Domain.Files.File>
{
    public void Configure(EntityTypeBuilder<Domain.Files.File> builder)
    {
        builder.ToTable(TableNames.Files);

        builder.HasKey(f => f.Id);
            
        builder.Property(f => f.Id).HasConversion(
            fileId => fileId.Value,
            value => new FileId(value));

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.Path)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.ContentType)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.Size)
            .IsRequired()
            .HasColumnType("bigint");

        builder.Property(f => f.StorageType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(f => f.CreatedOnUtc)
            .IsRequired();

        builder.Ignore(f => f.ModifiedOnUtc);
    }
}