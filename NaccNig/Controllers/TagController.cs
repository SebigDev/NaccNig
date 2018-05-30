using NaccNig.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NaccNig.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListArticleByTags(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new NaccNigDbContext())
            {
                var articles = database.Tags
                    .Include(t => t.Articles.Select(a => a.Tags))
                    .Include(t => t.Articles.Select(a => a.Author))
                    .FirstOrDefault(t => t.Id == id)
                    .Articles
                    .ToList();

                return View(articles);
            }
        }
    }
}