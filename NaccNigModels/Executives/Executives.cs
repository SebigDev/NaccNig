using NaccNigModels.Members;
using NaccNigModels.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaccNigModels.Executives
{
    public class Executives
    {

        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string ActiveMemberId { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }
    }

    public class StateExco : Executives
    {
        public int StateExcoId { get; set; }
        public virtual StateChapter StateChapter { get; set; }
    }
   

    public class ZonalExco : Executives
    {
        public int ZonalExcoId { get; set; }
        public int ZoneId { get; set; }
        public virtual ZonalChapter ZonalChapter { get; set; }

    }
    public class ZonalChapter
    {
        public int Id { get; set; }
        public string ZoneName { get; set; }
    }

    public class Portfolio
    {
        public int Id { get; set; }
        public string PortfolioName { get; set; }
    }

}
