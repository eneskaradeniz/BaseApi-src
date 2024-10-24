using BaseApi.Domain.Users;
using BaseApi.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;
internal class EmailVerificationCodeConfiguration : IEntityTypeConfiguration<EmailVerificationCode>
{
    public void Configure(EntityTypeBuilder<EmailVerificationCode> builder)
    {
        builder.ToTable(TableNames.EmailVerificationCodes);

        builder.HasKey(evc => evc.Id);

        builder.Property(evc => evc.Id).HasConversion(
            emailVerificationCodeId => emailVerificationCodeId.Value,
            value => new EmailVerificationCodeId(value));

        builder.OwnsOne(u => u.VerificationCode, verificationCodeBuilder =>
        {
            verificationCodeBuilder.WithOwner();

            verificationCodeBuilder.Property(f => f.Value)
                .HasColumnName(nameof(EmailVerificationCode.VerificationCode))
                .HasMaxLength(VerificationCode.Length) 
                .IsFixedLength()
                .IsRequired();
        });

        builder.Property(evc => evc.CreatedOnUtc)
            .IsRequired();

        builder.Property(evc => evc.ExpiresOnUtc)
            .IsRequired();

        builder.Property(evc => evc.IsUsed)
            .HasDefaultValue(false);
    }
}
