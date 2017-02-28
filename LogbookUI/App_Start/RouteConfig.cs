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
                "Logbook/{logbookId}",                           // URL with parameters
                new { controller = "Logbook", action = "Logbook" }  // Parameter defaults
            );

            routes.MapRoute(
                "AddLogbookEntry",                                              // Route name
                "Logbook/{logbookId}/AddEntry",                           // URL with parameters
                new { controller = "Logbook", action = "AddLogbookEntry" }  // Parameter defaults
            );;


            routes.MapRoute(
                "MyLogbooks",                                              // Route name
                "MyLogbooks",                           // URL with parameters
                new { controller = "Logbook", action = "MyLogbooks" }  // Parameter defaults
            );


            routes.MapRoute(
                "NewLogbook",                                              // Route name
                "NewLogbook",                           // URL with parameters
                new { controller = "Logbook", action = "CreateLogbook" }  // Parameter defaults
            );


            routes.MapRoute(
                "Settings",                                              // Route name
                "Settings",                           // URL with parameters
                new { controller = "Settings", action = "Settings" }  // Parameter defaults
            );


            routes.MapRoute(
                "RemoveUserActivity",                                              // Route name
                "Settings/RemoveUserActivity/{activityId}",                           // URL with parameters
                new { controller = "Settings", action = "RemoveUserActivity" }  // Parameter defaults
            );

            routes.MapRoute(
                "Settings/EditActivity",                                              // Route name
                "Settings/EditActivity/{activityId}",                           // URL with parameters
                new { controller = "Settings", action = "EditActivity" }  // Parameter defaults
            );

            routes.MapRoute(
                "Settings/EditField",                                              // Route name
                "Settings/EditField/{fieldId}",                           // URL with parameters
                new { controller = "Settings", action = "EditField" }  // Parameter defaults
            );

            routes.MapRoute(
                "Settings/EditFieldOption",                                              // Route name
                "Settings/EditFieldOption/{fieldOptionId}",                           // URL with parameters
                new { controller = "Settings", action = "EditFieldOption" }  // Parameter defaults
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
