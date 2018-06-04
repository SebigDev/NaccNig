using NaccNig.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NaccNig.Controllers
{
    [Authorize]
    public class HomeController : Controller

    {
        private NaccNigDbContext db;

        public HomeController()
        {
            db = new NaccNigDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
       

      [Authorize(Roles =  RoleName.Admin)]
        public async Task<ActionResult> Dashboard()
        {
            if (!User.IsInRole(RoleName.Admin))
            {
                return HttpNotFound();
            }
            int totalActiveMale = await db.ActiveMember.AsNoTracking().CountAsync(x => x.Gender.ToString().Equals("Male"));
            int totalPastMale = await db.PastMember.AsNoTracking().CountAsync(x => x.Gender.ToString().Equals("Male"));
            int totalActiveFemale = await db.ActiveMember.AsNoTracking().CountAsync(x => x.Gender.ToString().Equals("Female"));
            int totalPastFemale = await db.PastMember.AsNoTracking().CountAsync(x => x.Gender.ToString().Equals("Female"));
            int totalMale = totalActiveMale + totalPastMale;
            int totalFemale = totalActiveFemale + totalPastFemale;
            int totalActive = totalActiveMale + totalActiveFemale;
            int totalPast = totalPastMale + totalPastFemale;
            int totalMember = totalPast + totalActive;
            int blog = await db.Post.AsNoTracking().CountAsync();
            //int newUser = await db.ActiveMember.AsNoTracking().CountAsync(x=>x.);

            var activerMemberList = await db.ActiveMember.AsNoTracking().ToListAsync();
            var pastMemberList = await db.PastMember.AsNoTracking().ToListAsync();
        

            double valueA = totalMale * 100;
            double valueB= totalFemale * 100;

            double malePercentage = Math.Round(valueA / totalMale, 2);
            double femalePercentage = Math.Round(valueB/ totalFemale, 2);
            double totalPercentage = 100;

            ViewBag.TotalActiveMale = totalActiveMale;
            ViewBag.TotalPastMale = totalPastMale;
            ViewBag.TotalMale = totalMale;
            ViewBag.TotalFemale = totalFemale;
            ViewBag.TotalActive = totalActive;
            ViewBag.TotalPast = totalPast;
            ViewBag.MalePercentage = malePercentage;
            ViewBag.FemalePercentage = femalePercentage;
            ViewBag.TotalPercentage = totalPercentage;
            ViewBag.ActiveMemberList = activerMemberList;
            ViewBag.PastMemberList = pastMemberList;
            ViewBag.TotalMember = totalMember;
            ViewBag.Blog = blog;
        
            return View();
        }
        [Authorize(Roles = RoleName.Admin)]
        public async Task<ActionResult> ActiveMemberlist()
        {
            var actMem = await db.ActiveMember.AsNoTracking().Include(x=>x.StateChapter).ToListAsync();
            if (actMem == null)
            {
               return  ViewBag.Message = "No Registered Active Member(s) yet!";
            }

            ViewBag.ActMem = actMem;
           
            return View();
        }
        [Authorize(Roles = RoleName.Admin)]
        public async Task<ActionResult> PastMemberlist()
        {
            var pastMem = await db.PastMember.AsNoTracking().ToListAsync();
            if(pastMem == null)
            {
               ViewBag.Message = "No Registered Past Member(s) yet!";
                
            }

            ViewBag.PastMem = pastMem;

            return View();
        }
       
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}