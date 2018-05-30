using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaccNigModels.Structures
{
    public class StateChapter
    {   [Key]
        public int StateChapId { get; set; }
        public string StateChapterName { get; set; }
        public static IQueryable<StateChapter> GetStateChapters()
        {
            return new List<StateChapter>
            {
                new StateChapter{ StateChapId = 1, StateChapterName ="Plateau State"}, 
            }.AsQueryable();
        }



    }

    public class Zone
    {   [Key]
        public int ZId { get; set; }
        public string ZoneName { get; set; }
        public int StateChapId { get; set; }

        public static IQueryable<Zone> GetZones()
        {
            return new List<Zone>
            {
                new Zone{ZId = 1, ZoneName = "Jos North", StateChapId=1},
                 new Zone{ZId = 2, ZoneName = "Jos South", StateChapId=1},
                  new Zone{ZId = 3, ZoneName = "Jos East", StateChapId=1},
                   new Zone{ZId = 4, ZoneName = "Bassa", StateChapId=1},
                    new Zone{ZId = 5, ZoneName = "Barkin Ladi", StateChapId=1},
                     new Zone{ZId = 6, ZoneName = "Mangu", StateChapId=1},
                      new Zone{ZId = 7, ZoneName = "Pankshin", StateChapId=1},
                       new Zone{ZId = 8, ZoneName = "Shendam", StateChapId=1},
                        new Zone{ZId = 9, ZoneName = "Langtan North", StateChapId=1},
                         new Zone{ZId = 10, ZoneName = "Langtan South", StateChapId=1},
                          new Zone{ZId = 11, ZoneName = "Mikan", StateChapId=1},
                           new Zone{ZId = 12, ZoneName = "Kanam", StateChapId=1},
                            new Zone{ZId = 13, ZoneName = "Kanke", StateChapId=1},
                             new Zone{ZId = 14, ZoneName = "Quan Pan", StateChapId=1},
                              new Zone{ZId = 15, ZoneName = "Riyom", StateChapId=1},
                               new Zone{ZId = 16, ZoneName = "Bokkos", StateChapId=1},
                                new Zone{ZId = 17, ZoneName = "Wase", StateChapId=1},
                                
            }.AsQueryable();
        }
    }

}
