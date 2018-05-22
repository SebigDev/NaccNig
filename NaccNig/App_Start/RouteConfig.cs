using NaccNig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NaccNigModels
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               "GetZoneList",
               "ActiveMembers/GetZone/List/{StateChapId}",
               new {controller ="ActiveMembers", action= "GetZone", StateChapId = 0}
           );

            routes.MapRoute(
               "GetStateChapterList",
               "ActiveMembers/GetStateChapter/List/{ProId}",
               new { controller = "ActiveMembers", action = "GetStateChapter", ProId = 0 }
           );
            routes.MapRoute(
               "GetProvinceList",
               "ActiveMembers/GetProvince",
               new { controller = "ActiveMembers", action = "GetProvince" }
           );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            routes.Add(new LowercaseDashedRoute("{controller}/{action}/{id}",
    new RouteValueDictionary(
        new { controller = "Home", action = "Index", id = UrlParameter.Optional }),
        new DashedRouteHandler()
    )
);
        }
    }
}
