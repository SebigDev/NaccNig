using System.ComponentModel.DataAnnotations;

namespace NaccNigModels.Members
{
    public class ActiveMember : Person
    {
        public string ActiveMemberId { get; set; }

     
        [Required]
       
        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [Required]
        [Display(Name = "Call Up Number")]
        public string CallUpNumber { get; set; }

    }
}
