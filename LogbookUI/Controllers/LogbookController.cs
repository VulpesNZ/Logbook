using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Logbook.Core;
using Logbook.Core.DTO;
using LogbookUI.Models;
using Microsoft.AspNet.Identity;

namespace LogbookUI.Controllers
{
    [Authorize]
    public class LogbookController : CustomController
    {
        [Authorize]
        public ActionResult AddEntry()
        {
            return View();
        }

        [Authorize]
        public ActionResult CreateLogbook()
        {
            var model = new CreateLogbookViewModel();
            model.Activities = DataAccess.GetActivitiesForUser(DataAccess.GetUser(User.Identity.GetUserName()).UserId);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateLogbook(CreateLogbookViewModel model)
        {
            var logbook = new LogbookDTO();
            logbook.Name = model.Name;
            logbook.DefaultActivityId = model.DefaultActivityId;
            logbook.Status = "STATUS/ACTIVE";
            logbook.CreatedBy = Guid.Empty;
            logbook.UpdatedBy = Guid.Empty;
            logbook.CreateDate = DateTime.Now;
            logbook.UpdateDate = DateTime.Now;
            DataAccess.CreateLogbook(logbook);
            if (logbook.LogbookId == Guid.Empty)
            {
                ModelState.AddModelError("", "Failed to create logbook");
            }
            else
            {
                return RedirectToAction("Logbook", "Logbook", new { logbookId = logbook.LogbookId });
            }
            return View();
        }

        [Authorize]
        public ActionResult Logbook(Guid logbookId)
        {
            var logbook = new LogbookViewModel();
            var logbookDTO = DataAccess.GetLogbook(logbookId);
            logbook.Name = logbookDTO.Name;
            logbook.Activity = DataAccess.GetActivity(logbookDTO.DefaultActivityId).Name;
            logbook.LastUpdated = logbookDTO.UpdateDate;
            logbook.LogbookId = logbookDTO.LogbookId;
            logbook.Entries = DataAccess.GetLogbookEntries(logbook.LogbookId);
            return View(logbook);
        }

        [Authorize]
        public ActionResult AddLogbookEntry(Guid logbookId)
        {
            var model = new AddLogbookEntryViewModel();
            model.Activities = DataAccess.GetActivitiesForUser(DataAccess.GetUser(User.Identity.GetUserName()).UserId);  //TODO: Unfuck this
            model.LogbookId = logbookId;
            model.EntryDate = DateTime.Today;
            model.ActivityId = DataAccess.GetLogbook(logbookId).DefaultActivityId;
            model.ActivityFieldOptionMappings = DataAccess.GetFieldOptionMappings(DataAccess.GetUser(User.Identity.GetUserName()).UserId);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddLogbookEntry(AddLogbookEntryViewModel model)
        {
            var logbook = new LogbookEntryDTO();
            logbook.LogbookId = model.LogbookId;
            logbook.ActivityId = model.ActivityId;
            logbook.Status = "STATUS/ACTIVE";
            logbook.CreatedBy = Guid.Empty;
            logbook.UpdatedBy = Guid.Empty;
            logbook.CreateDate = DateTime.Now;
            logbook.UpdateDate = DateTime.Now;
            logbook.EntryDate = model.EntryDate;
            logbook.Notes = model.Notes;
            DataAccess.AddLogbookEntry(logbook);
            if (logbook.LogbookId == Guid.Empty)
            {
                ModelState.AddModelError("", "Failed to create entry");
            }
            else
            {
                return RedirectToAction("Logbook", "Logbook", new { logbookId = logbook.LogbookId });
            }
            return View(model);
        }

        [Authorize]
        public ActionResult MyLogbooks()
        {
            var model = new MyLogbooksViewModel();
            model.Logbooks = DataAccess.GetLogbooks();
            return View(model);
        }
    }
}
