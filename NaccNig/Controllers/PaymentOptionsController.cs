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
    public class PaymentOptionsController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: PaymentOptions
        public ActionResult Index()
        {
            return View(db.PaymentOptions.ToList());
        }

        // GET: PaymentOptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentOptions paymentOptions = db.PaymentOptions.Find(id);
            if (paymentOptions == null)
            {
                return HttpNotFound();
            }
            return View(paymentOptions);
        }

        // GET: PaymentOptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentOptionsId,PaymentOptionsName")] PaymentOptions paymentOptions)
        {
            if (ModelState.IsValid)
            {
                db.PaymentOptions.Add(paymentOptions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentOptions);
        }

        // GET: PaymentOptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentOptions paymentOptions = db.PaymentOptions.Find(id);
            if (paymentOptions == null)
            {
                return HttpNotFound();
            }
            return View(paymentOptions);
        }

        // POST: PaymentOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentOptionsId,PaymentOptionsName")] PaymentOptions paymentOptions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentOptions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentOptions);
        }

        // GET: PaymentOptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentOptions paymentOptions = db.PaymentOptions.Find(id);
            if (paymentOptions == null)
            {
                return HttpNotFound();
            }
            return View(paymentOptions);
        }

        // POST: PaymentOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentOptions paymentOptions = db.PaymentOptions.Find(id);
            db.PaymentOptions.Remove(paymentOptions);
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
