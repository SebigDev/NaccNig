using NaccNig.Models;
using NaccNig.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using NaccNigModels.PopUp;

namespace NaccNig.BusinessLogic
{
    public class QueryCommand
    {
        private readonly NaccNigDbContext _db;
        


        public QueryCommand()
        {
            _db = new NaccNigDbContext();
           
        }


        public int ConvertToKobo(int value)
        {
            return value * 100;
        }

        public int ConvertToNaira(int value)
        {
            return value / 100;
        }

        public string HashRemitaRequest(string merchantId, string serviceTypeId, string orderId, string amount, string responseUrl, string apiKey)
        {
            string hash_string = merchantId + serviceTypeId + orderId + amount + responseUrl + apiKey;
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            return BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
        }

        public string HashRemitedValidate(string orderID, string apiKey, string merchantId)
        {
            string hash_string = orderID + apiKey + merchantId;
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            return BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
        }

        public string HashRemitedRePost(string merchantId, string rrr, string apiKey)
        {
            string hash_string = merchantId + rrr + apiKey;
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            return BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
        }

        public string HashRrrQuery(string rrr, string apiKey, string merchantId)
        {
            string hash_string = rrr + apiKey + merchantId;
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            return BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
        }


        public BaseVm GetPaymentStatus()
        {
            var model = new BaseVm();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var checkMember = _db.ActiveMember.AsNoTracking()
                                              .FirstOrDefault(x =>x.ActiveMemberId == userId);




            if (checkMember != null)
            {

                int memReg = Convert.ToInt32(PaymentType.MembershipRegistration);
                int monDues = Convert.ToInt32(PaymentType.MonthlyDues);

                var memberFee = _db.MembershipFee.Where(x => x.ActiveMemberId==checkMember.ActiveMemberId
                                                                 && x.Status==true
                                                                  && x.FeeCategory==PaymentType.MembershipRegistration)
                                                                      .ToList();


                var monthDues = _db.MembershipFee.Where(x => x.ActiveMemberId == checkMember.ActiveMemberId
                                                                 && x.Status==true
                                                                  && x.FeeCategory == PaymentType.MonthlyDues)
                                                                      .ToList();

            }

            return model;
        }

        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}