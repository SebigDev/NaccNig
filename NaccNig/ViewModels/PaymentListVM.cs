using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class PaymentListVM
    {
        public string PayerId { get; set; }
        public string Fullname { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public string StateOfOrigin { get; set; }
        public string StateOfDeployment { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}