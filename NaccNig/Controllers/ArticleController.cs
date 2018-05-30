using NaccNig.Models;
using NaccNig.Models.Blog;
using NaccNig.ViewModels;
using NaccNigModels.Blog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NaccNig.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //
        // GET: Article/List
        public ActionResult List()
        {
            using (var database = new NaccNigDbContext())
            {
                var articles = database.Articles
                    .Include(a => a.Author)
                    .Include(a => a.Tags)
                    .ToList();

                if(articles == null)
                {
                    var message = "No Articles yet";
                    ViewBag.Article = message;
                }

                return View(articles);
            }
        }

        //
        // GET: Article/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
              return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               // throw new HttpException();
            }

            using (var database = new NaccNigDbContext())
            {
                var article = database.Articles
                    .Where(a => a.Id == id)
                    .Include(a => a.Author)
                    .Include(a=>a.Comments)
                    .Include(a => a.Tags)
                    .First();
                

                if (article == null)
                {
                    return HttpNotFound();
                }

                IQueryable getComments = database.Comments.Where(c => c.Id == id).Select(s => s.CommentDetail);
                if (HttpContext.Request.IsAjaxRequest())
                {
                    return Json(getComments, JsonRequestBehavior.AllowGet);
                }
                ViewBag.Paylist = getComments;
                return View(article);
            }
        }

        //
        // GET: Article/Create
        [Authorize]
        public ActionResult Create()
        {
            using (var database = new NaccNigDbContext())
            {
                var model = new ArticleViewModel();
                model.Categories = database.Categories
                    .OrderBy(c => c.Name)
                    .ToList();
                return View(model);
            }

        }

        //
        // POST: Article/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var database = new NaccNigDbContext())
                {
                    var authorId = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;

                    var article = new Article(authorId, model.Title, model.Content, model.CategoryId);

                    this.SetArticleTags(article, model, database);

                    database.Articles.Add(article);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        //
        // GET: Article/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new NaccNigDbContext())
            {

                var article = database.Articles
                    .Where(a => a.Id == id)
                    .Include(a => a.Author)
                    .Include(a => a.Category)
                    .First();

                if (!IsUserAuthorizedToEdit(article))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                ViewBag.TagsString = string.Join(", ", article.Tags.Select(t => t.Name));

                if (article == null)
                {
                    return HttpNotFound();
                }

                var model = new ArticleViewModel();
                model.Id = article.Id;
                model.Title = article.Title;
                model.Content = article.Content;
                model.CategoryId = article.CategoryId; ;
                model.Categories = database.Categories
                    .OrderBy(c => c.Name)
                    .ToList();
                return View(model);

            }



        }

        //
        // POST: Article/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new NaccNigDbContext())
            {
                var article = database.Articles
                .Where(a => a.Id == id)
                .Include(a => a.Author)
                .First();

                if (article == null)
                {
                    return HttpNotFound();
                }

                database.Articles.Remove(article);
                database.SaveChanges();



                return RedirectToAction("Index");
            }
        }

        //
        // GET: Article/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new NaccNigDbContext())
            {
                var article = database.Articles
                    .Where(a => a.Id == id)
                    .First();

                if (!IsUserAuthorizedToEdit(article))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                if (article == null)
                {
                    return HttpNotFound();
                }

                var model = new ArticleViewModel();
                model.Id = article.Id;
                model.Title = article.Title;
                model.Content = article.Content;
                model.CategoryId = article.CategoryId; ;
                model.Categories = database.Categories
                    .OrderBy(c => c.Name)
                    .ToList();
                model.Tags = string.Join(", ", article.Tags.Select(t => t.Name));

                return View(model);
            }
        }

        //
        // POST: Article/Edit
        [HttpPost]
        public ActionResult Edit(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var database = new NaccNigDbContext())
                {
                    var article = database.Articles
                        .FirstOrDefault(a => a.Id == model.Id);

                    article.Title = model.Title;
                    article.Content = model.Content;
                    article.CategoryId = model.CategoryId;
                    this.SetArticleTags(article, model, database);

                    database.Entry(article).State = EntityState.Modified;
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        private void SetArticleTags(Article article, ArticleViewModel model, NaccNigDbContext database)
        {
            var tagsStrings = model.Tags
                 .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(t => t.ToLower())
                 .Distinct();

            article.Tags.Clear();

            foreach (var tagString in tagsStrings)
            {
                Tag tag = database.Tags.FirstOrDefault(t => t.Name.Equals(tagString));

                if (tag == null)
                {
                    tag = new Tag() { Name = tagString };
                    database.Tags.Add(tag);
                }

                article.Tags.Add(tag);
            }
        }

        private bool IsUserAuthorizedToEdit(Article article)
        {
            bool isAdmin = this.User.IsInRole("Admin");
            bool isAuthor = article.IsAuthor(this.User.Identity.Name);

            return isAdmin || isAuthor;
        }

        public PartialViewResult Comments(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = "";
            }
            return PartialView();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Comments(Comments comments)
        {
            if (ModelState.IsValid)
            {
                using(var db = new NaccNigDbContext())
                {
                    db.Comments.Add(comments);
                    db.SaveChanges();
                    return PartialView("CommentList");
                }
                
            }

            return PartialView(comments);
        }
        public PartialViewResult CommentList(int? id)
        {
            using(var db = new NaccNigDbContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
               IEnumerable<Comments> commentListed = db.Comments.Where(c=>c.Id == id).ToList();
                
                ViewBag.PayList = new SelectList(commentListed, "Id", "CommentDetail");
                return PartialView();

            }

        }

    }


}
