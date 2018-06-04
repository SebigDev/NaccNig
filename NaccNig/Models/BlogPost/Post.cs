using NaccNigModels.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.Models.BlogPost
{
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.Tags = new HashSet<Tag>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Body { get; set; }

        public bool MakePrivate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser Person { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}