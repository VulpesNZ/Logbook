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
    public class SettingsController : Controller
    {
        [Authorize]
        public ActionResult Settings()
        {
            var model = new ActivitySettingsViewModel();
            model.Activities = DataAccess.GetActivitiesForUser(DataAccess.GetUser(User.Identity.GetUserName()).UserId);
            return View(model);
        }

        [Authorize]
        [HttpDelete]
        public ActionResult RemoveUserActivity(Guid activityId)
        {
            DataAccess.DeleteUserActivity(activityId);
            return null;
        }

        [Authorize]
        public ActionResult EditActivity(Guid activityId)
        {
            var model = new EditActivityViewModel();
            var dto = DataAccess.GetActivity(activityId);
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.ImageUrl = dto.ImageUrl;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditActivity(EditActivityViewModel model)
        {
            var dto = new ActivityDTO();
            dto.ActivityId = model.ActivityId;
            dto.Name = model.Name;
            dto.Description = model.Description;
            DataAccess.UpdateActivity(dto);
            return RedirectToAction("Settings", "Settings");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddUserActivity(string name)
        {
            var activities = DataAccess.GetActivitiesForUser(DataAccess.GetUser(User.Identity.GetUserName()).UserId, false);
            var existingActivity = activities.FirstOrDefault(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (existingActivity != null)
            {
                if (existingActivity.Active)
                    ModelState.AddModelError("An activity by that name already exists.", string.Empty);  // TODO: This doesn't work
                else
                    DataAccess.UndeleteActivity(existingActivity.ActivityId);
            }
            else
            {
                DataAccess.AddUserActivity(DataAccess.GetUser(User.Identity.GetUserName()).UserId, name);
            }
            return null;
        }
    }
}
