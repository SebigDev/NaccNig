using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NaccNig.Models;
using NaccNig.ViewModels;
using NaccNigModels.Members;
using NaccNigModels.PopUp;

namespace NaccNig.Controllers
{
    public class PastMembersController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: PastMembers
        public ActionResult Index()
        {
            return View(db.PastMember.ToList());
        }


        public async Task<ActionResult> Dashboard(MemberDashboardVM model)
        {
            var userId = User.Identity.GetUserId();
            var pastUser = await db.PastMember.AsNoTracking().SingleOrDefaultAsync(p=>p.PastMemberId.Equals(userId));
            if(pastUser != null)
            {
                model.Firstname = pastUser.Firstname;
                model.Middlename = pastUser.Middlename;
                model.Surname = pastUser.Surname;
                model.Age = pastUser.Age;
                model.Address = pastUser.Address;
                model.Gender = pastUser.Gender;
                model.PhoneNumber = pastUser.PhoneNumber;
                model.StateOfDeployment = pastUser.StateServed;
                model.Dob = pastUser.Dob;

            }

            return View(model);

        }
        // GET: PastMembers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastMember pastMember = db.PastMember.Find(id);
            if (pastMember == null)
            {
                return HttpNotFound();
            }
            return View(pastMember);
        }

        public ActionResult ProfileUpdate()
        {
            var myGender = from Gender s in Enum.GetValues(typeof(Gender))
                           select new { ID = s, Name = s.ToString() };
            var myState = from State s in Enum.GetValues(typeof(State))
                          select new { ID = s, Name = s.ToString() };
            var depState = from StateDeployed s in Enum.GetValues(typeof(StateDeployed))
                           select new { ID = s, Name = s.ToString() };
            ViewBag.Gender = new SelectList(myGender, "Name", "Name");
            ViewBag.StateOfOrigin = new SelectList(myState, "Name", "Name");
            ViewBag.StateOfDeployment = new SelectList(depState, "Name", "Name");
            return View();
        }

        // POST: PastMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProfileUpdate(PastMember pastMember)
        {
            var userId = User.Identity.GetUserId();
            pastMember.PastMemberId = userId;
            if (ModelState.IsValid)
            {
                db.PastMember.Add(pastMember);
                await db.SaveChangesAsync();
                ViewBag.Message = $"Hello {User.Identity.GetUserName()}; Your Profile Updated Successfully.";
                return RedirectToAction("Dashboard");
            }
            var myGender = from Gender s in Enum.GetValues(typeof(Gender))
                           select new { ID = s, Name = s.ToString() };
            var myState = from State s in Enum.GetValues(typeof(State))
                          select new { ID = s, Name = s.ToString() };
            var depState = from StateDeployed s in Enum.GetValues(typeof(StateDeployed))
                           select new { ID = s, Name = s.ToString() };
            ViewBag.Gender = new SelectList(myGender, "Name", "Name");
            ViewBag.StateOfOrigin = new SelectList(myState, "Name", "Name");
            ViewBag.StateOfDeployment = new SelectList(depState, "Name", "Name");

            return View(pastMember);
        }
        // GET: PastMembers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastMember pastMember = db.PastMember.Find(id);
            if (pastMember == null)
            {
                return HttpNotFound();
            }
            return View(pastMember);
        }

        // POST: PastMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PastMemberId,StateServed,Firstname,Middlename,Surname,Gender,Dob,StateOfOrigin,Address,Passport,PhoneNumber")] PastMember pastMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pastMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pastMember);
        }

        // GET: PastMembers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastMember pastMember = db.PastMember.Find(id);
            if (pastMember == null)
            {
                return HttpNotFound();
            }
            return View(pastMember);
        }

        // POST: PastMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PastMember pastMember = db.PastMember.Find(id);
            db.PastMember.Remove(pastMember);
            db.SaveChanges();
            return RedirectToAction("PastMemberList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
