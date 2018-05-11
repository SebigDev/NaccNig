using NaccNigModels.Members;
using NaccNigModels.PopUp;


namespace NaccNigModels.Payments
{

    public class MemberRegistration
    {
        public int MemberRegistrationId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaidRegistrationFee { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }


    }

    public class Donations
    {
        public int DonationsId { get; set; }
        public decimal Amount { get; set; }
        public bool IsMadeDonations { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }
    }


    public class MonthlyDues
    {
        public int MonthlyDuesId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaidMonthlyDues { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }

    }
}