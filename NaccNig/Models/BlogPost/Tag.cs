using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.Models.BlogPost
{
    public class Tag
    {

        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }

        public int ID { get; set; }
        public string Name { get; set; }


        public virtual ICollection<Post> Posts { get; set; }
    }
}