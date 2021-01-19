using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_AddressRole : Enumeration<short>
    {
        public EN_AddressRole() : base() { }
        public EN_AddressRole(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_AddressRole> GetAll() => GetAll<EN_AddressRole>();
        public static EN_AddressRole FromID(short id) => FromID<EN_AddressRole>(id);

        public static EN_AddressRole Residential = new EN_AddressRole(1, nameof(Residential));
        public static EN_AddressRole Living = new EN_AddressRole(2, nameof(Living));
        public static EN_AddressRole Other = new EN_AddressRole(100, nameof(Other));
    }
}
