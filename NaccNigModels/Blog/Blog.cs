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
        [DataType(DataType.Date)]
        public DateTime DatePublished { get; set; }
        public string PublishedDate
        {
            get
            {
                return DatePublished.ToLongDateString();
            }
          }
        public string Author
        {
            get
            {
                return "Admin";
            }
           
        }
        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }

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

    public class BlogComment
    {
        [Key]
        public int CommentId { get; set; }
        public string ActiveMemberId { get; set; }
        public string PastMemberId { get; set; }
        public ActiveMember ActiveMember { get; set; }
        public PastMember PastMember { get; set; }
        public string Comment { get; set; }
        public int BlogListId { get; set; }
        public BlogList BlogList { get; set; }

        [DataType(DataType.Date)]
        public DateTime CommentTime { get; set; }
        public string GetCommentTime
        {
            get
            {
                return CommentTime.ToShortTimeString();
            }
        }

    }
}
