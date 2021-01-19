using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_ContactType : Enumeration<short>
    {
        public EN_ContactType() : base() { }
        public EN_ContactType(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_ContactType> GetAll() => GetAll<EN_ContactType>();
        public static EN_ContactType FromID(short id) => FromID<EN_ContactType>(id);


        public static EN_ContactType Email = new EN_ContactType(10, nameof(Email));
        public static EN_ContactType PEC = new EN_ContactType(11, nameof(PEC));
        
        public static EN_ContactType Mobile = new EN_ContactType(20, nameof(Mobile));
        public static EN_ContactType Telephone = new EN_ContactType(21, nameof(Telephone));
        public static EN_ContactType Fax = new EN_ContactType(22, nameof(Fax));
        
        public static EN_ContactType Url = new EN_ContactType(30, nameof(Url));
        public static EN_ContactType Linkedin = new EN_ContactType(31, nameof(Linkedin));
        public static EN_ContactType Facebook = new EN_ContactType(32, nameof(Facebook));
        public static EN_ContactType Twitter = new EN_ContactType(33, nameof(Twitter));
        public static EN_ContactType Instagram = new EN_ContactType(34, nameof(Instagram));
    }
}
