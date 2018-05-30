
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class PaymentVM
    {
        public string PaymentId { get; set; }
        public int AmountId { get; set; }
        public int PaymentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Amount { get; set; }
        public string MemberName { get; set; }
        public string Photo { get; set; }

    }
}