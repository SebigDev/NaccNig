using NaccNigModels.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class BlogPostVM
    {

        public int Id { get; set; }
        public int BlogListId { get; set; }
        public string DetailDescription { get; set; }
        public virtual BlogList BlogList { get; set; }
    }
}