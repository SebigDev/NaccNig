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
    public class DonationsController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: Donations
        public ActionResult Index()
        {
            var donations = db.Donations.Include(d => d.ActiveMember);
            return View(donations.ToList());
        }

        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donations donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            return View(donations);
        }

        // GET: Donations/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonationsId,IsMadeDonations,PaymentStatus,ActiveMemberId")] Donations donations)
        {
            if (ModelState.IsValid)
            {
                db.Donations.Add(donations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", donations.ActiveMemberId);
            return View(donations);
        }

        // GET: Donations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donations donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", donations.ActiveMemberId);
            return View(donations);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationsId,IsMadeDonations,PaymentStatus,ActiveMemberId")] Donations donations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", donations.ActiveMemberId);
            return View(donations);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donations donations = db.Donations.Find(id);
            if (donations == null)
            {
                return HttpNotFound();
            }
            return View(donations);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donations donations = db.Donations.Find(id);
            db.Donations.Remove(donations);
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
