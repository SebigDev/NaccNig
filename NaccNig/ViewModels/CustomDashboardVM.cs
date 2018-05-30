
using NaccNigModels.PopUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class CustomDashboardVM
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public MemberCategory MemberCategory { get; set; }
    }
}