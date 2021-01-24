using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Core.Log
{
    public interface ILogRepository : IRepository<Log_Master, int>
    {
        #region GDPR
        Task<Log_GDPR> GDPR_SaveAsync(Log_GDPR entity, bool saveChanges = false);
        #endregion
    }
}
