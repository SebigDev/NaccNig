using System;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NaccNigModels.Members;
using NaccNig.Models;
using NaccNig.ViewModels;
using NaccNigModels.PopUp;
using System.Collections.Generic;
using NaccNigModels.Blog;

namespace NaccNigModels.Controllers
{
    [Authorize(Roles =RoleName.Admin +"," + RoleName.ActiveMember )]
    
    public class ActiveMembersController : Controller
    {
        private NaccNigDbContext db;

        public IEnumerable Amount { get; private set; }

        public ActiveMembersController()
        {
            db = new NaccNigDbContext();
        }

        [Authorize(Roles = RoleName.Admin)]
        // GET: ActiveMembers
        public ActionResult Index()
        {
            return View(db.ActiveMember.ToList());
        }
        public ActionResult Dashboard(MemberDashboardVM model)
        {
            var userId = User.Identity.GetUserId();
            var activeId = userId;
            var activeUser = db.ActiveMember.AsNoTracking()
                                     .FirstOrDefault(a => a.ActiveMemberId.Equals(activeId));
            if (activeUser != null)
            {
                model.Firstname = activeUser.Firstname;
                model.Middlename = activeUser.Middlename;
                model.Surname = activeUser.Surname;
                model.Address = activeUser.Address;
                model.CallUpNumber = activeUser.CallUpNumber;
                model.PhoneNumber = activeUser.PhoneNumber;
                model.StateCode = activeUser.StateCode;
                model.StateOfDeployment = activeUser.StateOfDeployment;
                model.StateOfOrigin = activeUser.StateOfOrigin;
                model.Gender = activeUser.Gender;
                model.MemberId = activeUser.ActiveMemberId;
                model.Fullname = activeUser.Fullname;
                model.Age = activeUser.Age;
            }
            int blog = db.BlogList.Count();
            ViewBag.Blog = blog;
            return View(model);
        }

        public async Task<ActionResult> BlogLists()
        {
            var blogList = await db.BlogList.AsNoTracking().Take(3).ToListAsync();
            
            if(blogList == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogList = blogList;
            return View();
        }

        public ActionResult BlogPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPost.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            var blogCat = db.BlogCategory.ToList();
            var blogCount = db.BlogCategory.FirstOrDefault();
            var name = blogCount.CategoryName;
            int catPost = db.BlogList.Count(x => x.BlogCategory.CategoryName.ToString() == name);
            ViewBag.BlogCat = blogCat;
            ViewBag.CountPost = catPost;
            return View(blogPost);
        }
        // GET: ActiveMembers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActiveMember activeMember = db.ActiveMember.Find(id);
            if (activeMember == null)
            {
                return HttpNotFound();
            }
            return View(activeMember);
        }

        // GET: ActiveMembers/Create
        public ActionResult ProfileUpdate()
        {
            var myGender = from Gender s in Enum.GetValues(typeof(Gender))
                           select new { ID = s, Name = s.ToString() };
            var myState = from State s in Enum.GetValues(typeof(State))
                          select new { ID = s, Name = s.ToString() };
            var depState = from StateDeployed s in Enum.GetValues(typeof(StateDeployed))
                           select new { ID = s, Name = s.ToString() };
            ViewBag.Gender = new SelectList(myGender, "Name", "Name");
            ViewBag.StateOfOrigin = new SelectList(myState, "Name", "Name");
            ViewBag.StateOfDeployment = new SelectList(depState, "Name", "Name");
            return View();
        }

        // POST: ActiveMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProfileUpdate(ActiveMember activeMember, string MemberId)
        {
            var userId = User.Identity.GetUserId();
            activeMember.ActiveMemberId = userId;
            if (ModelState.IsValid)
            {
                db.ActiveMember.Add(activeMember);
                await db.SaveChangesAsync();
                ViewBag.Message = $"Hello {User.Identity.GetUserName()}; Your Profile Updated Successfully.";
                return RedirectToAction("Dashboard");
            }
            var myGender = from Gender s in Enum.GetValues(typeof(Gender))
                           select new { ID = s, Name = s.ToString() };
            var myState = from State s in Enum.GetValues(typeof(State))
                          select new { ID = s, Name = s.ToString() };
            var depState = from StateDeployed s in Enum.GetValues(typeof(StateDeployed))
                           select new { ID = s, Name = s.ToString() };
            ViewBag.Gender = new SelectList(myGender, "Name", "Name");
            ViewBag.StateOfOrigin = new SelectList(myState, "Name", "Name");
            ViewBag.StateOfDeployment = new SelectList(depState, "Name", "Name");

            return View(activeMember);
        }
        [Authorize]
       

        public async Task<ActionResult> MyProfile()
        {
            MemberDashboardVM model = new MemberDashboardVM();

            var userId = User.Identity.GetUserId();
            var activeId = userId;
            var activeUser = await db.ActiveMember.AsNoTracking()
                                     .FirstOrDefaultAsync(a => a.ActiveMemberId.Equals(activeId));
            if (activeUser != null)
            {
                model.Firstname = activeUser.Firstname;
                model.Middlename = activeUser.Middlename;
                model.Surname = activeUser.Surname;
                model.Address = activeUser.Address;
                model.CallUpNumber = activeUser.CallUpNumber;
                model.PhoneNumber = activeUser.PhoneNumber;
                model.StateCode = activeUser.StateCode;
                model.StateOfDeployment = activeUser.StateOfDeployment;
                model.StateOfOrigin = activeUser.StateOfOrigin;
                model.Gender = activeUser.Gender;
                model.MemberId = activeUser.ActiveMemberId;
                model.Fullname = activeUser.Fullname;
                model.Age = activeUser.Age;
                model.MemberId = activeUser.ActiveMemberId;

            }
            return View(model);
        }

        // GET: ActiveMembers/Edit/5
        public ActionResult Update(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActiveMember activeMember = db.ActiveMember.Find(id);
            if (activeMember == null)
            {
                return HttpNotFound();
            }
            var myGender = from Gender s in Enum.GetValues(typeof(Gender))
                           select new { ID = s, Name = s.ToString() };
            var myState = from State s in Enum.GetValues(typeof(State))
                          select new { ID = s, Name = s.ToString() };
            var depState = from StateDeployed s in Enum.GetValues(typeof(StateDeployed))
                           select new { ID = s, Name = s.ToString() };
            ViewBag.Gender = new SelectList(myGender, "Name", "Name");
            ViewBag.StateOfOrigin = new SelectList(myState, "Name", "Name");
            ViewBag.StateOfDeployment = new SelectList(depState, "Name", "Name");
            return View(activeMember);
        }

        // POST: ActiveMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ActiveMember activeMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activeMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyProfile");
            }

            var myGender = from Gender s in Enum.GetValues(typeof(Gender))
                           select new { ID = s, Name = s.ToString() };
            var myState = from State s in Enum.GetValues(typeof(State))
                          select new { ID = s, Name = s.ToString() };
            var depState = from StateDeployed s in Enum.GetValues(typeof(StateDeployed))
                           select new { ID = s, Name = s.ToString() };
            ViewBag.Gender = new SelectList(myGender, "Name", "Name");
            ViewBag.StateOfOrigin = new SelectList(myState, "Name", "Name");
            ViewBag.StateOfDeployment = new SelectList(depState, "Name", "Name");
            return View(activeMember);
        }

        [Authorize(Roles = RoleName.Admin)]
        // GET: ActiveMembers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActiveMember activeMember = db.ActiveMember.Find(id);
            if (activeMember == null)
            {
                return HttpNotFound();
            }
            return View(activeMember);
        }
        [Authorize(Roles = RoleName.Admin)]
        // POST: ActiveMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ActiveMember activeMember = db.ActiveMember.Find(id);
            db.ActiveMember.Remove(activeMember);
            db.SaveChanges();
            return RedirectToAction("ActiveMemberList");
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
