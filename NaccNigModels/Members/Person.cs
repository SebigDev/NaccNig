using NaccNigModels.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace NaccNigModels.Members
{

    public abstract class Person
    {
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
        [Display(Name ="Current Province")]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        [Display(Name = "State Chapter")]
        public int StateChapterId { get; set; }
        public virtual StateChapter StateChapter { get; set; }
        [Display(Name = "Zone")]
        public int ZoneId { get; set; }
        public virtual Zone Zone { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public string Fullname => Firstname + " " + Middlename + " " + Surname;

        public int Age
        {
            get
            {
                var personAge = DateTime.Now.Subtract(Dob);
                return personAge.Days / 365;
            }
        }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Upload A Photo")]
        public string Photo { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }

}