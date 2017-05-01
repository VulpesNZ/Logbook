using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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
                entry.synced = true;
                entry.formattedDate = entry.date.ToString("dd-MMM-yyyy");
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
                //fieldOptions.Add(new LogbookEntryFieldDTO() {  });
                var dbField = DataAccess.GetField(field.fieldId);
                var dbFieldOption = DataAccess.GetFieldOption(field.fieldOption.FieldOptionId);
                var fieldOptionMappings = new List<ActivityFieldOptionMapping>
                {
                    new ActivityFieldOptionMapping()
                    {
                        FieldId = dbField.FieldId,
                        ActivityId = dbField.ActivityId,
                        FieldOptionId = field.fieldOption.FieldOptionId,
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
            entryDTO.EntryFields = fieldOptionsToSave.ToArray();

            return entryDTO;
        }

        public class UserData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
