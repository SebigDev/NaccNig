using NaccNig.Models;
using NaccNig.Models.Blog;
using NaccNigModels.Blog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class EditUserViewModel
    {

        public ApplicationUser User { get; set; }


        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }

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

        [Display(Name = "Position")]
        public string Position { get; set; }
        [Required]
        public string YearServed { get; set; }

        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [Required]
        [Display(Name = "Call Up Number")]
        public string CallUpNumber { get; set; }
        public string StateChapter { get; set; }
        public string Lastname { get; set; }
       

        public IList<Role> Roles { get; set; }
    }
}