using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NaccNig.Models.Blog;
using NaccNigModels.Blog;
using NaccNigModels.Members;
using NaccNigModels.Payment;
using NaccNigModels.Structures;

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
        public DbSet<PaymentSetting> PaymentSetting { get; set; }
        public DbSet<PaymentCategory> PaymentCategory { get; set; }
        public DbSet<Amount> Amount { get; set; }
       
        public virtual IDbSet<Article> Articles { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }
        public virtual IDbSet<Comments> Comments { get; set; }



    }
}