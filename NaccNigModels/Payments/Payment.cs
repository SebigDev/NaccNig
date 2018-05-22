using NaccNigModels.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaccNigModels.Payments
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }

        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }
    }

    public enum PayType
    {
        Registration, Dues
    }
    public class PaymentType
    {
        public int Id { get; set; }
        public PayType Pay { get; set; }

        public decimal? TotalPay
        {
            get
            {
                if (Pay.ToString().Equals(PayType.Registration))
                {
                    return 1500;
                }
                if (Pay.ToString().Equals(PayType.Dues))
                {
                    return 500;
                }
                return null;

            }
        }

    }
}
