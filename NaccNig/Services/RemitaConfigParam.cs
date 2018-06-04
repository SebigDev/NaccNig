using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.Services
{
    public class RemitaConfigParam
    {
        public const string MERCHANTID = "2266665151";
        public const string MEMBERSHIPREGISTRATION = "2248674655";
        public const string MONTHLYDUES = "2223831078";
       



        public const string APIKEY = "591873";
        public const string GATEWAYURL = "https://login.remita.net/remita/ecomm/init.reg";
        public const string CHECKSTATUSURL = "https://login.remita.net/remita/ecomm";
    }
    public class RemitaRePostVm
    {
        public string merchantId { get; set; }
        public string hash { get; set; }
        public string rrr { get; set; }
        public string responseurl { get; set; }
    }

}