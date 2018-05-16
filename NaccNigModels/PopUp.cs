using System;
using System.ComponentModel;
using System.Reflection;


namespace NaccNigModels.PopUp
{

    public enum PaymentOptionsName
    {
        [Description("Member Registration")]
        MemberRegistration = 1,
        [Description("Monthly Dues")]
        MonthlyDues,
        Donations
    }
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

    public enum MemberCategory
    {
        [Description("Active Member")]
        ActiveMember = 1,
        [Description("Past Member")]
        PastMember,
        [Description("Executive Member")]
        ExecutiveMember

    }
    public enum PaymentStatus
    {
        [Description("Not Paid")]
        NotPaid,
       Paid
    }
}