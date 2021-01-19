using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_AnagMasterRole : Enumeration<short>
    {
        public EN_AnagMasterRole() : base() { }
        public EN_AnagMasterRole(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_AnagMasterRole> GetAll() => GetAll<EN_AnagMasterRole>();
        public static EN_AnagMasterRole FromID(short id) => FromID<EN_AnagMasterRole>(id);
    }
}
