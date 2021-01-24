using eGym.Core.Localization.Context;
using eGym.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace eGym.Core.Localization
{
    public class LocalizationDbContext : DbContext, IUnitOfWork
    {
        readonly IConfiguration _configuration;
        public string MigrationConnectionString { get; set; }

        public LocalizationDbContext(IConfiguration configuration) { _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration)); }
        public LocalizationDbContext(DbContextOptions<LocalizationDbContext> options, IConfiguration configuration) { _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration)); }

        #region DbSets
        public DbSet<CMS_History> CMS_Histories { get; set; }
        public DbSet<CMS_Master> CMS_Masters { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                var connString = string.Empty;
                switch (System.Environment.MachineName)
                {
                    case "BUBBLES":
                    case "RSADO":
                        connString = string.Format(_configuration.GetConnectionString("Localization"), $"{System.Environment.MachineName}\\SQLEXPRESS");
                        break;
                    default:
                        connString = string.Format(_configuration.GetConnectionString("Localization"), $"(local)");
                        break;
                }

                optionsBuilder.UseSqlServer(connString);
#else
                optionsBuilder.UseSqlServer(MigrationConnectionString ?? _configuration.GetConnectionString("Localization"));
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<CMS_History>(e =>
            {
                e.HasOne(p => p.CMS_Master).WithMany(p => p.CMS_Histories).HasForeignKey(p => p.CMSH_MasterID).OnDelete(DeleteBehavior.Cascade);
            });

        }

        public IDbTransaction BeginTransaction() => new DbContextTransactionConverter(Database.BeginTransaction());

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) => (await base.SaveChangesAsync() > 0);
    }
}
