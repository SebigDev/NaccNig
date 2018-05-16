using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NaccNigModels.Members;

namespace NaccNigModels.Blog
{

    public class BlogCategory
    {
        [Key]
        public int BlogCategoryId { get; set; }
        public string CategoryName { get; set; }
    }


    public abstract class Blog
    {
        public string BlogTitle { get; set; }
        public string PublishedDate
        {
            get
            {
                return DateTime.Now.ToLongDateString();
            }
        }
        public string Author
        {
            get
            {
                return "Admin";
            }
           
        }

    }

    public class BlogList : Blog
    {
        [Key]
        public int BlogListId { get; set; } 
        public string Description { get; set; }

        public int BlogCategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }

    }

    public class BlogPost
    {
        public int Id { get; set; }
        public string DetailDescription { get; set; }
        public int BlogListId { get; set; }
        public virtual BlogList BlogList { get; set; }
    }
}
