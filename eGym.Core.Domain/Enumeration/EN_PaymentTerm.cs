using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_PaymentTerm : Enumeration<short>
    {
        public EN_PaymentTerm() : base() { }
        public EN_PaymentTerm(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_PaymentTerm> GetAll() => GetAll<EN_PaymentTerm>();
        public static EN_PaymentTerm FromID(short id) => FromID<EN_PaymentTerm>(id);

        public static EN_PaymentTerm Daily = new EN_PaymentTerm(365, nameof(Daily));
        public static EN_PaymentTerm Weekly = new EN_PaymentTerm(52, nameof(Weekly));
        public static EN_PaymentTerm Monthly = new EN_PaymentTerm(12, nameof(Monthly));
        public static EN_PaymentTerm Bimonthly = new EN_PaymentTerm(6, nameof(Bimonthly));
        public static EN_PaymentTerm Quarterly = new EN_PaymentTerm(4, nameof(Quarterly));
        public static EN_PaymentTerm HalfYearly = new EN_PaymentTerm(2, nameof(HalfYearly));
        public static EN_PaymentTerm Annual = new EN_PaymentTerm(1, nameof(Annual));
    }
}
