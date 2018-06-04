using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NaccNigModels.PaymentSettings;
using NaccNigModels.PopUp;

namespace NaccNig.ViewModels
{
    public class ConfirmPaymentVM : RemitaPostVm
    {
        [Display(Name = "Student's Name")]
        //[Required(ErrorMessage = "Student's Name is required")]
        public string ActiveMemberId { get; set; }
        public string MemberName{ get; set; }

        [Display(Name = "Fees Type")]
        public PaymentType FeeCategory { get; set; }



        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        public RemitaPaymentType RemitaPaymentType { get; set; }
        public string MemberFeeType { get; set; }

        public PMode PaymentMode { get; set; }

        public List<Paylist> Paylist { get; set; }
    }

    public class SelectPaymentVm
    {
        [Display(Name = "Member's Name")]
        [Required(ErrorMessage = "Member's Name is required")]
        public string ActiveMemberId { get; set; }

        [Display(Name = "Fees Type")]
        public PaymentType FeeCategory { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

    }
}