using eGym.Core.Domain.Context;
using eGym.Core.Security.Identity;
using eGym.Core.SeedWork;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eGym.Core.Domain
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) { }

        #region DbSets
        public DbSet<PasswordHistory> PasswordHistory { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                //questa serve per lanciare la migrazione da console di gestione pacchetti
                optionsBuilder.UseSqlServer("Server=RSADO\\SQLEXPRESS; Initial Catalog=eGym.Devlopment; Integrated Security = True; MultipleActiveResultSets=True;");
#endif
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityRoleConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityRoleClaimConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserClaimConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserRoleConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserTokenConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityUserLoginConfiguration());
            builder.ApplyConfiguration(new Security.Configuration.SecurityPasswordHistoryConfiguration());

            builder.Entity<Country>(e =>
            {
                e.HasIndex(p => p.Country_IsoCode).IsUnique(true).IsClustered(false);
                e.HasData(Context.SeedData.TabSchemaSeedData.Country);
            });

            builder.Entity<CMS_History>(e =>
            {
                e.HasOne(p => p.CMS_Master).WithMany(p => p.CMS_Histories).HasForeignKey(p => p.CMSH_MasterID).OnDelete(DeleteBehavior.Cascade);
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
                e.HasOne(p => p.Country).WithMany(p => p.Anag_Addresses).HasForeignKey(p => p.Adr_Country);
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
                e.HasOne(p => p.Country).WithMany(p => p.Anag_Masters).HasForeignKey(p => p.Ang_BirthCountry);
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

            builder.Entity<Sport_DivisionLocalized>(e =>
            {
                e.HasOne(p => p.Sport_Division).WithMany(p => p.Sport_DivisionLocalizeds).HasForeignKey(p => p.SDL_DivisionID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_LevelXSport>(e =>
            {
                e.HasOne(p => p.Sport_Master).WithMany(p => p.Sport_LevelXSports).HasForeignKey(p => p.LXS_SportID).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(p => p.Sport_Level).WithMany(p => p.Sport_LevelXSports).HasForeignKey(p => p.LXS_LevelID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_LevelLocalized>(e =>
            {
                e.HasOne(p => p.Sport_Level).WithMany(p => p.Sport_LevelLocalizeds).HasForeignKey(p => p.SLL_LevelID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_Schedule>(e =>
            {
                e.HasOne(p => p.Sport_Master).WithMany(p => p.Sport_Schedules).HasForeignKey(p => p.SS_SportID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_EventResultLocalized>(e =>
            {
                e.HasOne(p => p.Sport_EventResult).WithMany(p => p.Sport_EventResultLocalizeds).HasForeignKey(p => p.SerL_EventResultID).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Sport_EventResultTypeLocalized>(e =>
            {
                e.HasOne(p => p.Sport_EventResultType).WithMany(p => p.Sport_EventResultTypeLocalizeds).HasForeignKey(p => p.SertL_EventResultTypeID).OnDelete(DeleteBehavior.Cascade);
            });


            //base.OnModelCreating(builder);
        }

        public IDbTransaction BeginTransaction() => new DbContextTransactionConverter(Database.BeginTransaction());

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) => (await base.SaveChangesAsync() > 0);
    }
}
