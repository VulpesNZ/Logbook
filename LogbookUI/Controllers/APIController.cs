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
        public FieldDTO[] GetFieldsForUser(Guid userid)
        {
            return DataAccess.GetFieldsForUser(userid).ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public FieldOptionDTO[] GetFieldOptionsForUser(Guid userid)
        {
            return DataAccess.GetFieldOptionsForUser(userid).ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public LogbookDTO[] GetLogbooksForUser(Guid userId)
        {
            return DataAccess.GetLogbooks(userId).ToArray();
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
                entry.selectedFieldOptions = DataAccess.GetSelectedFieldsForApp(entry.entryId);
                entry.fieldCustomValues = DataAccess.GetCustomFieldsForApp(entry.entryId);
                entry.syncStatus = "SYNCED";
                entry.formattedDate = GetFormattedDate(entry.date);
            }
            return entries.ToArray();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public string Login([FromBody]UserData userInfo)
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
        public bool CreateEntry([FromBody]EntryFromAppJSON entry)
        {
            var entryDTO = GetDTOFromAppEntry(entry.userId, entry);

            var existingEntry = DataAccess.GetLogbookEntry(entry.entryId);
            if (existingEntry != null)
            {
                DataAccess.UpdateLogbookEntry(entryDTO);
            }
            else
            {
                DataAccess.AddLogbookEntry(entryDTO);
            }

            var createdEntry = DataAccess.GetLogbookEntry(entry.entryId);
            return createdEntry != null;
        }

        private LogbookEntryDTO GetDTOFromAppEntry(Guid userId, EntryFromAppJSON entry)
        {
            var entryDTO = new LogbookEntryDTO();

            entryDTO.LogbookEntryId = entry.entryId;

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

        public class UserData
        {
            public string Username { get; set; }
            public string Password { get; set; }
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
