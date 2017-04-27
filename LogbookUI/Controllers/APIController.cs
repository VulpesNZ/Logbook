using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Logbook.Core;
using Logbook.Core.DTO;

namespace LogbookUI.Controllers
{
    public class APIController : ApiController
    {
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public ActivityDTO[] GetActivities()
        {
            return DataAccess.GetActivities().ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public ActivityForAppDTO[] GetActivitiesForApp(Guid userid)
        {
            return DataAccess.GetActivitiesForApp(userid);
        }
    }
}
