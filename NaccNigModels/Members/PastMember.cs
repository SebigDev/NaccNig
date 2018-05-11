using System;
using System.Collections.Generic;
using NaccNigModels.Payments;
using System.Text;

namespace NaccNigModels.Members
{
    public class PastMember : Person
    {
        public string PastMemberId { get; set; }
        public string StateServed { get; set; }
        public virtual ICollection<MemberRegistration> MemberRegistration { get; set; }
        public virtual ICollection<Donations> Donations { get; set; }
        public virtual ICollection<MonthlyDues> MonthlyDues { get; set; }


    }
}
