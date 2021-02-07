using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Application
{
    public class AnagFilters
    {
        public IEnumerable<short> Roles { get; set; }
        public string SearchString { get; set; }
        public int SportID { get; set; }
    }
}
