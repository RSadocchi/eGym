using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_AccountancyDocType : Enumeration<short>
    {
        public EN_AccountancyDocType() : base() { }
        public EN_AccountancyDocType(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_AccountancyDocType> GetAll() => GetAll<EN_AccountancyDocType>();
        public static EN_AccountancyDocType FromID(short id) => FromID<EN_AccountancyDocType>(id);

        public static EN_AccountancyDocType Other = new EN_AccountancyDocType(0, nameof(Other));
        public static EN_AccountancyDocType Invoice = new EN_AccountancyDocType(1, nameof(Invoice));
        public static EN_AccountancyDocType Bill = new EN_AccountancyDocType(2, nameof(Bill));
    }
}
