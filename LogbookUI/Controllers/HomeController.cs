using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LogbookUI.Models;
using Microsoft.AspNet.Identity;

namespace LogbookUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Home()
        {
            var model = new HomeViewModel() { ActiveLogbookName = "Test" };
            return View(model);
        }

        [Authorize]
        public ActionResult AddEntry()
        {
            return RedirectToAction("AddEntry", "Logbook");
        }

        [Authorize]
        public ActionResult ChangeActiveLogbook()
        {
            return RedirectToAction("ChangeActiveLogbook", "Logbook");
        }

        [Authorize]
        public ActionResult CreateLogbook()
        {
            return RedirectToAction("CreateLogbook", "Logbook");
        }
    }
}
