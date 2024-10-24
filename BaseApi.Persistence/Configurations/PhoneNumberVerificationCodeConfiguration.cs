using BaseApi.Domain.Users;
using BaseApi.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;
internal sealed class PhoneNumberVerificationCodeConfiguration : IEntityTypeConfiguration<PhoneNumberVerificationCode>
{
    public void Configure(EntityTypeBuilder<PhoneNumberVerificationCode> builder)
    {
        builder.ToTable(TableNames.PhoneNumberVerificationCodes);

        builder.HasKey(pvc => pvc.Id);

        builder.Property(pvc => pvc.Id).HasConversion(
            phoneNumberVerificationCodeId => phoneNumberVerificationCodeId.Value,
            value => new PhoneNumberVerificationCodeId(value));

        builder.OwnsOne(p => p.VerificationCode, verificationCodeBuilder =>
        {
            verificationCodeBuilder.WithOwner();

            verificationCodeBuilder.Property(f => f.Value)
                .HasColumnName(nameof(PhoneNumberVerificationCode.VerificationCode))
                .HasMaxLength(VerificationCode.Length)
                .IsFixedLength()
                .IsRequired();
        });

        builder.Property(pvc => pvc.CreatedOnUtc)
            .IsRequired();

        builder.Property(pvc => pvc.ExpiresOnUtc)
            .IsRequired();

        builder.Property(pvc => pvc.IsUsed)
            .HasDefaultValue(false);
    }
}
