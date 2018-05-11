using NaccNigModels.Members;
using NaccNigModels.Payments;
using System;
using System.Collections.Generic;

namespace NaccNig.ViewModels
{
    public class PaymentOptionsVM
    {
        public int PaymentOptionsId { get; set; }
        public string PaymentOptionsName { get; set; }
        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }
        public virtual ICollection<MemberRegistration> MemberRegistration { get; set; }
        public virtual ICollection<Donations> Donations { get; set; }
        public virtual ICollection<MonthlyDues> MonthlyDues { get; set; }

    }
   
   
    public class MonthlyDuesVm
    {
        public int MonthlyDuesId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaidMonthlyDues { get; set; }
        public string PaymentStatus { get; set; }
        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }
    }
}