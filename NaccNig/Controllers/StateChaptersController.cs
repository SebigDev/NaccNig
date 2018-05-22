using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NaccNig.Models;
using NaccNigModels.Structures;

namespace NaccNig.Controllers
{
    public class StateChaptersController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: StateChapters
        public ActionResult Index()
        {
            return View(db.StateChapter.ToList());
        }

        // GET: StateChapters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateChapter stateChapter = db.StateChapter.Find(id);
            if (stateChapter == null)
            {
                return HttpNotFound();
            }
            return View(stateChapter);
        }

        // GET: StateChapters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StateChapters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StateChapId,StateChapterName,ProId")] StateChapter stateChapter)
        {
            if (ModelState.IsValid)
            {
                db.StateChapter.Add(stateChapter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stateChapter);
        }

        // GET: StateChapters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateChapter stateChapter = db.StateChapter.Find(id);
            if (stateChapter == null)
            {
                return HttpNotFound();
            }
            return View(stateChapter);
        }

        // POST: StateChapters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StateChapId,StateChapterName,ProId")] StateChapter stateChapter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stateChapter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stateChapter);
        }

        // GET: StateChapters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateChapter stateChapter = db.StateChapter.Find(id);
            if (stateChapter == null)
            {
                return HttpNotFound();
            }
            return View(stateChapter);
        }

        // POST: StateChapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StateChapter stateChapter = db.StateChapter.Find(id);
            db.StateChapter.Remove(stateChapter);
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
