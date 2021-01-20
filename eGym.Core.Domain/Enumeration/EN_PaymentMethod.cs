using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_PaymentMethod : Enumeration<short>
    {
        public EN_PaymentMethod() : base() { }
        public EN_PaymentMethod(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_PaymentMethod> GetAll() => GetAll<EN_PaymentMethod>();
        public static EN_PaymentMethod FromID(short id) => FromID<EN_PaymentMethod>(id);

        public static EN_PaymentMethod Cash = new EN_PaymentMethod(1, nameof(Cash));
        public static EN_PaymentMethod ATM = new EN_PaymentMethod(2, nameof(ATM));
        public static EN_PaymentMethod CreditCard = new EN_PaymentMethod(3, nameof(CreditCard));
        public static EN_PaymentMethod WireTransfer = new EN_PaymentMethod(4, nameof(WireTransfer));
        public static EN_PaymentMethod Financing = new EN_PaymentMethod(5, nameof(Financing));

    }
}
