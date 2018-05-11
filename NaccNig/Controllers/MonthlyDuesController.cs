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
    public class MonthlyDuesController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: MonthlyDues
        public ActionResult Index()
        {
            var monthlyDues = db.MonthlyDues.Include(m => m.ActiveMember);
            return View(monthlyDues.ToList());
        }

        // GET: MonthlyDues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyDues monthlyDues = db.MonthlyDues.Find(id);
            if (monthlyDues == null)
            {
                return HttpNotFound();
            }
            return View(monthlyDues);
        }

        // GET: MonthlyDues/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment");
            return View();
        }

        // POST: MonthlyDues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MonthlyDuesId,IsPaidMonthlyDues,PaymentStatus,ActiveMemberId")] MonthlyDues monthlyDues)
        {
            if (ModelState.IsValid)
            {
                db.MonthlyDues.Add(monthlyDues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", monthlyDues.ActiveMemberId);
            return View(monthlyDues);
        }

        // GET: MonthlyDues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyDues monthlyDues = db.MonthlyDues.Find(id);
            if (monthlyDues == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", monthlyDues.ActiveMemberId);
            return View(monthlyDues);
        }

        // POST: MonthlyDues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MonthlyDuesId,IsPaidMonthlyDues,PaymentStatus,ActiveMemberId")] MonthlyDues monthlyDues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyDues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", monthlyDues.ActiveMemberId);
            return View(monthlyDues);
        }

        // GET: MonthlyDues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyDues monthlyDues = db.MonthlyDues.Find(id);
            if (monthlyDues == null)
            {
                return HttpNotFound();
            }
            return View(monthlyDues);
        }

        // POST: MonthlyDues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthlyDues monthlyDues = db.MonthlyDues.Find(id);
            db.MonthlyDues.Remove(monthlyDues);
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
