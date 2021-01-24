using eGym.Core.SeedWork;
using System.Threading.Tasks;

namespace eGym.Core.Log
{
    public class LogRepository : BaseRepository<LogDbContext, Log_Master, int>, ILogRepository
    {

        public LogRepository(LogDbContext dbContext) : base(dbContext) { }

        public async Task<Log_GDPR> GDPR_SaveAsync(Log_GDPR entity, bool saveChanges = false)
        {
            if (entity.Id <= 0)
                entity = _context.Log_GDPRs.Add(entity).Entity;
            else
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            if (saveChanges)
                await _context.SaveEntitiesAsync();

            return entity;
        }
    }
}
