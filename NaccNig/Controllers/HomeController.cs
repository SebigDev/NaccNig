using Microsoft.AspNet.Identity;
using NaccNig.Models;
using NaccNig.ViewModels;
using NaccNigModels.Members;
using NaccNigModels.PopUp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NaccNigModels.Controllers
{
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexPage()
        {
            return View();
        }

      [Authorize]
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

            var activerMemberList = await db.ActiveMember.AsNoTracking().ToListAsync();
            var pastMemberList = await db.PastMember.AsNoTracking().ToListAsync();


            int blogPost = await db.BlogList.AsNoTracking().CountAsync();

           

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
            ViewBag.BlogCount = blogPost;

            return View();
        }

        public async Task<ActionResult> ActiveMemberlist()
        {
            var actMem = await db.ActiveMember.AsNoTracking().ToListAsync();
            if (actMem == null)
            {
               return  ViewBag.Message = "No Registered Active Member(s) yet!";
            }

            ViewBag.ActMem = actMem;
           
            return View();
        }

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
       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}