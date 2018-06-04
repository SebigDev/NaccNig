using NaccNigModels.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.Models
{
   
        public class RemitaPaymentLog
        {
            public int RemitaPaymentLogId { get; set; }
            public string OrderId { get; set; }
            public string Amount { get; set; }
            public string Rrr { get; set; }
            public string StatusCode { get; set; }
            public string TransactionMessage { get; set; }
            public DateTime PaymentDate { get; set; }
            public PaymentType PaymentName { get; set; }
            public string PayerName { get; set; }
        }
}