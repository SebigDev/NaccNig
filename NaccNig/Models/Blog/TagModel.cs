﻿
using NaccNig.Models.Blog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NaccNigModels.Blog
{
    public class Tag
    {
        private ICollection<Article> articles;

        public Tag()
        {
            this.articles = new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }


       // [Index(IsUnique =true)]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }
    }
}