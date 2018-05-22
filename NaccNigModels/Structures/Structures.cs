using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaccNigModels.Structures
{
    public class Province
    {   [Key]
        public int ProId { get; set; }
        public string ProvinceName { get; set; }
    }


    public class StateChapter
    {   [Key]
        public int StateChapId { get; set; }
        public string StateChapterName { get; set; }
        public int ProId { get; set; }



    }

    public class Zone
    {   [Key]
        public int ZId { get; set; }
        public string ZoneName { get; set; }
        public int StateChapId { get; set; }
    }

}
