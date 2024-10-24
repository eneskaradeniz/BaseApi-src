using BaseApi.Domain.Users;
using BaseApi.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
            userId => userId.Value,
            value => new UserId(value));

        builder.OwnsOne(u => u.FirstName, firstNameBuilder =>
        {
            firstNameBuilder.WithOwner();

            firstNameBuilder.Property(f => f.Value)
                .HasColumnName(nameof(User.FirstName))
                .HasMaxLength(FirstName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(u => u.LastName, lastNameBuilder =>
        {
            lastNameBuilder.WithOwner();

            lastNameBuilder.Property(l => l.Value)
                .HasColumnName(nameof(User.LastName))
                .HasMaxLength(LastName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(u => u.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(e => e.Value)
                .HasColumnName(nameof(User.Email))
                .HasMaxLength(Email.MaxLength)
                .IsRequired();

            emailBuilder.HasIndex(e => e.Value)
                .IsUnique();
        });

        builder.OwnsOne(u => u.PhoneNumber, phoneNumberBuilder =>
        {
            phoneNumberBuilder.WithOwner();

            phoneNumberBuilder.Property(pn => pn.Value)
                .HasColumnName(nameof(User.PhoneNumber))
                .HasMaxLength(PhoneNumber.MaxLength)
                .IsRequired();

            phoneNumberBuilder.HasIndex(pn => pn.Value)
                .IsUnique();
        });

        builder.Property<string>("_passwordHash")
            .HasField("_passwordHash")
            .HasColumnName("PasswordHash")
            .IsRequired();

        builder.Property(u => u.EmailVerified)
            .HasDefaultValue(false);

        builder.Property(u => u.PhoneNumberVerified)
            .HasDefaultValue(false);

        builder.Property(u => u.CreatedOnUtc)
            .IsRequired();

        builder.Property(u => u.ModifiedOnUtc);

        builder.Property(u => u.DeletedOnUtc);

        builder.Property(u => u.Deleted)
            .HasDefaultValue(false);

        builder.HasQueryFilter(u => !u.Deleted);

        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId);

        builder.HasMany(u => u.EmailVerificationCodes)
            .WithOne()
            .HasForeignKey(evc => evc.UserId);

        builder.HasMany(u => u.PhoneNumberVerificationCodes)
            .WithOne()
            .HasForeignKey(pvc => pvc.UserId);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne()
            .HasForeignKey(rt => rt.UserId);

        builder.HasMany(u => u.PasswordResetTokens)
            .WithOne()
            .HasForeignKey(prt => prt.UserId);
    }
}
