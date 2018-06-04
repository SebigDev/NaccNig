using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NaccNig.Models.BlogPost;

namespace NaccNig.Models
{

   public class Category
    {
        private ICollection<Post> post;

        public Category()
        {
            this.post = new HashSet<Post>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        //[Index(IsUnique = true)]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}