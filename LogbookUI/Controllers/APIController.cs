using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Logbook.Core;
using Logbook.Core.DTO;

namespace LogbookUI.Controllers
{
    public class APIController : ApiController
    {
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public ActivityDTO[] GetActivities()
        {
            return DataAccess.GetActivities().ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public ActivityForAppDTO[] GetActivitiesForApp(Guid userid)
        {
            return DataAccess.GetActivitiesForApp(userid);
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public LogbookDTO[] GetLogbooksForUser(Guid userId)
        {
            return DataAccess.GetLogbooks(userId).ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public bool CreateEntry(Guid userId, EntryFromAppJSON entry)
        {
            var entryDTO = new LogbookEntryDTO();

            entryDTO.LogbookEntryId = entry.entryId;

            entryDTO.EntryDate = entry.date;
            entryDTO.ActivityId = entry.activityId;
            entryDTO.CreateDate = DateTime.Now;
            entryDTO.CreatedBy = userId;
            entryDTO.LogbookId = entry.logbookId;
            entryDTO.Notes = entry.notes;
            entryDTO.Status = "STATUS/ACTIVE";

            var fieldOptions = new List<LogbookEntryFieldDTO>();
            foreach (var fieldOption in entry.selectedFieldOptions)
            {
                //fieldOptions.Add(new LogbookEntryFieldDTO() {  });
            }
            entryDTO.EntryFields = fieldOptions.ToArray();

            DataAccess.AddLogbookEntry(entryDTO);

            // need to return a success message only if it's definitely here, so the app knows.
            // also need to make sure that trying to add the same entry twice doesn't cause an issue.  App should be providing unique GUIDs so just check if it already exists.
            return false;
        }
    }
}
