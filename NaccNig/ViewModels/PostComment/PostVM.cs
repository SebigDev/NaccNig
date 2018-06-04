using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels.PostComment
{
    public class PostVM
    {
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string PostAuthor { get; set; }
        public DateTime PostDate { get; set; }
        public List<CommentVM> Comments { get; set; }
    }
}