using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace eGym.Core.Domain
{
    public class EN_AnagMasterRole : Enumeration<short>
    {
        public EN_AnagMasterRole() : base() { }
        public EN_AnagMasterRole(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_AnagMasterRole> GetAll() => GetAll<EN_AnagMasterRole>();
        public static EN_AnagMasterRole FromID(short id) => FromID<EN_AnagMasterRole>(id);

        public static EN_AnagMasterRole Affiliate = new EN_AnagMasterRole(1, nameof(Affiliate));
        public static EN_AnagMasterRole Employee = new EN_AnagMasterRole(2, nameof(Employee));
        public static EN_AnagMasterRole Customer = new EN_AnagMasterRole(3, nameof(Customer));
        public static EN_AnagMasterRole Supplier = new EN_AnagMasterRole(4, nameof(Supplier));
        public static EN_AnagMasterRole Sponsor = new EN_AnagMasterRole(5, nameof(Sponsor));
    }
}
