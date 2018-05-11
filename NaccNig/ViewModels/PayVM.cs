using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NaccNigModels.Members;
using NaccNigModels.Payments;
using NaccNigModels.PopUp;

namespace NaccNig.ViewModels
{
    public class PayVM
    {
        public int MonthlyDuesId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaidMonthlyDues { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }
    }
}