using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Core.Domain.AggregateModels
{
    
    public interface ICountryRepository : IRepository<Country, string>
    {
        Country this[string isoCode] { get; }
    }
}
