using BaseApi.Domain.Users;
using BaseApi.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;

internal sealed class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable(TableNames.PasswordResetTokens);

        builder.HasKey(prs => prs.Id);

        builder.HasIndex(prs => prs.Token);

        builder.Property(prs => prs.Id).HasConversion(
            passwordResetTokenId => passwordResetTokenId.Value,
            value => new PasswordResetTokenId(value));

        builder.Property(prs => prs.Token)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(evc => evc.CreatedOnUtc)
            .IsRequired();

        builder.Property(evc => evc.ExpiresOnUtc)
            .IsRequired();

        builder.Property(evc => evc.IsUsed)
            .HasDefaultValue(false);

    }
}
