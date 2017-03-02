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
    public class SettingsController : CustomController
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
            ViewBag.BackLinkHtml = MenuConstructor.ConstructHtmlBackLink("EditActivity", activityId);
            var model = new EditActivityViewModel();
            var dto = DataAccess.GetActivity(activityId);
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.ImageUrl = dto.ImageUrl;
            model.Fields = DataAccess.GetFields(activityId);
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
        
        [Authorize]
        public ActionResult EditField(Guid fieldId)
        {
            ViewBag.BackLinkHtml = MenuConstructor.ConstructHtmlBackLink("EditField", fieldId);
            var model = new EditFieldViewModel();
            var dto = DataAccess.GetField(fieldId);
            model.FieldId = dto.FieldId;
            model.Name = dto.Name;
            model.FieldOptions = DataAccess.GetFieldOptions(fieldId);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditField(EditFieldViewModel model)
        {
            var dto = new FieldDTO();
            dto.FieldId = model.FieldId;
            dto.Name = model.Name;
            DataAccess.UpdateField(dto);
            return RedirectToAction("EditActivity", "Settings", new { activityId = DataAccess.GetField(model.FieldId).ActivityId });
        }

        [Authorize]
        public ActionResult EditFieldOption(Guid fieldOptionId)
        {
            ViewBag.BackLinkHtml = MenuConstructor.ConstructHtmlBackLink("EditFieldOption", fieldOptionId);
            var model = new EditFieldOptionViewModel();
            var dto = DataAccess.GetFieldOption(fieldOptionId);
            model.FieldOptionId = fieldOptionId;
            model.Text = dto.Text;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddField(Guid activityId, string fieldName)
        {
            var fields = DataAccess.GetFields(activityId, false);
            var existingField = fields.FirstOrDefault(a => a.Name.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase));
            if (existingField != null)
            {
                if (existingField.Active)
                    ModelState.AddModelError("A field by that name already exists.", string.Empty);  // TODO: This doesn't work
                else
                    DataAccess.UndeleteField(existingField.FieldId);
            }
            else
            {
                DataAccess.AddField(DataAccess.GetUser(User.Identity.GetUserName()).UserId, activityId, fieldName);
            }
            return null;
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddFieldOption(Guid fieldId, string optionName)
        {
            var fieldOptions = DataAccess.GetFieldOptions(fieldId);
            var existingOption = fieldOptions.FirstOrDefault(a => a.Text.Equals(optionName, StringComparison.CurrentCultureIgnoreCase));
            if (existingOption != null)
            {
                if (existingOption.Active)
                    ModelState.AddModelError("An option by that name already exists.", string.Empty);  // TODO: This doesn't work
                else
                    DataAccess.UndeleteFieldOption(existingOption.FieldOptionId);
            }
            else
            {
                DataAccess.AddFieldOption(fieldId, optionName);
            }
            return null;
        }
    }
}
