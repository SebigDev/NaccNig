using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaccNig.Models.Blog
{
    public class Comments
    {

        private ICollection<Article> comment;

        public Comments()
        {
            this.comment = new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }
        public string CommentDetail { get; set; }


        public virtual ICollection<Article> Articles
        {
            get { return this.comment; }
            set { this.comment = value; }
        }
    }
}