using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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


            public byte[] Passport { get; set; }
            [Required]
            [Display(Name = "Phone Number")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
        }
    }