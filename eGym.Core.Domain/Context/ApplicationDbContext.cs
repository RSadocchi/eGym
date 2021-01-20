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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public IDbTransaction BeginTransaction() => new DbContextTransactionConverter(Database.BeginTransaction());

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) => (await base.SaveChangesAsync() > 0);
    }
}
