using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NaccNig.Models;
using NaccNigModels.Payments;

namespace NaccNigModels.Controllers
{
    public class MemberRegistrationsController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: MemberRegistrations
        public ActionResult Index()
        {
            var memberRegistration = db.MemberRegistration.Include(m => m.ActiveMember);
            return View(memberRegistration.ToList());
        }

        // GET: MemberRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberRegistration memberRegistration = db.MemberRegistration.Find(id);
            if (memberRegistration == null)
            {
                return HttpNotFound();
            }
            return View(memberRegistration);
        }

        // GET: MemberRegistrations/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment");
            return View();
        }

        // POST: MemberRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberRegistrationId,IsPaidRegistrationFee,PaymentStatus,ActiveMemberId")] MemberRegistration memberRegistration)
        {
            if (ModelState.IsValid)
            {
                db.MemberRegistration.Add(memberRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", memberRegistration.ActiveMemberId);
            return View(memberRegistration);
        }

        // GET: MemberRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberRegistration memberRegistration = db.MemberRegistration.Find(id);
            if (memberRegistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", memberRegistration.ActiveMemberId);
            return View(memberRegistration);
        }

        // POST: MemberRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberRegistrationId,IsPaidRegistrationFee,PaymentStatus,ActiveMemberId")] MemberRegistration memberRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", memberRegistration.ActiveMemberId);
            return View(memberRegistration);
        }

        // GET: MemberRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberRegistration memberRegistration = db.MemberRegistration.Find(id);
            if (memberRegistration == null)
            {
                return HttpNotFound();
            }
            return View(memberRegistration);
        }

        // POST: MemberRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberRegistration memberRegistration = db.MemberRegistration.Find(id);
            db.MemberRegistration.Remove(memberRegistration);
            db.SaveChanges();
            return RedirectToAction("Index");
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
