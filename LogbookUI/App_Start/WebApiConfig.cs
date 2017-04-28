﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace LogbookUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));


            config.Routes.MapHttpRoute(
                name: "GetActivitiesForApp",
                routeTemplate: "api/activities/{userid}", 
                defaults: new { controller = "Api", action = "GetActivitiesForApp" }
            );
            config.Routes.MapHttpRoute(
                name: "GetLogbooksForUser",
                routeTemplate: "api/logbooks/{userid}",
                defaults: new { controller = "Api", action = "GetLogbooksForUser" }
            );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
