using eGym.Core.SeedWork;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace eGym.Core.Domain.Context
{
    public class DbContextTransactionConverter : IDbTransaction
    {
        readonly IDbContextTransaction _dbContextTransaction;

        public DbContextTransactionConverter(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }
    }
}
