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
            var model = new EditLogbookEntryViewModel();
            model.Activities = DataAccess.GetActivitiesForUser(Guid.Parse(User.Identity.GetUserId()));
            model.LogbookId = logbookId;
            model.LogbookEntryId = Guid.Empty;
            model.EntryDate = DateTime.Today;
            model.ActivityId = DataAccess.GetLogbook(logbookId).DefaultActivityId;
            model.LogbookEntryFields = DataAccess.GetFieldOptionMappings(Guid.Parse(User.Identity.GetUserId()), model.ActivityId);
            return View(model);
        }

        [Authorize]
        public ActionResult EditLogbookEntry(Guid logbookEntryId)
        {
            var model = new EditLogbookEntryViewModel();
            var entry = DataAccess.GetLogbookEntry(logbookEntryId);
            var selections = DataAccess.GetSelectedFields(logbookEntryId);
            model.Activities = DataAccess.GetActivitiesForUser(Guid.Parse(User.Identity.GetUserId()));
            model.LogbookId = entry.LogbookId;
            model.LogbookEntryId = logbookEntryId;
            model.EntryDate = entry.EntryDate;
            model.ActivityId = entry.ActivityId;
            model.Notes = entry.Notes;
            model.LogbookEntryFields = DataAccess.GetFieldOptionMappings(Guid.Parse(User.Identity.GetUserId()), model.ActivityId);
            foreach (var field in model.LogbookEntryFields)
            {
                field.ActivityFieldOptionMappings = DataAccess.GetFieldOptionMappings(field.FieldId);
            }
            foreach (var selection in selections)
            {
                if (selection.FieldOptionId == Guid.Empty)
                {
                    model.LogbookEntryFields.Single(f => f.FieldId == selection.FieldId).CustomText = selection.OptionText;
                }
                else
                {
                    model.LogbookEntryFields.Single(f => f.FieldId == selection.FieldId)
                        .ActivityFieldOptionMappings.Single(m => m.FieldOptionId == selection.FieldOptionId)
                        .Selected = true;
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddLogbookEntry(EditLogbookEntryViewModel model)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var logbook = new LogbookEntryDTO();
            logbook.LogbookId = model.LogbookId;
            logbook.ActivityId = model.ActivityId;
            logbook.Status = "STATUS/ACTIVE";
            logbook.CreatedBy = userId;
            logbook.UpdatedBy = userId;
            logbook.CreateDate = DateTime.Now;
            logbook.UpdateDate = DateTime.Now;
            logbook.EntryDate = model.EntryDate;
            logbook.Notes = model.Notes;
            logbook.EntryFields = model.LogbookEntryFields ?? new LogbookEntryFieldDTO[0];
            DataAccess.AddLogbookEntry(logbook, userId);
            if (logbook.LogbookEntryId == Guid.Empty)
            {
                ModelState.AddModelError("", "Failed to create entry");
            }
            else
            {
                return RedirectToAction("Logbook", "Logbook", new { logbookId = logbook.LogbookId });
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EditLogbookEntry(EditLogbookEntryViewModel model)
        {
            var logbook = new LogbookEntryDTO();
            logbook.LogbookId = model.LogbookId;
            logbook.LogbookEntryId = model.LogbookEntryId;
            logbook.ActivityId = model.ActivityId;
            logbook.Status = "STATUS/ACTIVE";
            logbook.UpdatedBy = Guid.Parse(User.Identity.GetUserId());
            logbook.UpdateDate = DateTime.Now;
            logbook.EntryDate = model.EntryDate;
            logbook.Notes = model.Notes;
            logbook.EntryFields = model.LogbookEntryFields ?? new LogbookEntryFieldDTO[0];
            DataAccess.UpdateLogbookEntry(logbook);
            return RedirectToAction("LogbookEntry", "Logbook", new { logbookEntryId = logbook.LogbookEntryId });
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
            model.LogbookEntryId = logbookEntryId;
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
