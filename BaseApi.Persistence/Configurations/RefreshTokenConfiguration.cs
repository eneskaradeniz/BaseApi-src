using BaseApi.Domain.Users;
using BaseApi.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(TableNames.RefreshTokens);

        builder.HasKey(rt => rt.Id);

        builder.HasIndex(rt => rt.Token);

        builder.Property(rt => rt.Id).HasConversion(
            refreshTokenId => refreshTokenId.Value,
            value => new RefreshTokenId(value));

        builder.Property(rt => rt.Token)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(rt => rt.CreatedOnUtc)
            .IsRequired();

        builder.Property(rt => rt.ExpiresOnUtc)
            .IsRequired();

        builder.Property(rt => rt.IsRevoked)
            .HasDefaultValue(false);

        builder.Property(rt => rt.RevokedOnUtc)
            .IsRequired(false);
    }
}
