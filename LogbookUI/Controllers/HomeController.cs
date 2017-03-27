using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Logbook.Core;
using LogbookUI.Models;
using Microsoft.AspNet.Identity;

namespace LogbookUI.Controllers
{
    [Authorize]
    public class HomeController : CustomController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Home()
        {
            var model = new HomeViewModel() { ActiveLogbookName = string.Empty };
            model.Announcements = DataAccess.GetAnnouncementPreviews();
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

        [Authorize]
        public ActionResult Announcement(Guid announcementId)
        {
            var model = new AnnouncementViewModel();
            var announcement = DataAccess.GetAnnouncement(announcementId);
            model.Title = announcement.Title;
            model.Body = announcement.Body;
            model.PublishDate = announcement.PublishDate;
            return View(model);
        }
    }
}
