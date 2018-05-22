using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class MemberDashboardVM
    {
        public string ActiveMemberId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        public string Middlename { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        [Display(Name = "Sex")]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime Dob { get; set; }
        [Required]
        [Display(Name = "State Of Origin")]
        public string StateOfOrigin { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public string Fullname { get; set; }

        public int Age { get; set; }

        public string Photo { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "State Serving/Served")]
        public string StateOfDeployment { get; set; }
        [Required]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [Required]
        [Display(Name = "Call Up Number")]
        public string CallUpNumber { get; set; }
    }
}