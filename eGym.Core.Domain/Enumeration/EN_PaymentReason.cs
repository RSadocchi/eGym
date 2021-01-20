using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_PaymentReason : Enumeration<short>
    {
        public EN_PaymentReason() : base() { }
        public EN_PaymentReason(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_PaymentReason> GetAll() => GetAll<EN_PaymentReason>();
        public static EN_PaymentReason FromID(short id) => FromID<EN_PaymentReason>(id);

        public static EN_PaymentReason MembershipFee = new EN_PaymentReason(1, nameof(MembershipFee));
        public static EN_PaymentReason PurchaseProducts = new EN_PaymentReason(2, nameof(PurchaseProducts));
        public static EN_PaymentReason PurchaseServices = new EN_PaymentReason(3, nameof(PurchaseServices));
        public static EN_PaymentReason VoluntaryContribution = new EN_PaymentReason(4, nameof(VoluntaryContribution));
        public static EN_PaymentReason Sponsorship = new EN_PaymentReason(5, nameof(Sponsorship));
        public static EN_PaymentReason Advertising = new EN_PaymentReason(6, nameof(Advertising));
        public static EN_PaymentReason Membership = new EN_PaymentReason(7, nameof(Membership));
        public static EN_PaymentReason EventRegistration = new EN_PaymentReason(8, nameof(EventRegistration));
    }
}
