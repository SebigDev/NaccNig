using Microsoft.AspNet.Identity;
using NaccNig.Models;
using NaccNigModels.PopUp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            int paidMemberRegistration = await db.MemberRegistration.AsNoTracking().CountAsync(x => x.IsPaidRegistrationFee.Equals(true));
            int paidMonthlyDues = await db.MonthlyDues.AsNoTracking().CountAsync(x=>x.IsPaidMonthlyDues.Equals(true));
            int totalMember = totalPast + totalActive;

            var activerMemberList = await db.ActiveMember.AsNoTracking().ToListAsync();
            var pastMemberList = await db.PastMember.AsNoTracking().ToListAsync();

           

            double val1 = totalMale * 100;
            double val2 = totalFemale * 100;

            double malePercentage = Math.Round(val1 / totalMale, 2);
            double femalePercentage = Math.Round(val2 / totalFemale, 2);
            double totalPercentage = 100;

            ViewBag.TotalActiveMale = totalActiveMale;
            ViewBag.TotalPastMale = totalPastMale;
            ViewBag.TotalMale = totalMale;
            ViewBag.TotalFemale = totalFemale;
            ViewBag.TotalActive = totalActive;
            ViewBag.TotalPast = totalPast;
            ViewBag.MalePercentage = malePercentage;
            ViewBag.FemalePercentage = femalePercentage;
            ViewBag.PaidMemberRegistration = paidMemberRegistration;
            ViewBag.PaidMonthlyDues = paidMonthlyDues;
            ViewBag.TotalMember = totalMember;
            ViewBag.TotalPercentage = totalPercentage;
            ViewBag.ActiveMemberList = activerMemberList;
            ViewBag.PastMemberList = pastMemberList;

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