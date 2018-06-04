﻿using NaccNigModels.Members;
using NaccNigModels.PopUp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaccNig.ViewModels
{
    public class MembershipFeeVM
    {

        public int MembershipRegistrationId { get; set; }

        [Display(Name = "Member's Name")]
        [Required(ErrorMessage = "Member's Name is required")]
        public string ActiveMemberId { get; set; }
        public string ReferenceNo { get; set; }

        public PaymentType FeeCategory { get; set; }
        public string MemberFeeType { get; set; }

        //[Index(IsUnique = true)]
        [MaxLength(50)]
        [Required]
        public string OrderId { get; set; }

        [Display(Name = "Amount Paid")]
        public decimal PaidFee { get; set; }


        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }



        [Display(Name = "Date of Payment")]
        public DateTime Date { get; set; }


        [Display(Name = "Fee Status")]
        public bool Status { get; set; }

        public string PaymentStatus { get; set; }

        public virtual ActiveMember ActiveMember { get; set; }
    }



}
