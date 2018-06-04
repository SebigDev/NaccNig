using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class BaseVm
    {
        public bool HasPayedMembershipRegistration { get; set; }

        public bool HasPayedMonthlyDues { get; set; }
        
    }
}