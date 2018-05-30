
using NaccNigModels.Members;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NaccNigModels.Payment
{
    public class PaymentSetting
    {
        [Key]
        public string PaymentId { get; set; }

        public int PaymentCategoryId { get; set; }
        public int AmountId { get; set; }
        public virtual Amount Amount { get; set; }

        public string ActiveMemberId { get; set; }
        public virtual ActiveMember ActiveMember { get; set; }

    }

    public class PaymentCategory
    {
        [Key]
        public int PaymentCategoryId { get; set; }
        public string CategoryName { get; set; }

        public static IQueryable<PaymentCategory> GetPaymentCategoryList()
        {
            return new List<PaymentCategory>
            {
                new PaymentCategory{PaymentCategoryId = 1, CategoryName = "Membership Registration"},
                new PaymentCategory{PaymentCategoryId = 2, CategoryName ="Monthly Dues"}
            }.AsQueryable();
        } 
    }


    public class Amount
    {
        [Key]
        public int PriceId { get; set; }
        public int Price { get; set; }
        public int PaymentCategoryId { get; set; }


        public static IQueryable<Amount> GetAmountList()
        {
            return new List<Amount>
            {
                new Amount{PriceId = 1, Price = 1250, PaymentCategoryId = 1},
                new Amount{PriceId = 2, Price = 450, PaymentCategoryId = 2}
            }.AsQueryable();
        }
    }
}
