using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaccNigModels.PaymentSettings
{

    public class FeeCategory
    {
        public int FeeCategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<MembershipFee> MembershipFee { get; set; }
      
    }
}
