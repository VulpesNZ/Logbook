using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LogbookUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}
