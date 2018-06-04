
using NaccNigModels.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
  
        public class RemitaPostVm
        {
            public string payerName { get; set; }
            public string payerEmail { get; set; }
            public string payerPhone { get; set; }
            public string orderId { get; set; }
            public string merchantId { get; set; }
            public string serviceTypeId { get; set; }
            public string responseurl { get; set; }
            public string amt { get; set; }
            public string hash { get; set; }
            public string paymentType { get; set; }

        }

        public class ConfirmRrr
        {
            public string rrr { get; set; }

            public PaymentType PaymentType{ get; set; }
        }
    }