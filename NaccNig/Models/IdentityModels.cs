using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NaccNigModels.Members;
using NaccNigModels.Payments;


namespace NaccNig.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class NaccNigDbContext : IdentityDbContext<ApplicationUser>
    {
        public NaccNigDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static NaccNigDbContext Create()
        {
            return new NaccNigDbContext();
        }

        public DbSet<ActiveMember> ActiveMember { get; set; }
        public DbSet<PastMember> PastMember { get; set; }
        public DbSet<ExecutiveMember> ExecutiveMember { get; set; }
        public DbSet<PaymentOptions> PaymentOptions { get; set; }
        public DbSet<MemberRegistration> MemberRegistration { get; set; }
        public DbSet<Donations> Donations { get; set; }
        public DbSet<MonthlyDues> MonthlyDues { get; set; }

    }
}