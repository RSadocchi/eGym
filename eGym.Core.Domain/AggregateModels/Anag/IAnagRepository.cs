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
        Task Load_Roles(params int[] angIds);
        Task Load_AddressesAndContacts(params int[] angIds);
        Task Load_Documents(params int[] angIds);
        #endregion
    }

    public class AnagRepository : BaseRepository<ApplicationDbContext, Anag_Master, int>, IAnagRepository
    {
        #region ///LOADS
        public AnagRepository(ApplicationDbContext context) : base(context) { }

        public async Task Load_Roles(params int[] angIds)
        {
            if (angIds?.Count() > 0)
            {
                await _context.Anag_MasterRoles
                    .Where(t => angIds.Contains(t.AngR_AnagID))
                    .LoadAsync();

                await _context.Anag_CorporateRoles
                    .Where(t => angIds.Contains(t.CR_AnagID))
                    .LoadAsync();
            }
        }

        public async Task Load_AddressesAndContacts(params int[] angIds)
        {
            if (angIds?.Count() > 0)
            {
                await _context.Anag_Addresses
                    .Where(t => angIds.Contains(t.Adr_AnagID))
                    .Include(t => t.Anag_AddressRoles)
                    .LoadAsync();

                await _context.Anag_Contacts
                    .Where(t => angIds.Contains(t.Cnt_AnagID))
                    .LoadAsync();
            }
        }

        public async Task Load_Documents(params int[] angIds)
        {
            if (angIds?.Count() > 0)
            {
                await _context.Anag_Documents
                    .Where(t => angIds.Contains(t.Doc_AnagID))
                    .LoadAsync();
            }
        }
        #endregion
    }
}
