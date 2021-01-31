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

    public class EN_TodoStatus : Enumeration<short>
    {
        public EN_TodoStatus() : base() { }
        public EN_TodoStatus(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_TodoStatus> GetAll() => GetAll<EN_TodoStatus>();
        public static EN_TodoStatus FromID(short id) => FromID<EN_TodoStatus>(id);

        public static EN_TodoStatus Scheduled = new EN_TodoStatus(0, nameof(Scheduled));
        public static EN_TodoStatus Completed = new EN_TodoStatus(1, nameof(Completed));
        public static EN_TodoStatus Deleted = new EN_TodoStatus(2, nameof(Deleted));
        public static EN_TodoStatus Suspensed = new EN_TodoStatus(3, nameof(Suspensed));
        public static EN_TodoStatus Expired = new EN_TodoStatus(4, nameof(Expired));
    }
}
