using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_ContactType : Enumeration<char>
    {
        public EN_ContactType() : base() { }
        public EN_ContactType(char id, string code) : base(id, code) { }

        public static IEnumerable<EN_ContactType> GetAll() => GetAll<EN_ContactType>();
        public static EN_ContactType FromID(char id) => FromID<EN_ContactType>(id);
        public static EN_ContactType FromCode(string code) => FromCode<EN_ContactType>(code);

        public static EN_ContactType Email = new EN_ContactType('e', nameof(Email));
        public static EN_ContactType PEC = new EN_ContactType('E', nameof(PEC));
        public static EN_ContactType Mobile = new EN_ContactType('m', "Cellulare");
        public static EN_ContactType Phone = new EN_ContactType('p', "Telefono fisso");
    }
}
