using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LogbookUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "PasswordReset",
                url: "Account/PasswordReset/{requestId}",
                defaults: new { controller = "Account", action = "PasswordReset" }
            );

            routes.MapRoute(
                "ViewLogbook",                                              // Route name
                "Logbook/Logbook/{logbookId}",                           // URL with parameters
                new { controller = "Logbook", action = "Logbook" }  // Parameter defaults
            );


            // Default always goes last
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
