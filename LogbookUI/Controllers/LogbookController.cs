using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LogbookUI.Controllers
{
    [Authorize]
    public class LogbookController : Controller
    {
        [Authorize]
        public ActionResult AddEntry()
        {
            return View();
        }
    }
}
