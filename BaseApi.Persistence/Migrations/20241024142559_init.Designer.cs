﻿// <auto-generated />
using System;
using BaseApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BaseApi.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241024142559_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BaseApi.Domain.Files.File", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<string>("StorageType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Files", (string)null);
                });

            modelBuilder.Entity("BaseApi.Domain.Roles.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        },
                        new
                        {
                            Id = 4
                        },
                        new
                        {
                            Id = 5
                        },
                        new
                        {
                            Id = 6
                        },
                        new
                        {
                            Id = 7
                        },
                        new
                        {
                            Id = 8
                        },
                        new
                        {
                            Id = 9
                        },
                        new
                        {
                            Id = 10
                        },
                        new
                        {
                            Id = 11
                        },
                        new
                        {
                            Id = 12
                        },
                        new
                        {
                            Id = 13
                        },
                        new
                        {
                            Id = 14
                        },
                        new
                        {
                            Id = 15
                        },
                        new
                        {
                            Id = 16
                        },
                        new
                        {
                            Id = 17
                        },
                        new
                        {
                            Id = 18
                        },
                        new
                        {
                            Id = 19
                        },
                        new
                        {
                            Id = 20
                        });
                });

            modelBuilder.Entity("BaseApi.Domain.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0e6e3e27-9dc5-4757-9dfa-4e95dd8b2a37"),
                            CreatedOnUtc = new DateTime(2024, 10, 24, 14, 25, 58, 850, DateTimeKind.Utc).AddTicks(3432)
                        });
                });

            modelBuilder.Entity("BaseApi.Domain.Roles.RolePermission", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("0e6e3e27-9dc5-4757-9dfa-4e95dd8b2a37"),
                            PermissionId = 1
                        });
                });

            modelBuilder.Entity("BaseApi.Domain.Users.EmailVerificationCode", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUsed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EmailVerificationCodes", (string)null);
                });

            modelBuilder.Entity("BaseApi.Domain.Users.PasswordResetToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUsed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Token");

                    b.HasIndex("UserId");

                    b.ToTable("PasswordResetTokens", (string)null);
                });

            modelBuilder.Entity("BaseApi.Domain.Users.PhoneNumberVerificationCode", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUsed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PhoneNumberVerificationCodes", (string)null);
                });

            modelBuilder.Entity("BaseApi.Domain.Users.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("RevokedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Token");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("BaseApi.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PhoneNumberVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("_passwordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("385b8860-9940-4ac6-88d7-7e989f15a66a"),
                            CreatedOnUtc = new DateTime(2024, 10, 24, 14, 25, 58, 851, DateTimeKind.Utc).AddTicks(7271),
                            EmailVerified = true,
                            PhoneNumberVerified = true,
                            _passwordHash = "1EBB46B80291C4D92A0C688EA9A3A04AB5F0D4910A2D4BEEE6FE3A8006F7D275-9D60FDD94F5DEE8FD745EB6F4D1BE4A3"
                        });
                });

            modelBuilder.Entity("BaseApi.Domain.Users.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("385b8860-9940-4ac6-88d7-7e989f15a66a"),
                            RoleId = new Guid("0e6e3e27-9dc5-4757-9dfa-4e95dd8b2a37")
                        });
                });

            modelBuilder.Entity("BaseApi.Domain.Roles.Permission", b =>
                {
                    b.OwnsOne("BaseApi.Domain.Roles.Name", "Name", b1 =>
                        {
                            b1.Property<int>("PermissionId")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Name");

                            b1.HasKey("PermissionId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Permissions");

                            b1.WithOwner()
                                .HasForeignKey("PermissionId");

                            b1.HasData(
                                new
                                {
                                    PermissionId = 1,
                                    Value = "All"
                                },
                                new
                                {
                                    PermissionId = 2,
                                    Value = "GetUsers"
                                },
                                new
                                {
                                    PermissionId = 3,
                                    Value = "GetUserById"
                                },
                                new
                                {
                                    PermissionId = 4,
                                    Value = "UpdateUser"
                                },
                                new
                                {
                                    PermissionId = 5,
                                    Value = "RemoveUser"
                                },
                                new
                                {
                                    PermissionId = 6,
                                    Value = "AssignRole"
                                },
                                new
                                {
                                    PermissionId = 7,
                                    Value = "GetUsersWithRoles"
                                },
                                new
                                {
                                    PermissionId = 8,
                                    Value = "GetUserByIdWithRoles"
                                },
                                new
                                {
                                    PermissionId = 9,
                                    Value = "GetRoles"
                                },
                                new
                                {
                                    PermissionId = 10,
                                    Value = "GetRoleById"
                                },
                                new
                                {
                                    PermissionId = 11,
                                    Value = "GetRolesWithPermissions"
                                },
                                new
                                {
                                    PermissionId = 12,
                                    Value = "GetRoleByIdWithPermissions"
                                },
                                new
                                {
                                    PermissionId = 13,
                                    Value = "CreateRole"
                                },
                                new
                                {
                                    PermissionId = 14,
                                    Value = "UpdateRole"
                                },
                                new
                                {
                                    PermissionId = 15,
                                    Value = "RemoveRole"
                                },
                                new
                                {
                                    PermissionId = 16,
                                    Value = "AssignPermission"
                                },
                                new
                                {
                                    PermissionId = 17,
                                    Value = "GetPermissions"
                                },
                                new
                                {
                                    PermissionId = 18,
                                    Value = "GetFiles"
                                },
                                new
                                {
                                    PermissionId = 19,
                                    Value = "UploadFile"
                                },
                                new
                                {
                                    PermissionId = 20,
                                    Value = "RemoveFile"
                                });
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("BaseApi.Domain.Roles.Role", b =>
                {
                    b.OwnsOne("BaseApi.Domain.Roles.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Name");

                            b1.HasKey("RoleId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Roles");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");

                            b1.HasData(
                                new
                                {
                                    RoleId = new Guid("0e6e3e27-9dc5-4757-9dfa-4e95dd8b2a37"),
                                    Value = "Administrator"
                                });
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("BaseApi.Domain.Roles.RolePermission", b =>
                {
                    b.HasOne("BaseApi.Domain.Roles.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseApi.Domain.Roles.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BaseApi.Domain.Users.EmailVerificationCode", b =>
                {
                    b.HasOne("BaseApi.Domain.Users.User", null)
                        .WithMany("EmailVerificationCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("BaseApi.Domain.Users.VerificationCode", "VerificationCode", b1 =>
                        {
                            b1.Property<Guid>("EmailVerificationCodeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("nchar(6)")
                                .HasColumnName("VerificationCode")
                                .IsFixedLength();

                            b1.HasKey("EmailVerificationCodeId");

                            b1.ToTable("EmailVerificationCodes");

                            b1.WithOwner()
                                .HasForeignKey("EmailVerificationCodeId");
                        });

                    b.Navigation("VerificationCode")
                        .IsRequired();
                });

            modelBuilder.Entity("BaseApi.Domain.Users.PasswordResetToken", b =>
                {
                    b.HasOne("BaseApi.Domain.Users.User", null)
                        .WithMany("PasswordResetTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BaseApi.Domain.Users.PhoneNumberVerificationCode", b =>
                {
                    b.HasOne("BaseApi.Domain.Users.User", null)
                        .WithMany("PhoneNumberVerificationCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("BaseApi.Domain.Users.VerificationCode", "VerificationCode", b1 =>
                        {
                            b1.Property<Guid>("PhoneNumberVerificationCodeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("nchar(6)")
                                .HasColumnName("VerificationCode")
                                .IsFixedLength();

                            b1.HasKey("PhoneNumberVerificationCodeId");

                            b1.ToTable("PhoneNumberVerificationCodes");

                            b1.WithOwner()
                                .HasForeignKey("PhoneNumberVerificationCodeId");
                        });

                    b.Navigation("VerificationCode")
                        .IsRequired();
                });

            modelBuilder.Entity("BaseApi.Domain.Users.RefreshToken", b =>
                {
                    b.HasOne("BaseApi.Domain.Users.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BaseApi.Domain.Users.User", b =>
                {
                    b.OwnsOne("BaseApi.Domain.Users.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.HasData(
                                new
                                {
                                    UserId = new Guid("385b8860-9940-4ac6-88d7-7e989f15a66a"),
                                    Value = "admin@baseapi.com"
                                });
                        });

                    b.OwnsOne("BaseApi.Domain.Users.FirstName", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.HasData(
                                new
                                {
                                    UserId = new Guid("385b8860-9940-4ac6-88d7-7e989f15a66a"),
                                    Value = "Admin"
                                });
                        });

                    b.OwnsOne("BaseApi.Domain.Users.LastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.HasData(
                                new
                                {
                                    UserId = new Guid("385b8860-9940-4ac6-88d7-7e989f15a66a"),
                                    Value = "User"
                                });
                        });

                    b.OwnsOne("BaseApi.Domain.Users.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("UserId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.HasData(
                                new
                                {
                                    UserId = new Guid("385b8860-9940-4ac6-88d7-7e989f15a66a"),
                                    Value = "+1000000000"
                                });
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("BaseApi.Domain.Users.UserRole", b =>
                {
                    b.HasOne("BaseApi.Domain.Roles.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseApi.Domain.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BaseApi.Domain.Roles.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("BaseApi.Domain.Users.User", b =>
                {
                    b.Navigation("EmailVerificationCodes");

                    b.Navigation("PasswordResetTokens");

                    b.Navigation("PhoneNumberVerificationCodes");

                    b.Navigation("RefreshTokens");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}