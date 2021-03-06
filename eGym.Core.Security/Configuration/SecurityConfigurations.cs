﻿using eGym.Core.Security.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace eGym.Core.Security.Configuration
{
    public class SecurityUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(name: nameof(User), schema: "Security");
            builder.HasKey(prop => prop.Id);
            builder.Property(e => e.Id).HasColumnName("UserID");
            builder.Property(e => e.Culture).IsRequired(false);
            builder.Property(e => e.PasswordHash).HasColumnType("ntext");

            var auto = new User
            {
                Id = 1,
                Culture = "it-IT",
                Disabled = true,
                Email = "dev@digitalbubbles.cloud",
                NormalizedEmail = "dev@digitalbubbles.cloud".ToUpper(),
                EmailConfirmed = true,
                EmailConfirmedDateTime = System.DateTime.MinValue,
                UserName = "auto",
                NormalizedUserName = "auto".ToUpper(),
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PasswordExpirationDateTime = System.DateTime.Now.AddYears(100),
                SecurityStamp = System.Guid.NewGuid().ToString()
            };
            var autoPasswordHash = new PasswordHasher<User>().HashPassword(auto, System.Guid.NewGuid().ToString());
            auto.PasswordHash = autoPasswordHash;
            
            var admin = new User
            {
                Id = 2,
                Culture = "it-IT",
                Disabled = false,
                Email = "info@digitalbubbles.cloud",
                NormalizedEmail = "info@digitalbubbles.cloud".ToUpper(),
                EmailConfirmed = true,
                EmailConfirmedDateTime = System.DateTime.MinValue,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PasswordExpirationDateTime = System.DateTime.Now.AddYears(100),
                SecurityStamp = System.Guid.NewGuid().ToString()
            };
            var adminPasswordHash = new PasswordHasher<User>().HashPassword(admin, "dgtBu88l3$");
            admin.PasswordHash = adminPasswordHash;

            builder.HasData(auto, admin);
        }
    }

    public class SecurityRoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(name: nameof(Role), schema: "Security");
            builder.HasKey(prop => prop.Id);
            builder.Property(e => e.Id).HasColumnName("RoleID");
            builder.HasData(
                new Role() { Id = 1, Name = Const_RoleTypes.Auto, NormalizedName = Const_RoleTypes.Auto.ToUpper() },
                new Role() { Id = 2, Name = Const_RoleTypes.SysAdmin, NormalizedName = Const_RoleTypes.SysAdmin.ToUpper() },
                new Role() { Id = 3, Name = Const_RoleTypes.Administarator, NormalizedName = Const_RoleTypes.Administarator.ToUpper() },
                new Role() { Id = 4, Name = Const_RoleTypes.User, NormalizedName = Const_RoleTypes.User.ToUpper() }
                );
        }
    }

    public class SecurityRoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable(name: nameof(RoleClaim), schema: "Security");
            builder.HasKey(prop => prop.Id);
            builder.Property(e => e.Id).HasColumnName("RoleClaimID");
            builder.Property(e => e.RoleId).HasColumnName("RoleID");
            builder.Property(e => e.ClaimType).HasMaxLength(50).IsRequired();
            builder.Property(e => e.ClaimValue).HasMaxLength(50).IsRequired();
            builder.HasData(
                new RoleClaim { Id = 1, RoleId = 1, ClaimType = Const_ClaimTypes.AUTO, ClaimValue = Const_ClaimValues.DefaultValue },

                new RoleClaim { Id = 2, RoleId = 2, ClaimType = Const_ClaimTypes.GOD, ClaimValue = Const_ClaimValues.DefaultValue },
                new RoleClaim { Id = 3, RoleId = 2, ClaimType = Const_ClaimTypes.ADMINISTRATOR, ClaimValue = Const_ClaimValues.DefaultValue },
                new RoleClaim { Id = 4, RoleId = 2, ClaimType = Const_ClaimTypes.USER, ClaimValue = Const_ClaimValues.DefaultValue },

                new RoleClaim { Id = 5, RoleId = 3, ClaimType = Const_ClaimTypes.ADMINISTRATOR, ClaimValue = Const_ClaimValues.DefaultValue },
                new RoleClaim { Id = 6, RoleId = 3, ClaimType = Const_ClaimTypes.USER, ClaimValue = Const_ClaimValues.DefaultValue },

                new RoleClaim { Id = 7, RoleId = 4, ClaimType = Const_ClaimTypes.USER, ClaimValue = Const_ClaimValues.DefaultValue }
            );
        }
    }

    public class SecurityUserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable(name: nameof(UserClaim), schema: "Security");
            builder.HasKey(prop => prop.Id);
            builder.Property(e => e.Id).HasColumnName("UserClaimID");
            builder.Property(e => e.UserId).HasColumnName("UserID");
            builder.Property(e => e.ClaimType).HasMaxLength(150).IsRequired();
            builder.Property(e => e.ClaimValue).IsRequired();
        }
    }

    public class SecurityUserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable(name: nameof(UserLogin), schema: "Security");
            builder.Property(e => e.UserId).HasColumnName("UserID");
        }
    }

    public class SecurityUserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(name: nameof(UserRole), schema: "Security");
            builder.HasKey(p => p.Id);
            builder.Property(e => e.UserId).HasColumnName("UserID");
            builder.Property(e => e.RoleId).HasColumnName("RoleID");
            builder.HasData(
                new UserRole { Id = 1, RoleId = 1, UserId = 1 },
                new UserRole { Id = 2, RoleId = 2, UserId = 2 }
                );
        }
    }

    public class SecurityUserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable(name: nameof(UserToken), schema: "Security");
            builder.HasKey(p => p.Id);
            builder.Property(e => e.UserId).HasColumnName("UserID");
        }
    }

    public class SecurityPasswordHistoryConfiguration : IEntityTypeConfiguration<PasswordHistory>
    {
        public void Configure(EntityTypeBuilder<PasswordHistory> builder)
        {
            builder.ToTable(name: nameof(PasswordHistory), schema: "Security");
            builder.HasKey(p => p.Id);
            builder.Property(e => e.UserId).HasColumnName("UserID");
            builder.HasOne(t => t.User).WithMany(t => t.PasswordHistory).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
