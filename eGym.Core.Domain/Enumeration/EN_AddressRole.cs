using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_AddressRole : Enumeration<string>
    {
        public EN_AddressRole() : base() { }
        public EN_AddressRole(string id, string code) : base(id, code) { }

        public static IEnumerable<EN_AddressRole> GetAll() => GetAll<EN_AddressRole>();
        public static EN_AddressRole FromID(string id) => FromID<EN_AddressRole>(id);
        public static EN_AddressRole FromCode(string code) => FromCode<EN_AddressRole>(code);

        public static EN_AddressRole Residential = new EN_AddressRole("RES", "Residenza");
        public static EN_AddressRole Living = new EN_AddressRole("LIV", "Domicilio");
        public static EN_AddressRole Other = new EN_AddressRole("OTH", "Altro");
    }
}
