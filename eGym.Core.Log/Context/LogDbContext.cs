using eGym.Core.Log.Context;
using eGym.Core.SeedWork;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGym.Core.Log
{
    public class LogDbContext : DbContext, IUnitOfWork
    {
        readonly IConfiguration _configuration;
        public string MigrationConnectionString { get; set; }

        public LogDbContext(IConfiguration configuration) { _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration)); }
        public LogDbContext(DbContextOptions<LogDbContext> options, IConfiguration configuration) { _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration)); }

        #region DbSets
        public DbSet<Log_Master> Log_Masters { get; set; }
        public DbSet<Batch_Master> Batch_Masters { get; set; }
        public DbSet<Batch_LogXBatch> Batch_LogXBatches { get; set; }
        public DbSet<Log_GDPR> Log_GDPRs { get; set; }
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
                        connString = string.Format(_configuration.GetConnectionString("Log"), $"{System.Environment.MachineName}\\SQLEXPRESS");
                        break;
                    default:
                        connString = string.Format(_configuration.GetConnectionString("Log"), $"(local)");
                        break;
                }

                optionsBuilder.UseSqlServer(connString);
#else
                optionsBuilder.UseSqlServer(MigrationConnectionString ?? _configuration.GetConnectionString("Log"));
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Batch_LogXBatch>(e =>
            {
                e.HasOne(p => p.Batch).WithMany(p => p.LogXBatches).HasForeignKey(p => p.BatchId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Log).WithMany(p => p.LogXBatches).HasForeignKey(p => p.LogId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Log_Master>(e =>
            {
                e.HasOne(p => p.Parent).WithMany(p => p.Childs).HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.NoAction);
            });
            
            builder.Entity<Log_GDPR>(e =>
            {
                //e.Property(p => p.TableNames)
                //    .HasConversion(
                //        convertToProviderExpression: v => string.Join(',', v ?? Array.Empty<string>()),
                //        convertFromProviderExpression: v => !string.IsNullOrWhiteSpace(v) ? v.Split(',', StringSplitOptions.RemoveEmptyEntries).Where(t => !string.IsNullOrWhiteSpace(t)).ToArray() : Array.Empty<string>());

                //e.Property(p => p.FieldNames)
                //    .HasConversion(
                //        convertToProviderExpression: v => string.Join(',', v ?? Array.Empty<string>()),
                //        convertFromProviderExpression: v => !string.IsNullOrWhiteSpace(v) ? v.Split(',', StringSplitOptions.RemoveEmptyEntries).Where(t => !string.IsNullOrWhiteSpace(t)).ToArray() : Array.Empty<string>());
            });
        }

        public IDbTransaction BeginTransaction() => new DbContextTransactionConverter(Database.BeginTransaction());

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) => (await base.SaveChangesAsync() > 0);
    }
}
