using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NaccNigModels.Members;
using NaccNigModels.Structures;
using NaccNig.Models.BlogPost;
using NaccNigModels.PaymentSettings;

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
        public DbSet<StateChapter> StateChapter { get; set; }
        public DbSet<Zone> Zone { get; set; }
       
        public DbSet<Post> Post { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Tag>Tag { get; set; }

        //Remitta
        public DbSet<MembershipFee> MembershipFee { get; set; }

        //public DbSet<FeeCategory> FeeCategory { get; set; }

        public DbSet<MemberFeeType> MemberFeeType { get; set; }

        public DbSet<RemitaPaymentLog> RemitaPaymentLog { get; set; }

        
    }
}