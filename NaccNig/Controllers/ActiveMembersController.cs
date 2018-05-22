using System;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NaccNigModels.Members;
using NaccNig.Models;
using NaccNig.ViewModels;
using NaccNigModels.PopUp;
using System.IO;
using NaccNigModels.Structures;
using System.Collections.Generic;

namespace NaccNigModels.Controllers
{
    [Authorize(Roles = RoleName.Admin + "," + RoleName.ActiveMember)]

    public class ActiveMembersController : Controller
    {
        private NaccNigDbContext db;

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
                model.ActiveMemberId = activeUser.ActiveMemberId;
                model.Fullname = activeUser.Fullname;
                model.Age = activeUser.Age;
                model.Photo = activeUser.Photo;
            }
            return View(model);
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

        // GET: ActiveMembers/Edit/5
        [AllowAnonymous]
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
            ViewBag.MyList = new SelectList(db.Province, "ProId", "ProvinceName");

            return View();
        }

        // POST: ActiveMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileUpdate(ActiveMember activeMember)
        {
            var userId = User.Identity.GetUserId();
            activeMember.ActiveMemberId = userId;


            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileNameWithoutExtension(activeMember.ImageFile.FileName);
                var extension = Path.GetExtension(activeMember.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                activeMember.Photo = "~/PhotoUpload/ActiveMembers/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/PhotoUpload/ActiveMembers/"), fileName);
                activeMember.ImageFile.SaveAs(fileName);
                db.ActiveMember.Add(activeMember);
                db.SaveChanges();
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
            ViewBag.MyList = new SelectList(db.Province, "ProId", "ProvinceName");
            return View(activeMember);
        }
        [Authorize]
        public async Task<ActionResult> MyProfile(MyProfileVM model)
        {
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
                model.ActiveMemberId = activeUser.ActiveMemberId;
                model.Fullname = activeUser.Fullname;
                model.Age = activeUser.Age;
                model.ActiveMemberId = activeUser.ActiveMemberId;
                model.Photo = activeUser.Photo;
                model.Province = activeUser.ProvinceId;
                model.StateChapter = activeUser.StateChapterId;
                model.Zone = activeUser.ZoneId;

            }
            return View(model);
        }

        // GET: ActiveMembers/Edit/5
        
        [HttpGet]
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
            ViewBag.MyList = new SelectList(db.Province, "ProId", "ProvinceName");
            return View(activeMember);
        }

        // POST: ActiveMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ActiveMember activeMember)
        {
            var userId = User.Identity.GetUserId();
            activeMember.ActiveMemberId = userId;

     

            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileNameWithoutExtension(activeMember.ImageFile.FileName);
                var extension = Path.GetExtension(activeMember.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                activeMember.Photo = "~/PhotoUpload/ActiveMembers/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/PhotoUpload/ActiveMembers/"), fileName);
                activeMember.ImageFile.SaveAs(fileName);
                db.Entry(activeMember).State = EntityState.Modified;
                await db.SaveChangesAsync();
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
            ViewBag.MyList = new SelectList(db.Province, "ProId", "ProvinceName");
            return View(activeMember);
        }


        public ActionResult GetProvinceList()
        {
            List<Province> ProvinceList = db.Province.ToList();
            ViewBag.MyList = new SelectList(ProvinceList, "ProvinceId", "Province");
            return View();
        }

        public JsonResult GetStateChapterList(int? ProId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<StateChapter> StateChapterList = db.StateChapter.Where(x => x.ProId == ProId).ToList();
            return Json(StateChapterList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetZoneList(int? StateChapId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Zone> ZoneList = db.Zone.Where(x => x.StateChapId == StateChapId).ToList();
            return Json(ZoneList, JsonRequestBehavior.AllowGet);
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
