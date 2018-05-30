using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NaccNig.Models;
using NaccNig.ViewModels;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using Paystack.Net.SDK.Transactions;
using System.Collections;
using NaccNigModels.Members;
using NaccNigModels.Structures;
using NaccNigModels.Payment;
using NaccNigModels.PopUp;

namespace NaccNig.Controllers
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
            var stateChapter = StateChapter.GetStateChapters().FirstOrDefault();
            var zone = Zone.GetZones().FirstOrDefault();
            var amountpaid = Amount.GetAmountList().FirstOrDefault();
            var paycategory = PaymentCategory.GetPaymentCategoryList().FirstOrDefault(x => x.PaymentCategoryId ==amountpaid.PaymentCategoryId);
            if (activeUser != null)
            {
                model.Firstname = activeUser.Firstname;
                model.Middlename = activeUser.Middlename;
                model.Surname = activeUser.Surname;
                model.Address = activeUser.Address;
                model.CallUpNumber = activeUser.CallUpNumber;
                model.PhoneNumber = activeUser.PhoneNumber;
                model.StateCode = activeUser.StateCode;
                model.StateOfOrigin = activeUser.StateOfOrigin;
                model.Gender = activeUser.Gender;
                model.YearServed = activeUser.DateServed;
                model.ActiveMemberId = activeUser.ActiveMemberId;
                model.Fullname = activeUser.Fullname;
                model.Age = activeUser.Age;
                model.Photo = activeUser.Photo;
                model.StateChapter = stateChapter.StateChapterName;
                model.Zone = zone.ZoneName;
                model.Amount = amountpaid.Price;
                model.PaymentName = paycategory.CategoryName;
            }
            var myPix = model.Photo;
            var paylist = PaymentCategory.GetPaymentCategoryList();
            ViewBag.Photo = myPix;
            ViewBag.List = paylist;
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
            ViewBag.StateList = new SelectList(StateChapter.GetStateChapters(), "StateChapId", "StateChapterName");

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
            ViewBag.StateList = new SelectList(StateChapter.GetStateChapters(), "StateChapId", "StateChapterName");
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
                model.StateOfOrigin = activeUser.StateOfOrigin;
                model.Gender = activeUser.Gender;
                model.ActiveMemberId = activeUser.ActiveMemberId;
                model.Fullname = activeUser.Fullname;
                model.Age = activeUser.Age;
                model.ActiveMemberId = activeUser.ActiveMemberId;
                model.Photo = activeUser.Photo;
               
                model.StateChapter = activeUser.StateChapterId;
                model.Zone = activeUser.ZoneId;

            }
            var pic = model.Photo;
            ViewBag.Photo = pic;
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
            ViewBag.StateList = new SelectList(StateChapter.GetStateChapters(), "StateChapId", "StateChapterName");
            ViewBag.Zone = new SelectList(Zone.GetZones(), "ZId", "ZoneName");
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
            ViewBag.StateList = new SelectList(db.StateChapter, "StateChapId", "StateChapterName");
            ViewBag.Zone = new SelectList(db.Zone, "ZId", "ZoneName");
            return View(activeMember);
        }

        public ActionResult GetStateChapterList()
        {
            List<StateChapter> stateChapters = db.StateChapter.ToList();
            ViewBag.StateList = new SelectList(StateChapter.GetStateChapters().ToList());
            return View();
        }

        public ActionResult GetZoneList(int? StateChapId)
        {
            IQueryable ZoneList = Zone.GetZones().Where(x => x.StateChapId == StateChapId);
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(ZoneList, "ZId", "ZoneName"),
                    JsonRequestBehavior.AllowGet);
            }
            return View(ZoneList);
        }


        //P A Y M E N T   C O N T R O L L E R  M E T H O D

        public ActionResult Payment()
        {
            var payid = "naccnigeria-" + HttpContext.User.Identity.GetUserId() + "--" + DateTime.Now.ToString("yymmssfff");
            ViewBag.PaymentId = payid;
            ViewBag.PayList = new SelectList(PaymentCategory.GetPaymentCategoryList().ToList());
            //ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "FullName");
            return View();
        }
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(PaymentSetting paymentSetting)
        {
            var payeeId = HttpContext.User.Identity.GetUserId();
            var payid = "naccnigeria-" + HttpContext.User.Identity.GetUserId() + "--" + DateTime.Now.ToString("yymmssfff");
            
            if (String.IsNullOrEmpty(payid))
            {
                return View("Payment", new { message = "No or Invalid Payment Id" });
            }

            if (ModelState.IsValid)
            {
                db.PaymentSetting.Add(paymentSetting);
                db.SaveChanges();
                return RedirectToAction("MakePayment", new {id = paymentSetting.PaymentCategoryId });
            }
            ViewBag.PaymentId = payid;
           // ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId","FullName", paymentSetting.ActiveMemberId);
            ViewBag.PayList = new SelectList(PaymentCategory.GetPaymentCategoryList().ToList());
            return View(paymentSetting);
        }
        public ActionResult GetPayCategory(string PaymentId)
        {
            IQueryable paymentCategories = PaymentCategory.GetPaymentCategoryList();
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(paymentCategories, "PaymentCategoryId", "CategoryName"),
               JsonRequestBehavior.AllowGet);
            }
            return View(paymentCategories);
        }
        public ActionResult GetAmount(int? PaymentCategoryId)
        {
            IQueryable amounts = Amount.GetAmountList().Where(x=>x.PaymentCategoryId == PaymentCategoryId);
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(amounts, "PriceId", "Price"),
               JsonRequestBehavior.AllowGet);
            }
            return View(amounts);
        }


        public async Task<JsonResult> InitializePayment(PaymentVM model)
        {
            string secretKey = ConfigurationManager.AppSettings["PaystackSecret"];
            var paystackTransactionAPI = new PaystackTransaction(secretKey);
            var response = await paystackTransactionAPI.InitializeTransaction(model.PaymentId, model.Amount , model.CategoryName, model.MemberName, "http://localhost:8081/active-members/pay-status");
            //Note that callback url is optional
            if (response.status == true)
            {
                return Json(new { error = false, result = response }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true, result = response }, JsonRequestBehavior.AllowGet);

        }
       
        public async Task<ActionResult> PayStatus()
        {
            string secretKey = ConfigurationManager.AppSettings["PaystackSecret"];
            var paystackTransactionAPI = new PaystackTransaction(secretKey);
            var tranxRef = HttpContext.Request.QueryString["reference"];
            if (tranxRef != null)
            {
                var response = await paystackTransactionAPI.VerifyTransaction(tranxRef);
                if (response.status)
                {
                    return View(response);
                }
            }

            return View("PaymentError");
        }

        //A J A X   C A L L  F O R   F O R    C A S C A D I N G    P A Y M E N T    W I T H   A M O U N T   A N D   C A T E G O R Y
        public ActionResult GetPayment()
        {
            List<PaymentCategory> paymentCategoryList = db.PaymentCategory.ToList();
            ViewBag.PayList = new SelectList(paymentCategoryList, "PaymentCategoryId", "CategoryName");
            return View();
        }

        public JsonResult GetAmountList(int? PaymentCategoryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Amount> PayAmountList = db.Amount.Where(p=>p.PaymentCategoryId == PaymentCategoryId).ToList();
            return Json(PayAmountList, JsonRequestBehavior.AllowGet);
        
        }


        public ActionResult MakePayment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentVM model = new PaymentVM();
            var payerId = User.Identity.GetUserId();
            var payee = db.ActiveMember.AsNoTracking().FirstOrDefault(x=>x.ActiveMemberId == payerId);
            PaymentSetting paymentSetting = db.PaymentSetting.FirstOrDefault(x => x.PaymentCategoryId == id);
            var paycategories = PaymentCategory.GetPaymentCategoryList().FirstOrDefault(x => x.PaymentCategoryId == id);
            var amountpaid = Amount.GetAmountList().FirstOrDefault(x=>x.PaymentCategoryId == id);

            if (paymentSetting == null)
            {
                return HttpNotFound();
            }

            if(paymentSetting != null)
            {
                model.PaymentId = paymentSetting.PaymentId;
                model.PaymentCategoryId = paymentSetting.PaymentCategoryId;
                model.MemberName = payee.Fullname;
                model.CategoryName = paycategories.CategoryName;
                model.Amount = amountpaid.Price;
                model.Photo = payee.Photo;
            }

            return View(model);
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
