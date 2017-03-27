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
            model.Activities = DataAccess.GetActivitiesForUser(Guid.Parse(User.Identity.GetUserId()));
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateLogbook(CreateLogbookViewModel model)
        {
            var logbook = new LogbookDTO();
            logbook.UserId = Guid.Parse(User.Identity.GetUserId());
            logbook.Name = model.Name;
            logbook.DefaultActivityId = model.DefaultActivityId;
            logbook.Status = "STATUS/ACTIVE";
            logbook.CreatedBy = Guid.Parse(User.Identity.GetUserId());
            logbook.UpdatedBy = Guid.Parse(User.Identity.GetUserId());
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
            ViewBag.BackLinkHtml = MenuConstructor.ConstructHtmlBackLink("Logbook", logbookId);
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
            model.Activities = DataAccess.GetActivitiesForUser(Guid.Parse(User.Identity.GetUserId()));  //TODO: Unfuck this
            model.LogbookId = logbookId;
            model.EntryDate = DateTime.Today;
            model.ActivityId = DataAccess.GetLogbook(logbookId).DefaultActivityId;
            model.LogbookEntryFields = DataAccess.GetFieldOptionMappings(Guid.Parse(User.Identity.GetUserId()), model.ActivityId);
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
            logbook.CreatedBy = Guid.Parse(User.Identity.GetUserId());
            logbook.UpdatedBy = Guid.Parse(User.Identity.GetUserId());
            logbook.CreateDate = DateTime.Now;
            logbook.UpdateDate = DateTime.Now;
            logbook.EntryDate = model.EntryDate;
            logbook.Notes = model.Notes;
            logbook.EntryFields = model.LogbookEntryFields;
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
            model.Logbooks = DataAccess.GetLogbooks(Guid.Parse(User.Identity.GetUserId()));
            return View(model);
        }

        [Authorize]
        public ActionResult LogbookEntry(Guid logbookEntryId)
        {
            ViewBag.BackLinkHtml = MenuConstructor.ConstructHtmlBackLink("LogbookEntry", logbookEntryId);
            var entry = DataAccess.GetLogbookEntry(logbookEntryId);
            var model = new LogbookEntryViewModel();
            model.Notes = entry.Notes;
            model.EntryDate = entry.EntryDate;
            model.Logbook = DataAccess.GetLogbook(entry.LogbookId);
            model.ActivityId = entry.ActivityId;
            // format the option selections for readability.
            var fields = DataAccess.GetSelectedFields(logbookEntryId);
            Dictionary<string,string> selectedFieldText = new Dictionary<string, string>();
            foreach (var f in fields)
            {
                if (selectedFieldText.ContainsKey(f.FieldName))
                {
                    selectedFieldText[f.FieldName] += ", " + f.OptionText;
                }
                else
                {
                    selectedFieldText.Add(f.FieldName, f.OptionText);
                }
            }
            model.SelectedFields = selectedFieldText.Select(f => new SelectedFieldOption() {FieldName = f.Key, OptionText = f.Value}).ToArray();
            return View(model);
        }
    }
}
