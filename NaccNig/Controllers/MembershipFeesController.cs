using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NaccNig.BusinessLogic;
using NaccNig.Models;
using NaccNig.Services;
using NaccNig.ViewModels;
using NaccNigModels.PaymentSettings;
using NaccNigModels.PopUp;
using Newtonsoft.Json;

namespace NaccNig.Controllers
{
    public class MembershipFeesController : BaseController
    {
        private NaccNigDbContext db = new NaccNigDbContext();
        

        // GET: MembershipRegistrations
        public ActionResult Index()
        {
            var membershipRegistrations = db.MembershipFee.Include(m => m.ActiveMember);
            return View(membershipRegistrations.ToList());
        }

        // GET: MembershipRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipFee membershipRegistration = db.MembershipFee.Find(id);
            if (membershipRegistration == null)
            {
                return HttpNotFound();
            }
            return View(membershipRegistration);
        }

        // GET: MembershipRegistrations/Create
        public ActionResult CreateFee()
        {
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname");
            return View();
        }

        // POST: MembershipRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFee([Bind(Include = "MembershipFeeId,ActiveMemberId,ReferenceNo,OrderId,PaidFee,TotalAmount,Date,Status,PaymentStatus")] MembershipFee membershipRegistration)
        {
            if (ModelState.IsValid)
            {  
                db.MembershipFee.Add(membershipRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateCode", membershipRegistration.ActiveMemberId);
            return View(membershipRegistration);
        }
        // GET: SchoolFeePayments/Create
        [HttpGet]
        public ActionResult MakePayment()
        {
            var user = User.Identity.GetUserId();
            var payer = db.ActiveMember.AsNoTracking().FirstOrDefault(x=>x.ActiveMemberId==user);
            ViewBag.Payer = payer.Fullname;
            var paytype = from PaymentType p in Enum.GetValues(typeof(PaymentType))
                          select new { ID = p, Name = p.ToString() };

            ViewBag.FeeCategory = new SelectList(paytype, "Name", "Name");
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "Fullname");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> MakePayment(MembershipFeeVM model)
        {
            var userId = User.Identity.GetUserId();
            

            var hasPayed = await db.MembershipFee.AsNoTracking().Where(x => x.ActiveMemberId.Equals(userId))
                                                                         .FirstOrDefaultAsync();
            if (hasPayed != null)
            {
                return RedirectToAction("RetryMembershipRegistration", new { orderId = hasPayed.OrderId });
            }

            var member = await db.ActiveMember.AsNoTracking().Where(x => x.ActiveMemberId.Equals(userId))
                                                              .FirstOrDefaultAsync();
            var paylist = new List<Paylist>();
            var paymentList = await db.MembershipFee.AsNoTracking().ToListAsync();

            foreach (var pay in paylist)
            {
                var myPay = new Paylist
                {
                    PayTypeName = pay.PayTypeName,
                    Amount = pay.Amount,
                    Description = pay.Description

                };
                paylist.Add(myPay);
            }

                var confirmPayment = new SelectPaymentVm
                {
                    ActiveMemberId = User.Identity.GetUserId(),
                    FeeCategory = model.FeeCategory,
                    TotalAmount = paymentList.Sum(s => s.PaidFee),

                };

            var paytype = from PaymentType p in Enum.GetValues(typeof(PaymentType))
                          select new { ID = p, Name = p.ToString() };

            ViewBag.PayType = new SelectList(paytype, "Name", "Name");
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "FullName");
                return RedirectToAction("Create", confirmPayment);
 
            }

       //GET
         public async Task<ActionResult> Create(MembershipFeeVM model)
        {
            if (model.FeeCategory.Equals(PaymentType.MembershipRegistration.ToString()) && _IsPayedMembershipRegistration.Equals(true))
            {
                return RedirectToAction("Index");
            }
            if (model.FeeCategory.Equals(PaymentType.MonthlyDues.ToString()) && _IsPayedMonthlyDues.Equals(true))
            {
                return RedirectToAction("Index");
            }
            var memberId = User.Identity.GetUserId();
            var hasPayedList = await db.MembershipFee.AsNoTracking().Where(x => x.ActiveMemberId==memberId
                                        && x.FeeCategory==model.FeeCategory)
                                        .ToListAsync();

            
            var member = await db.ActiveMember.AsNoTracking()
                                    .Where(x => x.ActiveMemberId==memberId).FirstOrDefaultAsync();

            var paymentList = await db.MemberFeeType.AsNoTracking()
                                        .Where(x => x.FeeCategory==model.FeeCategory)
                                         .ToListAsync();

            var fullName = $"{member.Fullname}";
            var paylist = new List<Paylist>();
          
            foreach (var fee in paymentList)
            {
                var myFee = new Paylist
                {
                    PayTypeName= fee.FeeName,
                    Amount = fee.Amount,
                    Description = fee.Description
                };
                paylist.Add(myFee);
            }
            System.Threading.Thread.Sleep(1);
            long milliseconds = DateTime.Now.Ticks;
            var url = Url.Action("ConfrimPayment", "MembershipFee", new { }, protocol: Request.Url.Scheme);

            if (hasPayedList != null)
            {
                foreach (var hasPayed in hasPayedList)
                {
                    if (hasPayed.Status.Equals(false))
                    {
                        string serviceTypeId = string.Empty;
                        if (hasPayed.FeeCategory.Equals(PaymentType.MembershipRegistration.ToString()))
                        {
                            serviceTypeId = RemitaConfigParam.MEMBERSHIPREGISTRATION;
                        }
                        else if (hasPayed.FeeCategory.Equals(PaymentType.MonthlyDues.ToString()))
                        {
                            serviceTypeId = RemitaConfigParam.MONTHLYDUES;
                        }
                        //var amount = paymentList.Sum(s => s.Amount).ToString();

                        var hashed = _query.HashRemitedValidate(hasPayed.OrderId, RemitaConfigParam.APIKEY, RemitaConfigParam.MERCHANTID);
                        string checkurl = RemitaConfigParam.CHECKSTATUSURL + "/" + RemitaConfigParam.MERCHANTID + "/" + hasPayed.OrderId + "/" + hashed + "/" + "orderstatus.reg";
                        string jsondata = new WebClient().DownloadString(checkurl);
                        var result = JsonConvert.DeserializeObject<RemitaResponse>(jsondata);
                        if (string.IsNullOrEmpty(result.Rrr))
                        {
                            var entry = db.Entry(hasPayed);
                            if (entry.State == EntityState.Detached)
                                db.MembershipFee.Attach(hasPayed);
                            db.MembershipFee.Remove(hasPayed);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            return RedirectToAction("ConfrimPayment", new { orderID = hasPayed.OrderId });
                        }
                    }

                }

            }

            var confirmPaymentVm = new ConfirmPaymentVM();

            confirmPaymentVm.Paylist = paylist;
            confirmPaymentVm.MemberName = fullName;
            confirmPaymentVm.ActiveMemberId = memberId;
            confirmPaymentVm.FeeCategory = model.FeeCategory;
            confirmPaymentVm.TotalAmount = paymentList.Sum(s => s.Amount);
            
            confirmPaymentVm.payerName = fullName;
            confirmPaymentVm.payerPhone = member.PhoneNumber;
            if (model.FeeCategory.Equals(PaymentType.MembershipRegistration.ToString()))
            {
                confirmPaymentVm.amt = paymentList.Sum(s => s.Amount).ToString();
                confirmPaymentVm.TotalAmount = paymentList.Sum(s => s.Amount);
            }
            if (model.FeeCategory.Equals(PaymentType.MonthlyDues.ToString()))
            {
                confirmPaymentVm.amt = paymentList.Sum(s => s.Amount).ToString();
                confirmPaymentVm.TotalAmount = paymentList.Sum(s => s.Amount);
            }
            confirmPaymentVm.merchantId = RemitaConfigParam.MERCHANTID;
            confirmPaymentVm.orderId = $"NACCPlateau{milliseconds}";
            confirmPaymentVm.responseurl = url;
            confirmPaymentVm.serviceTypeId = RemitaConfigParam.MEMBERSHIPREGISTRATION;
            confirmPaymentVm.paymentType = model.MemberFeeType;

            return View(confirmPaymentVm);


        }
        // POST: MembershipFee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ConfirmPaymentVM model)
        {
            if (ModelState.IsValid)
            {
                var hasTransaction = await db.MembershipFee.AsNoTracking().Where(x => x.ActiveMemberId==model.ActiveMemberId
                                                && x.FeeCategory==model.FeeCategory)
                                                .ToListAsync();
                model.paymentType = model.RemitaPaymentType.ToString().Replace("_", " ").ToLower();
               
                if (model.FeeCategory==PaymentType.MembershipRegistration)
                {
                    model.serviceTypeId = RemitaConfigParam.MEMBERSHIPREGISTRATION;
                }
                else if (model.FeeCategory==PaymentType.MonthlyDues)
                {
                    model.serviceTypeId = RemitaConfigParam.MONTHLYDUES;
                }
                if (string.IsNullOrEmpty(model.payerEmail))
                {
                    model.payerEmail = $"{model.payerName.Trim()}@naccplateau.org";
                }
               
                
                var memberFee = new MembershipFee
                {
                    OrderId = model.orderId,
                    FeeCategory = model.FeeCategory,
                    Date = DateTime.Now,
                    ActiveMemberId = model.ActiveMemberId,
                    PaidFee = model.TotalAmount,
                    TotalAmount = model.TotalAmount,
                    PaymentMode = model.PaymentMode


                };
                db.MembershipFee.Add(memberFee);
                var log = new RemitaPaymentLog
                {
                    OrderId = model.orderId,
                    PaymentName = model.FeeCategory,
                    PaymentDate = DateTime.Now,
                    Amount = model.TotalAmount.ToString(),
                    PayerName = model.MemberName
                };
                db.RemitaPaymentLog.Add(log);
                await db.SaveChangesAsync();
                model.hash = _query.HashRemitaRequest(model.merchantId, model.serviceTypeId, model.orderId, model.amt, model.responseurl, RemitaConfigParam.APIKEY);
                return RedirectToAction("SubmitRemita", model);
            }
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult SubmitRemita(ConfirmPaymentVM model)
        {
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RetrySchoolFeePayment(string rrr)
        {

            var hashrrr = _query.HashRrrQuery(rrr, RemitaConfigParam.APIKEY, RemitaConfigParam.MERCHANTID);
            string posturl = RemitaConfigParam.CHECKSTATUSURL + "/" + RemitaConfigParam.MERCHANTID + "/" + rrr + "/" + hashrrr + "/" + "status.reg";
            string jsondata = new WebClient().DownloadString(posturl);
            var result = JsonConvert.DeserializeObject<RemitaResponse>(jsondata);
            if (result.Status.Equals("00") || result.Status.Equals("01"))
            {
                return RedirectToAction("ConfrimPayment", "MembershipFee", new { RRR = result.Rrr, orderID = result.OrderId });
            }
            var url = Url.Action("ConfrimPayment", "MembershipFee", new { }, protocol: Request.Url.Scheme);
            var hash = _query.HashRemitedRePost(RemitaConfigParam.MERCHANTID, rrr, RemitaConfigParam.APIKEY);

            var model = new RemitaRePostVm
            {
                rrr = rrr,
                merchantId = RemitaConfigParam.MERCHANTID,
                hash = hash,
                responseurl = url
            };
            return View(model);
        }


        // GET: MembershipRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipFee membershipFee = db.MembershipFee.Find(id);
            if (membershipFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "FullName", membershipFee.ActiveMemberId);
            return View(membershipFee);
        }

        // POST: MembershipRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MembershipFeeId,ActiveMemberId,ReferenceNo,OrderId,PaidFee,TotalAmount,Date,Status,PaymentStatus")] MembershipFee membershipRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membershipRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActiveMemberId = new SelectList(db.ActiveMember, "ActiveMemberId", "StateCode", membershipRegistration.ActiveMemberId);
            return View(membershipRegistration);
        }

        // GET: MembershipRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipFee membershipFee = db.MembershipFee.Find(id);
            if (membershipFee == null)
            {
                return HttpNotFound();
            }
            return View(membershipFee);
        }

        // POST: MembershipRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipFee membershipRegistration = db.MembershipFee.Find(id);
            db.MembershipFee.Remove(membershipRegistration);
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
