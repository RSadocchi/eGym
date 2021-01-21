using eGym.Core.Security.Configuration;
using eGym.Core.Security.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eGym.Core.Security
{
    public class SecurityDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        readonly IConfiguration _configuration;
        public string MigrationConnectionString { get; set; }

        #region ///DbSets
        public DbSet<PasswordHistory> PasswordHistory { get; set; }
        public DbSet<UserVoucher> UserVouchers { get; set; }
        public DbSet<Captcha> Captchas { get; set; }
        public DbSet<ServiceToken> ServiceTokens { get; set; }
        #endregion

        #region ///Constructors
        public SecurityDbContext() { }
        public SecurityDbContext(IConfiguration configuration) { _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration)); }
        public SecurityDbContext(DbContextOptions<DbContext> options, IConfiguration configuration) : base(options) { _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration)); }
        #endregion

        #region ///Override Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(MigrationConnectionString ?? _configuration.GetConnectionString("Security"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new Security.Configuration.SecurityUserConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityRoleConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityRoleClaimConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserRoleConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserClaimConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserTokenConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserLoginConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityPasswordHistoryConfiguration());
        }
        #endregion
    }
}
