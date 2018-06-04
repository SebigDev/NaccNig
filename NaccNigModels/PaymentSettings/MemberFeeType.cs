using NaccNigModels.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaccNigModels.PaymentSettings
{
    public class MemberFeeType
    {

        public int MemberFeeTypeId { get; set; }
       
        public int FeeCategoryId { get; set; }
        public PaymentType FeeCategory { get; set; }
        public string FeeName { get; set; }
        public decimal Amount { get; set; }
        public string AmountInWords { get; set; }
        public string Description { get; set; }
        public virtual ICollection<MembershipFee>  MembershipFee { get; set; }
    }
}
