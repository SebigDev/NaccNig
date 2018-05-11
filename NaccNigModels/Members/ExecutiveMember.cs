
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NaccNigModels.Members
{
    public class ExecutiveMember
    {
        public string ExecutiveMemberId { get; set; }
        [Display(Name = "Post")]
        public string Position { get; set; }

    }
}
