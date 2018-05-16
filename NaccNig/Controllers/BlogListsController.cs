using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NaccNig.Models;
using NaccNigModels.Blog;

namespace NaccNig.Controllers
{
    public class BlogListsController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();

        // GET: BlogLists
        public ActionResult Index()
        {
            var blogList = db.BlogList.Include(b => b.BlogCategory);
            return View(blogList.ToList());
        }

        // GET: BlogLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogList blogList = db.BlogList.Find(id);
            if (blogList == null)
            {
                return HttpNotFound();
            }
            return View(blogList);
        }

        // GET: BlogLists/Create
        public ActionResult Create()
        {
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategory, "BlogCategoryId", "CategoryName");
            return View();
        }

        // POST: BlogLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogListId,Description,BlogCategoryId,BlogTitle")] BlogList blogList)
        {
            if (ModelState.IsValid)
            {
                db.BlogList.Add(blogList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogCategoryId = new SelectList(db.BlogCategory, "BlogCategoryId", "CategoryName", blogList.BlogCategoryId);
            return View(blogList);
        }

        // GET: BlogLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogList blogList = db.BlogList.Find(id);
            if (blogList == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategory, "BlogCategoryId", "CategoryName", blogList.BlogCategoryId);
            return View(blogList);
        }

        // POST: BlogLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogListId,Description,BlogCategoryId,BlogTitle")] BlogList blogList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategory, "BlogCategoryId", "CategoryName", blogList.BlogCategoryId);
            return View(blogList);
        }

        // GET: BlogLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogList blogList = db.BlogList.Find(id);
            if (blogList == null)
            {
                return HttpNotFound();
            }
            return View(blogList);
        }

        // POST: BlogLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogList blogList = db.BlogList.Find(id);
            db.BlogList.Remove(blogList);
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
