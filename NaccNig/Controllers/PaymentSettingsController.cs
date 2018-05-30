using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NaccNig.Models;
using NaccNigModels.Payment;

namespace NaccNig.Controllers
{
    public class PaymentSettingsController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: PaymentSettings
        public ActionResult Index()
        {
            var paymentSetting = db.PaymentSetting.Include(p => p.ActiveMember);
            return View(paymentSetting.ToList());
        }

        // GET: PaymentSettings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentSetting paymentSetting = db.PaymentSetting.Find(id);
            if (paymentSetting == null)
            {
                return HttpNotFound();
            }
            return View(paymentSetting);
        }

        // GET: PaymentSettings/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment");
            return View();
        }

        // POST: PaymentSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,PaymentCategoryId,AmountId,ActiveMemberId")] PaymentSetting paymentSetting)
        {
            if (ModelState.IsValid)
            {
                db.PaymentSetting.Add(paymentSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", paymentSetting.ActiveMemberId);
            return View(paymentSetting);
        }

        // GET: PaymentSettings/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentSetting paymentSetting = db.PaymentSetting.Find(id);
            if (paymentSetting == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", paymentSetting.ActiveMemberId);
            return View(paymentSetting);
        }

        // POST: PaymentSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,PaymentCategoryId,AmountId,ActiveMemberId")] PaymentSetting paymentSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateOfDeployment", paymentSetting.ActiveMemberId);
            return View(paymentSetting);
        }

        // GET: PaymentSettings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentSetting paymentSetting = db.PaymentSetting.Find(id);
            if (paymentSetting == null)
            {
                return HttpNotFound();
            }
            return View(paymentSetting);
        }

        // POST: PaymentSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PaymentSetting paymentSetting = db.PaymentSetting.Find(id);
            db.PaymentSetting.Remove(paymentSetting);
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
