using NaccNigModels.Members;
using System.Collections.Generic;
using NaccNigModels.Payments;
using NaccNigModels;
using NaccNigModels.PopUp;

namespace NaccNigModels.Payments
{
    public class PaymentOptions
    {
        public int PaymentOptionsId { get; set; }
        public PaymentOptionsName PaymentOptionsName { get; set; }
        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }
        public virtual ICollection<MemberRegistration> MemberRegistration { get; set; }
        public virtual ICollection<Donations> Donations { get; set; }
        public virtual ICollection<MonthlyDues> MonthlyDues { get; set; }
       
    }

    

}