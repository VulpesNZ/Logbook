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
    public class LogbookController : Controller
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
            model.Activities = DataAccess.GetActivities();
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
            return View(logbook);
        }
    }
}
