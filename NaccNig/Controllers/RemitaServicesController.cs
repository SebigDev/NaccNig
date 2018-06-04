using NaccNig.Models;
using NaccNig.Services;
using NaccNig.ViewModels;
using NaccNigModels.PaymentSettings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NaccNig.BusinessLogic;
using NaccNigModels.PopUp;

namespace NaccNig.Controllers
{
    public class RemitaServicesController : Controller
    {
        private NaccNigDbContext _db;
        public QueryCommand _query;

        public RemitaServicesController()
        {
            _db = new NaccNigDbContext();

        }
        [AllowAnonymous]
        public ActionResult GetPaymentStatus(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetPaymentStatus(ConfirmRrr model)
        {
            if (!string.IsNullOrEmpty(model.rrr))
            {
                if (model.PaymentType.ToString().Equals(PaymentType.MembershipRegistration))
                {
                    return RedirectToAction("RetryMembershipRegistration", "MembershipFee", new { rrr = model.rrr.Trim() });
                }
                if (model.PaymentType.ToString().Equals(PaymentType.MonthlyDues))
                {
                    return RedirectToAction("RetryMonthlyDues", "MembershipFee", new { rrr = model.rrr.Trim() });
                }
            }
            ViewBag.Message = "RRR cannot be empty or the selected Category is not applicable yet ";
            return View();
        }


        // GET: RemitaServices
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> PaymentNotification(List<RemitaNotificationVm> item)
        {
            if (ModelState.IsValid)
            {
                foreach (var model in item)
                {
                    if (!string.IsNullOrEmpty(model.orderRef) && !string.IsNullOrEmpty(model.rrr)
                        && !string.IsNullOrEmpty(model.serviceTypeId))
                    {
                        if (model.serviceTypeId.Equals(RemitaConfigParam.MEMBERSHIPREGISTRATION) 
                            || model.serviceTypeId.Equals(RemitaConfigParam.MONTHLYDUES))
                        {
                            await ProcessMembershipFee(model.rrr, model.orderRef);
                        }

                        else
                        {
                            return Content("Service Type is not registered on the Portal yet");
                        }

                    }
                }

            }
            return Content("Ok");
        }
        private async Task ProcessMembershipFee(string RRR, string orderID)
        {
            MembershipFee membershipRegistration;
            if (string.IsNullOrEmpty(orderID))
            {
                membershipRegistration = await _db.MembershipFee.AsNoTracking()
                        .Where(x => x.ReferenceNo.Equals(RRR))
                        .FirstOrDefaultAsync();
            }
            else
            {
                membershipRegistration = await _db.MembershipFee.AsNoTracking()
                        .Where(x => x.OrderId.Equals(orderID.Trim()))
                        .FirstOrDefaultAsync();
            }
            if (membershipRegistration != null)
            {
                if (membershipRegistration.Status.Equals(true))
                {

                }
                else
                {
                    var log = await _db.RemitaPaymentLog.AsNoTracking()
                        .Where(x => x.OrderId.Equals(membershipRegistration.OrderId))
                        .FirstOrDefaultAsync();

                    var hashed = _query.HashRemitedValidate(membershipRegistration.OrderId, RemitaConfigParam.APIKEY,
                        RemitaConfigParam.MERCHANTID);
                    string url = RemitaConfigParam.CHECKSTATUSURL + "/" + RemitaConfigParam.MERCHANTID + "/" +
                                 orderID + "/" + hashed + "/" + "orderstatus.reg";
                    string jsondata = new WebClient().DownloadString(url);
                    RemitaResponse result = JsonConvert.DeserializeObject<RemitaResponse>(jsondata);

                    if (result.Status.Equals("00") || result.Status.Equals("01"))
                    {
                        membershipRegistration.Status = true;
                        membershipRegistration.PaymentStatus = result.Message;
                        membershipRegistration.ReferenceNo = result.Rrr;
                        _db.Entry(membershipRegistration).State = EntityState.Modified;

                        log.Rrr = result.Rrr;
                        log.StatusCode = result.Status;
                        log.TransactionMessage = result.Message;
                        _db.Entry(log).State = EntityState.Modified;
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        membershipRegistration.Status = false;
                        membershipRegistration.PaymentStatus = result.Message;
                        membershipRegistration.ReferenceNo = result.Rrr;
                        _db.Entry(membershipRegistration).State = EntityState.Modified;

                        log.Rrr = result.Rrr;
                        log.StatusCode = result.Status;
                        log.TransactionMessage = result.Message;
                        _db.Entry(log).State = EntityState.Modified;
                        await _db.SaveChangesAsync();
                    }
                }


            }
        }

    }
}