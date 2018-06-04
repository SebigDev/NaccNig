using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels.PostComment
{
    public class CommentVM
    {
        public string Comment { get; set; }
        public string CommentAuthor { get; set; }
        public DateTime CommentDate { get; set; }
    }
}