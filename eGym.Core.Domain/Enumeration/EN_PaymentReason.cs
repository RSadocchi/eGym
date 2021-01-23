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

        public static EN_PaymentReason Other = new EN_PaymentReason(0, nameof(Other));

        public static EN_PaymentReason Membership = new EN_PaymentReason(1, nameof(Membership));
        public static EN_PaymentReason MembershipFee = new EN_PaymentReason(2, nameof(MembershipFee));
        public static EN_PaymentReason ExpensesReinbursement = new EN_PaymentReason(3, nameof(LiberalIssue));
        public static EN_PaymentReason VoluntaryContribution = new EN_PaymentReason(4, nameof(VoluntaryContribution));
        public static EN_PaymentReason LiberalIssue = new EN_PaymentReason(5, nameof(LiberalIssue));
        public static EN_PaymentReason EventRegistration = new EN_PaymentReason(6, nameof(EventRegistration));
        public static EN_PaymentReason PurchaseProducts = new EN_PaymentReason(7, nameof(PurchaseProducts));
        public static EN_PaymentReason PurchaseServices = new EN_PaymentReason(8, nameof(PurchaseServices));
        public static EN_PaymentReason Sponsorship = new EN_PaymentReason(9, nameof(Sponsorship));
        public static EN_PaymentReason Advertising = new EN_PaymentReason(10, nameof(Advertising));
        public static EN_PaymentReason Donation = new EN_PaymentReason(11, nameof(Donation));

        public static EN_PaymentReason Invoice = new EN_PaymentReason(100, nameof(Invoice));
        public static EN_PaymentReason Bill = new EN_PaymentReason(101, nameof(Bill));
    }
}
