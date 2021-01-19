using System;

namespace eGym.Core.SeedWork
{
    public interface IDbTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
