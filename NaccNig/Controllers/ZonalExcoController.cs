using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NaccNig.Models;
using NaccNigModels.Executives;
using NaccNig.ViewModels;

namespace NaccNig.Controllers
{
    public class ZonalExcoController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: ZonalExco
        public ActionResult Index()
        {
            var zonalExco = db.ZonalExco.Include(z => z.ActiveMember).Include(z => z.Portfolio);
            return View(zonalExco.ToList());
        }

        // GET: ZonalExco/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZonalExco zonalExco = db.ZonalExco.Find(id);
            if (zonalExco == null)
            {
                return HttpNotFound();
            }
            return View(zonalExco);
        }

        // GET: ZonalExco/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname");
            ViewBag.PortfolioId = new SelectList(db.Portfolio, "Id", "PortfolioName");
            ViewBag.ZoneId = new SelectList(db.ZonalChapter, "Id", "ZoneName");
            return View();
        }

        // POST: ZonalExco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ZonalExco zonalExco)
        {
            if (ModelState.IsValid)
            {
                db.ZonalExco.Add(zonalExco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname", zonalExco.ActiveMemberId);
            ViewBag.PortfolioId = new SelectList(db.Portfolio, "Id", "PortfolioName", zonalExco.PortfolioId);
            ViewBag.ZoneId = new SelectList(db.ZonalChapter, "Id", "ZoneName", zonalExco.ZoneId);
            return View(zonalExco);
        }

        public ActionResult GetZone()
        {
            var zoneList = db.ZonalChapter.AsNoTracking().ToList();
            return View(zoneList);
        }
    
        

        public ActionResult ZonalExecutives(int? id)
        {
            var zonalChapter = db.ZonalExco.Include(x=>x.ZonalChapter).AsNoTracking().Where(x => x.ZoneId == id).ToList();
            return View(zonalChapter);
        }
        public ActionResult ExecutiveDetails(int? id)
        {
            StateExcoDetailVM model = new StateExcoDetailVM();
            var excoDetail = db.ZonalExco.Include(s => s.ActiveMember)
                                         .Include(s => s.Portfolio)
                                         .AsNoTracking()
                                        .FirstOrDefault(e => e.ZonalExcoId == id);

            if (excoDetail != null)
            {
                model.Fullname = excoDetail.ActiveMember.Fullname;
                model.StateCode = excoDetail.ActiveMember.StateCode;
                model.Gender = excoDetail.ActiveMember.Gender;
                model.Photo = excoDetail.ActiveMember.Photo;
                model.StateOfOrigin = excoDetail.ActiveMember.StateOfOrigin;
                model.Address = excoDetail.ActiveMember.Address;
                model.PortfolioName = excoDetail.Portfolio.PortfolioName;
                model.ChapterName = ViewBag.State;
                model.ZoneName = ViewBag.Zones;
                model.PhoneNumber = excoDetail.ActiveMember.PhoneNumber;
                model.ZoneId = excoDetail.ZoneId;
            }
            return View(model);
        }

        // GET: ZonalExco/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZonalExco zonalExco = db.ZonalExco.Find(id);
            if (zonalExco == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname", zonalExco.ActiveMemberId);
            ViewBag.PortfolioId = new SelectList(db.Portfolio, "Id", "PortfolioName", zonalExco.PortfolioId);
            ViewBag.ZoneId = new SelectList(db.ZonalChapter, "Id", "ZoneName", zonalExco.ZoneId);
            return View(zonalExco);
        }

        // POST: ZonalExco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ZonalExco zonalExco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zonalExco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname", zonalExco.ActiveMemberId);
            ViewBag.PortfolioId = new SelectList(db.Portfolio, "Id", "PortfolioName", zonalExco.PortfolioId);
            ViewBag.ZoneId = new SelectList(db.ZonalChapter, "Id", "ZoneName", zonalExco.ZoneId);
            return View(zonalExco);
        }

        // GET: ZonalExco/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZonalExco zonalExco = db.ZonalExco.Find(id);
            if (zonalExco == null)
            {
                return HttpNotFound();
            }
            return View(zonalExco);
        }

        // POST: ZonalExco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZonalExco zonalExco = db.ZonalExco.Find(id);
            db.ZonalExco.Remove(zonalExco);
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
