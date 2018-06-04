using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NaccNig.Models;
using NaccNigModels.PaymentSettings;
using NaccNigModels.PopUp;

namespace NaccNig.Controllers
{
    public class MemberFeeTypesController : Controller
    {
        private NaccNigDbContext db;

        public MemberFeeTypesController()
        {
            db = new NaccNigDbContext();
        }
        // GET: MemberFeeTypes
        public ActionResult Index()
        {

            return View(db.MemberFeeType.ToList());
        }

        // GET: MemberFeeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberFeeType memberFeeType = db.MemberFeeType.Find(id);
            if (memberFeeType == null)
            {
                return HttpNotFound();
            }
            return View(memberFeeType);
        }

        // GET: MemberFeeTypes/Create
        public ActionResult Create()
        {
            var paytype = from PaymentType p in Enum.GetValues(typeof(PaymentType))
                          select new { ID = p, Name = p.ToString() };

            ViewBag.FeeCategory = new SelectList(paytype, "Name", "Name");
            return View();
        }

        // POST: MemberFeeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberFeeTypeId,FeeCategory,FeeName,Amount,AmountInWords,Description")] MemberFeeType memberFeeType)
        {
            if (ModelState.IsValid)
            {
                db.MemberFeeType.Add(memberFeeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var paytype = from PaymentType p in Enum.GetValues(typeof(PaymentType))
                          select new { ID = p, Name = p.ToString() };

            ViewBag.FeeCategory = new SelectList(paytype, "Name", "Name");

            return View(memberFeeType);
        }

        // GET: MemberFeeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberFeeType memberFeeType = db.MemberFeeType.Find(id);
            if (memberFeeType == null)
            {
                return HttpNotFound();
            }
            var paytype = from PaymentType p in Enum.GetValues(typeof(PaymentType))
                          select new { ID = p, Name = p.ToString() };

            ViewBag.FeeCategory = new SelectList(paytype, "Name", "Name");
            return View(memberFeeType);
        }

        // POST: MemberFeeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberFeeTypeId,FeeCategory,FeeName,Amount,AmountInWords,Description")] MemberFeeType memberFeeType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberFeeType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var paytype = from PaymentType p in Enum.GetValues(typeof(PaymentType))
                          select new { ID = p, Name = p.ToString() };

            ViewBag.FeeCategory = new SelectList(paytype, "Name", "Name");
            return View(memberFeeType);
        }

        // GET: MemberFeeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberFeeType memberFeeType = db.MemberFeeType.Find(id);
            if (memberFeeType == null)
            {
                return HttpNotFound();
            }
            return View(memberFeeType);
        }

        // POST: MemberFeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberFeeType memberFeeType = db.MemberFeeType.Find(id);
            db.MemberFeeType.Remove(memberFeeType);
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
