using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NaccNig.Models;
using NaccNig.ViewModels;
using NaccNigModels.Executives;
using NaccNigModels.Structures;

namespace NaccNig.Controllers
{
    public class StateExcoController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: StateExco
        public ActionResult Index()
        {
            var stateExco = db.StateExco.Include(s => s.ActiveMember).Include(s => s.Portfolio);
            return View(stateExco.ToList());
        }

        // GET: StateExco/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateExco stateExco = db.StateExco.Find(id);
            if (stateExco == null)
            {
                return HttpNotFound();
            }
            return View(stateExco);
        }


        public ActionResult StateExecutives()
        {
            var excoState = db.StateExco.Include(s => s.ActiveMember).Include(s => s.Portfolio).ToList();
            
            return View(excoState);
        }
        public ActionResult StateExcoDetail(int? id)
        {
            StateExcoDetailVM model = new StateExcoDetailVM();
            var excoDetail = db.StateExco.Include(s => s.ActiveMember)
                                         .Include(s => s.Portfolio)
                                         .AsNoTracking()
                                        .FirstOrDefault(e=>e.StateExcoId == id);

            if(excoDetail != null)
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
            }
            return View(model);

        }

        // GET: StateExco/Create
        public ActionResult Create()
        {
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname");
            ViewBag.PortfolioId = new SelectList(db.Portfolio, "Id", "PortfolioName");
            return View();
        }

        // POST: StateExco/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StateExcoId,PortfolioId,ActiveMemberId")] StateExco stateExco)
        {
            if (ModelState.IsValid)
            {
                db.StateExco.Add(stateExco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname", stateExco.ActiveMemberId);
            ViewBag.PortfolioId = new SelectList(db.Portfolio, "Id", "PortfolioName", stateExco.PortfolioId);
            return View(stateExco);
        }

        // GET: StateExco/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateExco stateExco = db.StateExco.Find(id);
            if (stateExco == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname", stateExco.ActiveMemberId);
            ViewBag.PortfolioId = new SelectList(db.Portfolio, "Id", "PortfolioName", stateExco.PortfolioId);
            return View(stateExco);
        }

        // POST: StateExco/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StateExcoId,PortfolioId,ActiveMemberId")] StateExco stateExco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stateExco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname", stateExco.ActiveMemberId);
            ViewBag.PortfolioId = new SelectList(db.Portfolio, "Id", "PortfolioName", stateExco.PortfolioId);
            return View(stateExco);
        }

        // GET: StateExco/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateExco stateExco = db.StateExco.Find(id);
            if (stateExco == null)
            {
                return HttpNotFound();
            }
            return View(stateExco);
        }

        // POST: StateExco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StateExco stateExco = db.StateExco.Find(id);
            db.StateExco.Remove(stateExco);
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
