using NaccNigModels.Executives;
using NaccNigModels.Members;
using NaccNigModels.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class StateExcoDetailVM
    {
        public string ExcoId { get; set; }
        public string PortfolioName { get; set; }
        public string Fullname { get; set; }
        public string StateOfOrigin { get; set; }
        public string Address { get; set; }
        public string StateCode{ get; set; }
        public string Photo { get; set; }
        public string Gender { get; set; }
        public int StateExcoId { get; set; }
        public string ChapterName { get; set; }
        public int ZonalExcoId { get; set; }
        public string ZoneName { get; set; }
        public string PhoneNumber { get; set; }
        public int ZoneId { get; set; }
    }
}