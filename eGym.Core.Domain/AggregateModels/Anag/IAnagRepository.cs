using eGym.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.Core.Domain
{
    public interface IAnagRepository : IRepository<Anag_Master, int>
    {
        #region ///LOADS
        Task Load_DirectNavigation(
            IEnumerable<int> angIds,
            bool roles = true,
            bool addresses = true,
            bool contacts = true,
            bool documents = false,
            bool athletes = true,
            bool sports = true);

        Task Load_AthleteNavigation(IEnumerable<int> athIds)
        #endregion
    }

    public class AnagRepository : BaseRepository<ApplicationDbContext, Anag_Master, int>, IAnagRepository
    {
        public AnagRepository(ApplicationDbContext context) : base(context) { }

        #region ///LOADS
        public async Task Load_DirectNavigation(
            IEnumerable<int> angIds,
            bool roles = true,
            bool addresses = true,
            bool contacts = true,
            bool documents = false,
            bool athletes = true,
            bool sports = true)
        {
            if (angIds?.Count() > 0)
            {
                if (roles)
                {
                    await _context.Anag_MasterRoles
                        .Where(t => angIds.Contains(t.AngR_AnagID))
                        .LoadAsync();

                    await _context.Anag_CorporateRoles
                        .Where(t => angIds.Contains(t.CR_AnagID))
                        .LoadAsync();
                }

                if (addresses)
                    await _context.Anag_Addresses
                        .Where(t => angIds.Contains(t.Adr_AnagID))
                        .Include(t => t.Anag_AddressRoles)
                        .LoadAsync();

                if (contacts)
                    await _context.Anag_Contacts
                    .Where(t => angIds.Contains(t.Cnt_AnagID))
                    .LoadAsync();

                if (documents)
                    await _context.Anag_Documents
                        .Where(t => angIds.Contains(t.Doc_AnagID))
                        .LoadAsync();

                if (athletes && !sports)
                    await _context.Athlete_Masters
                        .Where(t => angIds.Contains(t.Ath_AnagID))
                        .LoadAsync();

                if (sports)
                    await _context.Athlete_Masters
                        .Where(t => angIds.Contains(t.Ath_AnagID))
                        .Include(t => t.Sport_Master)
                        .LoadAsync();
            }
        }

        public async Task Load_AthleteNavigation(IEnumerable<int> athIds)
        {
            if (athIds?.Count() > 0)
            {
                await _context.Athlete_DivisionXAthletes
                    .Where(t => athIds.Contains(t.DXA_AthleteID))
                    .Include(t => t.Sport_Division)
                    .LoadAsync();

                await _context.Athlete_LevelXAthletes
                    .Where(t => athIds.Contains(t.LXA_AthleteID))
                    .Include(t => t.Sport_Level)
                    .LoadAsync();

                await _context.Athlete_WeightXAthletes
                    .Where(t => athIds.Contains(t.WXA_AthleteID))
                    .LoadAsync();
            }
        }
        #endregion
    }
}
