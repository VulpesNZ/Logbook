using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Logbook.Core;
using Logbook.Core.DTO;

namespace LogbookUI.Controllers
{
    public class APIController : ApiController
    {
        // Nuke it from orbit

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public ActivityForAppDTO[] GetActivitiesForApp(Guid userid)
        {
            return DataAccess.GetActivitiesForApp(userid);
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public FieldForAppDTO[] GetFieldsForUser(Guid userid)
        {
            return DataAccess.GetFieldsForUser(userid).ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public FieldOptionForAppDTO[] GetFieldOptionsForUser(Guid userid)
        {
            return DataAccess.GetFieldOptionsForUser(userid).ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public LogbookForAppDTO[] GetLogbooksForUser(Guid userId)
        {
            return DataAccess.GetLogbooksForApp(userId).ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public string Ping()
        {
            return "RESPONSE/SUCCESS";
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public LogbookEntryForAppDTO[] GetEntries(Guid userId)
        {
            var entries = DataAccess.GetAllEntries(userId);
            foreach (var entry in entries)
            {
                entry.selectedFieldOptions = DataAccess.GetSelectedFieldsForApp(entry.logbookEntryId);
                entry.fieldCustomValues = DataAccess.GetCustomFieldsForApp(entry.logbookEntryId);
                entry.syncStatus = "SYNCED";
            }
            return entries.ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public string Login([FromBody] UserData userInfo)
        {
            var user = DataAccess.GetUser(userInfo.Username);
            if (user == null)
            {
                return null;
            }

            var pbkdf2 = new Rfc2898DeriveBytes(userInfo.Password, user.PasswordSalt, 1000);
            var providedHash = pbkdf2.GetBytes(32);
            var passwordCorrect = true;
            for (var i = 0; i < 32; i++)
            {
                if (providedHash[i] != user.PasswordHash[i])
                {
                    passwordCorrect = false;
                }
            }

            return passwordCorrect ? user.UserId.ToString() : null;
        }
        

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public bool DeleteEntry([FromBody] EntryFromAppJSON entry)
        {
            var existingEntry = DataAccess.GetLogbookEntry(entry.entryId);
            if (existingEntry != null)
            {
                DataAccess.DeleteLogbookEntry(entry.entryId);
            }
            return true;
        }

        private LogbookEntryDTO GetDTOFromAppEntry(Guid userId, LogbookEntryForAppDTO entry)
        {
            var entryDTO = new LogbookEntryDTO();

            entryDTO.LogbookEntryId = entry.logbookEntryId;

            entryDTO.EntryDate = entry.date;
            entryDTO.ActivityId = entry.activityId;
            entryDTO.CreateDate = DateTime.Now;
            entryDTO.CreatedBy = userId;
            entryDTO.UpdateDate = DateTime.Now;
            entryDTO.UpdatedBy = userId;
            entryDTO.LogbookId = entry.logbookId;
            entryDTO.Notes = entry.notes;
            entryDTO.Status = "STATUS/ACTIVE";

            var fieldOptionsToSave = new List<LogbookEntryFieldDTO>();
            foreach (var field in entry.selectedFieldOptions)
            {
                var dbField = DataAccess.GetField(field.fieldId);
                var dbFieldOption = DataAccess.GetFieldOption(field.fieldOptionId);
                if (dbFieldOption != null)
                {
                    var fieldOptionMappings = new List<ActivityFieldOptionMapping>
                    {
                        new ActivityFieldOptionMapping()
                        {
                            FieldId = dbField.FieldId,
                            ActivityId = dbField.ActivityId,
                            FieldOptionId = field.fieldOptionId,
                            FieldName = dbField.Name,
                            OptionText = dbFieldOption.Text,
                            Selected = true
                        }
                    };
                    fieldOptionsToSave.Add(new LogbookEntryFieldDTO()
                    {
                        Name = dbField.Name,
                        FieldId = dbField.FieldId,
                        LogbookId = entry.logbookId,
                        Active = true,
                        ActivityId = entry.activityId,
                        ActivityFieldOptionMappings = fieldOptionMappings.ToArray()
                    });
                }
            }
            foreach (var field in entry.fieldCustomValues)
            {
                var dbField = DataAccess.GetField(field.fieldId);
                fieldOptionsToSave.Add(new LogbookEntryFieldDTO()
                {
                    Name = dbField.Name,
                    FieldId = dbField.FieldId,
                    LogbookId = entry.logbookId,
                    Active = true,
                    ActivityId = entry.activityId,
                    CustomText = field.customValue,
                    ActivityFieldOptionMappings = new List<ActivityFieldOptionMapping>().ToArray()
                });
            }
            entryDTO.EntryFields = fieldOptionsToSave.ToArray();

            return entryDTO;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public ExportResponse Export([FromBody] ExportRequest request)
        {
            var response = new ExportResponse();
            var user = DataAccess.GetUser(request.UserId);
            var attachmentData = new byte[0];
            // generate the report
            switch (request.Format)
            {
                case "CSV":
                    attachmentData = Reporting.GenerateCSVReport(request.ActivityId, request.FromDate, request.ToDate);
                    break;
            }

            // email the report
            Mailer.SendMessage("reporting@theoutdoorlogbook.com", user.Email, "Your report from The Outdoor Logbook", "Test body", attachmentData, $"activity report.{request.Format.ToLower()}");

            // return the result
            response.Success = true;
            response.Message = $"Your report has been emailed to {user.Email}.";
            return response;
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public SyncResponse SyncAll(Guid userId, [FromBody] SyncData syncData)
        {
            var errors = new List<string>();
            var response = new SyncResponse();
            var success = true;
            var entryCount = 0;
            var logbookCount = 0;
            // Create/update/delete logbook entries
            // If anything (fields, options, activities) doesn't exist for whatever reason, just remove the offending part if possible.  For missing activities, for now just set to first activity in list.
            foreach (var entry in syncData.entries)
            {
                try
                {
                    switch (entry.syncStatus)
                    {
                        case "DELETED": DataAccess.DeleteLogbookEntry(entry.logbookEntryId);
                            break;
                        case "NEW":
                        case "UPDATED":
                            {
                                var entryLocalDTO = GetDTOFromAppEntry(userId, entry);
                                if (DataAccess.GetLogbookEntry(entry.logbookEntryId) == null)
                                {
                                    DataAccess.AddLogbookEntry(entryLocalDTO, userId);
                                }
                                else
                                {
                                    DataAccess.UpdateLogbookEntry(entryLocalDTO);
                                }
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed on entry {entry.logbookEntryId}: {ex.Message} {ex.StackTrace}");
                    success = false;
                }
            }

            // Then create/update/delete logbooks
            // Make sure to only set status to deleted, don't delete anything!
            //foreach (var logbook in syncData.logbooks)
            //{
            //    // NYI
            //}

            if (success)
            {
                response.message = $"Sync complete. Synced {syncData.entries.Length} entries.";
                // get the post-sync data to pass to local
                response.logbooks = GetLogbooksForUser(userId);
                response.entries = GetEntries(userId);
                response.activities = GetActivitiesForApp(userId);
                response.fields = GetFieldsForUser(userId);
                response.fieldOptions = GetFieldOptionsForUser(userId);
            }
            else
            {
                response.message =
                    "Sync failed due to an error.  Please try again in a few minutes.  If this error persists, contact support.";
                response.errors = errors.ToArray();
            }
            response.ok = success;
            return response;
        }

        public class ExportResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public class ExportRequest
        {
            public Guid UserId { get; set; }
            public Guid ActivityId { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Format { get; set; }
        }

        public class UserData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class SyncData
        {
            public LogbookForAppDTO[] logbooks { get; set; }
            public LogbookEntryForAppDTO[] entries { get; set; }
        }

        public class SyncResponse
        {
            public string[] errors { get; set; }
            public string message { get; set; }
            public bool ok { get; set; }
            public ActivityForAppDTO[] activities { get; set; }
            public LogbookForAppDTO[] logbooks { get; set; }
            public LogbookEntryForAppDTO[] entries { get; set; }
            public FieldForAppDTO[] fields { get; set; }
            public FieldOptionForAppDTO[] fieldOptions { get; set; }
        }

        public string GetFormattedDate(DateTime d)
        {
            var s = new StringBuilder(d.Day.ToString());
            switch (d.Day.ToString().PadLeft(2, '0').Substring(1, 1))
            {
                case "1":
                    s.Append("st");
                    break;
                case "2":
                    s.Append("nd");
                    break;
                case "3":
                    s.Append("rd");
                    break;
                default:
                    s.Append("th");
                    break;
            }
            s.AppendFormat(" {0:MMMM yyyy}", d);
            return s.ToString();
        }
    }
}
