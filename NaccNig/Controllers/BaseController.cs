using Microsoft.AspNet.Identity;
using NaccNig.BusinessLogic;
using NaccNig.Models;
using System.Web.Mvc;

namespace NaccNig.Controllers
{
    public class BaseController : Controller
    {
        private NaccNigDbContext _db = new NaccNigDbContext();
        public bool _IsPayedMembershipRegistration;
        public bool _IsPayedMonthlyDues;
        public QueryCommand _query;

        public string userId;
       
        public BaseController()
        {
            _db = new NaccNigDbContext();
            _query = new QueryCommand();
           
            var model = _query.GetPaymentStatus();
            //userId = User.Identity.GetUserId(); ;
           
        }


        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            bool hasPayedMembershipRegistration = false;

            var model = _query.GetPaymentStatus();
            ViewBag.LayoutViewModel = model;

        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}