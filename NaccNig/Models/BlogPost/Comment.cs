﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaccNig.Models.BlogPost
{
    public class Comment
    {

        public int ID { get; set; }
        public int PostID { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }

        public virtual Post Post { get; set; }
    }
}