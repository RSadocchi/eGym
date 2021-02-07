using eGym.Application.DTO;
using eGym.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public interface IAnagService
    {
        Task<IQueryable<Anag_Master>> ListAsync(AnagFilters filters);
        Task<Anag_Master> FindByAsync(int? id = null, string taxCode = null, int? userId = null, int? athleteId = null);
        Task<Anag_Master> SaveAsync(AnagDTO dto = null, Anag_Master entity = null);
    }

    public class AnagService
    {

    }
}
