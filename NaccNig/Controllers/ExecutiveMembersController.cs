using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NaccNigModels.Members;
using NaccNig.Models;

namespace NaccNigModels.Controllers
{
    public class ExecutiveMembersController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: ExecutiveMembers
        public ActionResult Index()
        {
            return View(db.ExecutiveMember.ToList());
        }

        // GET: ExecutiveMembers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutiveMember executiveMember = db.ExecutiveMember.Find(id);
            if (executiveMember == null)
            {
                return HttpNotFound();
            }
            return View(executiveMember);
        }

        // GET: ExecutiveMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExecutiveMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExecutiveMemberId,Position")] ExecutiveMember executiveMember)
        {
            if (ModelState.IsValid)
            {
                db.ExecutiveMember.Add(executiveMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(executiveMember);
        }

        // GET: ExecutiveMembers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutiveMember executiveMember = db.ExecutiveMember.Find(id);
            if (executiveMember == null)
            {
                return HttpNotFound();
            }
            return View(executiveMember);
        }

        // POST: ExecutiveMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExecutiveMemberId,Position")] ExecutiveMember executiveMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(executiveMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(executiveMember);
        }

        // GET: ExecutiveMembers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutiveMember executiveMember = db.ExecutiveMember.Find(id);
            if (executiveMember == null)
            {
                return HttpNotFound();
            }
            return View(executiveMember);
        }

        // POST: ExecutiveMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ExecutiveMember executiveMember = db.ExecutiveMember.Find(id);
            db.ExecutiveMember.Remove(executiveMember);
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
