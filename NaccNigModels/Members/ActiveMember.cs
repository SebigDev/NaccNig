using NaccNigModels.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NaccNigModels.Members
{
    public class ActiveMember : Person
    {
        public string ActiveMemberId { get; set; }

        [Display(Name = "State Posted")]
        public string StateOfDeployment { get; set; }
        [Required]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [Required]
        [Display(Name = "Call Up Number")]
        public string CallUpNumber { get; set; }
        public virtual ICollection<MemberRegistration> MemberRegistration { get; set; }
        public virtual ICollection<Donations> Donations { get; set; }
        public virtual ICollection<MonthlyDues> MonthlyDues { get; set; }
    }
}
