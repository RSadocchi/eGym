using eGym.Core.Domain.Context;
using eGym.Core.Security.Identity;
using eGym.Core.SeedWork;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace eGym.Core.Domain
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        readonly IConfiguration _configuration;
        public string MigrationConnectionString { get; set; }

        public ApplicationDbContext(IConfiguration configuration) { _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration)); }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) { _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration)); }

        #region DbSets
        public DbSet<Anag_Address> Anag_Addresses { get; set; }
        public DbSet<Anag_AddressRole> Anag_AddressRoles { get; set; }
        public DbSet<Anag_Contact> Anag_Contacts { get; set; }
        public DbSet<Anag_CorporateRole> Anag_CorporateRoles { get; set; }
        public DbSet<Anag_Document> Anag_Documents { get; set; }
        public DbSet<Anag_Master> Anag_Masters { get; set; }
        public DbSet<Anag_MasterRole> Anag_MasterRoles { get; set; }

        public DbSet<Athlete_DivisionXAthlete> Athlete_DivisionXAthletes { get; set; }
        public DbSet<Athlete_LevelXAthlete> Athlete_LevelXAthletes { get; set; }
        public DbSet<Athlete_Master> Athlete_Masters { get; set; }
        public DbSet<Athlete_WeightXAthlete> Athlete_WeightXAthletes { get; set; }

        public DbSet<Sport_Division> Sport_Divisions { get; set; }
        public DbSet<Sport_DivisionLocalized> Sport_DivisionLocalizeds { get; set; }
        public DbSet<Sport_DivisionXSport> Sport_DivisionXSports { get; set; }
        public DbSet<Sport_EventResult> Sport_EventResults { get; set; }
        public DbSet<Sport_EventResultLocalized> Sport_EventResultLocalizeds { get; set; }
        public DbSet<Sport_EventResultType> Sport_EventResultTypes { get; set; }
        public DbSet<Sport_EventResultTypeLocalized> Sport_EventResultTypeLocalizeds { get; set; }
        public DbSet<Sport_Level> Sport_Levels { get; set; }
        public DbSet<Sport_LevelLocalized> Sport_LevelLocalizeds { get; set; }
        public DbSet<Sport_LevelXSport> Sport_LevelXSports { get; set; }
        public DbSet<Sport_Master> Sport_Masters { get; set; }
        public DbSet<Sport_Schedule> Sport_Schedules { get; set; }

        public DbSet<Country> Countries { get; set; }
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
                        connString = string.Format(_configuration.GetConnectionString("Default"), $"{System.Environment.MachineName}\\SQLEXPRESS");
                        break;
                    default:
                        connString = string.Format(_configuration.GetConnectionString("Default"), $"(local)");
                        break;
                }

                optionsBuilder.UseSqlServer(connString);
#else
                optionsBuilder.UseSqlServer(MigrationConnectionString ?? _configuration.GetConnectionString("Default"));
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Country>(e =>
            {
                e.HasIndex(p => p.Country_IsoCode).IsUnique(true).IsClustered(false);
                e.HasData(Context.SeedData.TabSchemaSeedData.Country);
            });

            builder.Entity<Anag_MasterRole>(e =>
            {
                e.HasOne(p => p.Anag_Master).WithMany(p => p.Anag_MasterRoles).HasForeignKey(p => p.AngR_AnagID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Anag_CorporateRole>(e =>
            {
                e.HasOne(p => p.Anag_Master).WithMany(p => p.Anag_CorporateRoles).HasForeignKey(p => p.CR_AnagID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Anag_AddressRole>(e =>
            {
                e.HasOne(p => p.Anag_Address).WithMany(p => p.Anag_AddressRoles).HasForeignKey(p => p.AdrR_AddressID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Anag_Address>(e =>
            {
                e.HasOne(p => p.Anag_Master).WithMany(p => p.Anag_Addresses).HasForeignKey(p => p.Adr_AnagID).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Country).WithMany(p => p.Anag_Addresses).HasForeignKey(p => p.Adr_Country).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Anag_Contact>(e =>
            {
                e.HasOne(p => p.Anag_Master).WithMany(p => p.Anag_Contacts).HasForeignKey(p => p.Cnt_AnagID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Anag_Document>(e =>
            {
                e.HasOne(p => p.Anag_Master).WithMany(p => p.Anag_Documents).HasForeignKey(p => p.Doc_AnagID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Anag_Master>(e =>
            {
                e.HasOne(p => p.Country).WithMany(p => p.Anag_Masters).HasForeignKey(p => p.Ang_BirthCountry).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Athlete_Master>(e =>
            {
                e.HasOne(p => p.Anag_Master).WithOne(p => p.Athlete_Master).HasForeignKey<Athlete_Master>(p => p.Ath_AnagID).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Sport_Master).WithMany(p => p.Athlete_Masters).HasForeignKey(p => p.Ath_SportID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Athlete_WeightXAthlete>(e =>
            {
                e.HasOne(p => p.Athlete_Master).WithMany(p => p.Athlete_WeightXAthletes).HasForeignKey(p => p.WXA_AthleteID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Athlete_DivisionXAthlete>(e =>
            {
                e.HasOne(p => p.Athlete_Master).WithMany(p => p.Athlete_DivisionXAthletes).HasForeignKey(p => p.DXA_AthleteID).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Sport_Division).WithMany(p => p.Athlete_DivisionXAthletes).HasForeignKey(p => p.DXA_DivisionID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Athlete_LevelXAthlete>(e =>
            {
                e.HasOne(p => p.Athlete_Master).WithMany(p => p.Athlete_LevelXAthletes).HasForeignKey(p => p.LXA_AthleteID).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Sport_Level).WithMany(p => p.Athlete_LevelXAthletes).HasForeignKey(p => p.LXA_LevelID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_DivisionXSport>(e =>
            {
                e.HasOne(p => p.Sport_Master).WithMany(p => p.Sport_DivisionXSports).HasForeignKey(p => p.DXS_SportID).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Sport_Division).WithMany(p => p.Sport_DivisionXSports).HasForeignKey(p => p.DXS_DivisionID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_Division>(e =>
            {
                e.HasData(Context.SeedData.ConfigSchemaSeedData.Sport_Division);
            });

            builder.Entity<Sport_DivisionLocalized>(e =>
            {
                e.HasOne(p => p.Sport_Division).WithMany(p => p.Sport_DivisionLocalizeds).HasForeignKey(p => p.SDL_DivisionID).OnDelete(DeleteBehavior.Cascade);
                e.HasData(Context.SeedData.ConfigSchemaSeedData.Sport_DivisionLocalized);
            });

            builder.Entity<Sport_LevelXSport>(e =>
            {
                e.HasOne(p => p.Sport_Master).WithMany(p => p.Sport_LevelXSports).HasForeignKey(p => p.LXS_SportID).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Sport_Level).WithMany(p => p.Sport_LevelXSports).HasForeignKey(p => p.LXS_LevelID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_Level>(e =>
            {
                e.HasData(Context.SeedData.ConfigSchemaSeedData.Sport_Level);
            });

            builder.Entity<Sport_LevelLocalized>(e =>
            {
                e.HasOne(p => p.Sport_Level).WithMany(p => p.Sport_LevelLocalizeds).HasForeignKey(p => p.SLL_LevelID).OnDelete(DeleteBehavior.Cascade);
                e.HasData(Context.SeedData.ConfigSchemaSeedData.Sport_LevelLocalized);
            });

            builder.Entity<Sport_Schedule>(e =>
            {
                e.HasOne(p => p.Sport_Master).WithMany(p => p.Sport_Schedules).HasForeignKey(p => p.SS_SportID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_EventResult>(e =>
            {
                e.HasData(Context.SeedData.ConfigSchemaSeedData.Sport_EventResult);
            });

            builder.Entity<Sport_EventResultLocalized>(e =>
            {
                e.HasOne(p => p.Sport_EventResult).WithMany(p => p.Sport_EventResultLocalizeds).HasForeignKey(p => p.SerL_EventResultID).OnDelete(DeleteBehavior.Cascade);
                e.HasData(Context.SeedData.ConfigSchemaSeedData.Sport_EventResultLocalized);
            });

            builder.Entity<Sport_EventResultType>(e =>
            {
                e.HasData(Context.SeedData.ConfigSchemaSeedData.Sport_EventResultType);
            });

            builder.Entity<Sport_EventResultTypeLocalized>(e =>
            {
                e.HasOne(p => p.Sport_EventResultType).WithMany(p => p.Sport_EventResultTypeLocalizeds).HasForeignKey(p => p.SertL_EventResultTypeID).OnDelete(DeleteBehavior.Cascade);
                e.HasData(Context.SeedData.ConfigSchemaSeedData.Sport_EventResultTypeLocalized);
            });


            //base.OnModelCreating(builder);
        }

        public IDbTransaction BeginTransaction() => new DbContextTransactionConverter(Database.BeginTransaction());

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) => (await base.SaveChangesAsync() > 0);
    }
}
