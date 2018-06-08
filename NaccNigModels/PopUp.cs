using System;
using System.ComponentModel;
using System.Reflection;


namespace NaccNigModels.PopUp
{

   
    public enum Gender
    {
        Male, Female, Others
    }

    public enum State
    {
        Abia, Adamawa, AkwaIbom, Anambra, Bauchi, Bayelsa, Benue, Borno, CrossRiver, Delta, Ebonyi, Edo, Ekiti,
        Abuja, Gombe, Imo, Jigawa, Kaduna, Kano, Katsina, Kebbi, Kogi, Kwara, Lagos, Nasarawa, Niger, Ogun, Ondo, Osun,
        Oyo, Plateau, Rivers, Sokoto, Taraba, Yobe, Zamfara
    }
    public enum StateDeployed
    {
        Abia, Adamawa, AkwaIbom, Anambra, Bauchi, Bayelsa, Benue, Borno, CrossRiver, Delta, Ebonyi, Edo, Ekiti,
        Abuja, Gombe, Imo, Jigawa, Kaduna, Kano, Katsina, Kebbi, Kogi, Kwara, Lagos, Nasarawa, Niger, Ogun, Ondo, Osun,
        Oyo, Plateau, Rivers, Sokoto, Taraba, Yobe, Zamfara
    }

    public enum PaymentType
    {
        MembershipRegistration = 1,
        MonthlyDues
    }
    public enum RemitaPaymentType
    {
        MasterCard = 1, Visa, Verve, PocketMoni, POS, BANK_BRANCH, BANK_INTERNET, REMITA_PAY, RRRGEN
    }
    public enum PMode
    {
        Cash = 1, Cheque, Teller, OnlinePayment
    }

    public enum MemberCategory
    {
       
        [Description("Serving Corps Member")]
        ActiveMember = 1,
        [Description("Associate Member")]
        PastMember,
        [Description("Honorary Member")]
        ExecutiveMember

    }
  
}