using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Logbook.Core.DTO;

namespace Logbook.Core
{
    public static class DataAccess
    {
        public static UserDTO GetUser(string email)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<UserDTO>("SELECT TOP 1 * FROM [User] WHERE Email = '" + email + "'").FirstOrDefault();
            }
        }
        public static UserDTO GetUser(Guid userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<UserDTO>("SELECT TOP 1 * FROM [User] WHERE UserId = @UserId",
                            new { UserId = userId }).FirstOrDefault();
            }
        }

        public static Guid CreateUser(UserDTO user)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var userId =
                    conn.Query<Guid>(
                        "INSERT INTO [User] (Email, PasswordHash, PasswordSalt, Name, Location, Status) " +
                        "OUTPUT inserted.UserId " +
                        "VALUES (@Email, @PasswordHash, @PasswordSalt, @Name, @Location, @Status)",
                        user).Single();
                ResetActivitiesToDefault(userId);
                return userId;
            }
        }

        public static void ResetActivitiesToDefault(Guid userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute($"exec PopulateDefaultActivities '{userId}'");
            }
        }

        public static ActivityForAppDTO[] GetActivitiesForApp(Guid userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<ActivityForAppDTO>($"exec GetActivitiesForApp '{userId}'").ToArray();
            }
        }

        public static Guid GenerateRequest(RequestDTO request)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                request.RequestId = Guid.NewGuid();
                conn.Execute(
                    "INSERT INTO Request (RequestId, UserId, RequestType) VALUES (@RequestId, @UserId, @RequestType)",
                    request);
                return request.RequestId;
            }
        }

        public static void ResetPassword(UserDTO user)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE [User] SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt WHERE UserId = @UserId", user);
            }
        }

        public static void ConsumeRequest(Guid requestId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute($"UPDATE Request SET Consumed = 1 WHERE RequestId = '{requestId}'");
            }
        }

        public static bool CheckRequest(Guid requestId, string email)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var request =
                    conn.Query<RequestDTO>(
                        string.Format(
                            "SELECT * FROM Request " +
                            "JOIN [User] ON [User].UserId = Request.UserId " +
                            "WHERE [User].Email = '{0}' " +
                            "AND Request.RequestId = '{1}' " +
                            "AND Request.Expires > GETDATE()" +
                            "AND Consumed = 0", email, requestId)).FirstOrDefault();
                return request != null;
            }
        }

        public static List<ActivityDTO> GetActivities()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<ActivityDTO>("SELECT * FROM Activity ORDER BY Name").ToList();
            }
        }

        public static void CreateLogbook(LogbookDTO logbook)
        {
            logbook.LogbookId = Guid.NewGuid();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute(   "INSERT INTO Logbook (LogbookId, UserId, Name, DefaultActivityId, CreatedBy, UpdatedBy, Status) " +
                                "OUTPUT inserted.LogbookId " +
                                "VALUES (@LogbookId, @UserId, @Name, @DefaultActivityId, @CreatedBy, @UpdatedBy, @Status)",
                    logbook);
            }
        }

        public static LogbookDTO GetLogbook(Guid logbookId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<LogbookDTO>("SELECT TOP 1 * FROM Logbook WHERE LogbookId = @LogbookId", new { LogbookId = logbookId }).SingleOrDefault();
            }
        }

        public static ActivityDTO GetActivity(Guid activityId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<ActivityDTO>("SELECT TOP 1 * FROM Activity WHERE ActivityId = @ActivityId", new { ActivityId = activityId }).SingleOrDefault();
            }
        }

        public static void UpdateLogbookEntry(LogbookEntryDTO logbook)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE LogbookEntry " +
                             "SET UpdatedBy = @UpdatedBy, UpdateDate = @UpdateDate, Status = @Status, EntryDate = @EntryDate, Notes = @Notes " +
                             "WHERE LogbookEntryId = @LogbookEntryId",
                    logbook);
                foreach (var f in logbook.EntryFields)
                {
                    if (f.ActivityFieldOptionMappings != null)
                    {
                        foreach (var o in f.ActivityFieldOptionMappings.Where(d => !string.IsNullOrEmpty(d.OptionText)))
                        {
                            conn.Execute(
                                "EXEC SelectFieldOption @LogbookEntryId, @FieldOptionId, @Selected",
                                new { logbook.LogbookEntryId, o.FieldOptionId, o.Selected });
                        }
                    }
                    if (f.CustomText != null)
                    {
                        conn.Execute(
                                    "EXEC SetFieldCustomText @LogbookEntryId, @FieldId, @CustomText",
                                    new { logbook.LogbookEntryId, f.FieldId, f.CustomText });
                    }
                }
            }
        }

        public static void AddLogbookEntry(LogbookEntryDTO logbook)
        {
            logbook.LogbookEntryId = Guid.NewGuid();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var entryId = conn.Query<Guid>("INSERT INTO LogbookEntry (LogbookEntryId, LogbookId, ActivityId, CreatedBy, UpdatedBy, CreateDate, UpdateDate, Status, EntryDate, Notes) " +
                                               "OUTPUT inserted.LogbookEntryId " +
                                                "VALUES (@LogbookEntryId, @LogbookId, @ActivityId, @CreatedBy, @UpdatedBy, @CreateDate, @UpdateDate, @Status, @EntryDate, @Notes)",
                    logbook).Single();
                foreach (var f in logbook.EntryFields)
                {
                    if (f.ActivityFieldOptionMappings != null)
                    {
                        foreach (var o in f.ActivityFieldOptionMappings.Where(d => !string.IsNullOrEmpty(d.OptionText)))
                        {
                            conn.Execute(
                                "INSERT INTO LogbookEntryFieldOption (LogbookEntryId, FieldOptionId, Selected) " +
                                "VALUES (@LogbookEntryId, @FieldOptionId, @Selected)",
                                new {LogbookEntryId = entryId, FieldOptionId = o.FieldOptionId, Selected = o.Selected});
                        }
                    }
                    if (!string.IsNullOrEmpty(f.CustomText))
                    {
                        conn.Execute("INSERT INTO LogbookEntryFieldOptionCustom(LogbookEntryId, FieldId, CustomValue) " +
                                     "VALUES (@LogbookEntryId, @FieldId, @CustomValue)",
                            new {LogbookEntryId = entryId, f.FieldId, CustomValue = f.CustomText});
                    }
                }
            }
        }

        public static IEnumerable<LogbookEntryForAppDTO> GetAllEntries(Guid userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<LogbookEntryForAppDTO>("SELECT *, Activity.Name AS ActivityName, EntryDate AS Date FROM LogbookEntry " +
                                                   "JOIN Logbook ON Logbook.LogbookId = LogbookEntry.LogbookId " +
                                                   "JOIN Activity ON Activity.ActivityId = LogbookEntry.ActivityId " +
                                                   "WHERE Logbook.UserId = @UserId", new { UserId = userId });
            }
        }

        public static IEnumerable<LogbookEntryDTO> GetLogbookEntries(Guid logbookId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<LogbookEntryDTO>("SELECT * FROM LogbookEntry WHERE LogbookId = @LogbookId", new { LogbookId = logbookId });
            }
        }

        public static LogbookEntryDTO GetLogbookEntry(Guid logbookEntryId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<LogbookEntryDTO>("SELECT * FROM LogbookEntry WHERE LogbookEntryId = @LogbookEntryId", new { LogbookEntryId = logbookEntryId }).SingleOrDefault();
            }
        }

        public static IEnumerable<LogbookDTO> GetLogbooks(Guid userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<LogbookDTO>("SELECT * FROM Logbook WHERE UserId = @UserId AND Status = 'STATUS/ACTIVE'", new { UserId = userId });
            }
        }

        public static IEnumerable<LogbookForAppDTO> GetLogbooksForApp(Guid userId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<LogbookForAppDTO>("SELECT LogbookId, UpdateDate, Name, DefaultActivityId FROM Logbook WHERE UserId = @UserId AND Status = 'STATUS/ACTIVE'", new { UserId = userId });
            }
        }

        public static IEnumerable<ActivityDTO> GetActivitiesForUser(Guid userId, bool activeOnly = true)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var s = "SELECT * FROM Activity WHERE UserId = @UserId";
                if (activeOnly)
                    s += " AND Active = 1";
                return conn.Query<ActivityDTO>(s, new { UserId = userId });
            }
        }

        public static void DeleteUserActivity(Guid activityId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE Activity SET Active = 0 WHERE ActivityId = @ActivityId", new { ActivityId = activityId });
            }
        }

        public static void DeleteField(Guid fieldId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE Field SET Active = 0 WHERE FieldId = @FieldId", new { FieldId = fieldId });
            }
        }

        public static void AddUserActivity(Guid userId, string name)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("INSERT INTO Activity (UserId, Name) VALUES (@UserId, @Name)", new { UserId = userId, Name = name });
            }
        }

        public static void UpdateActivity(ActivityDTO dto)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE Activity SET Name = @Name, Description = @Description WHERE ActivityId = @ActivityId",
                    new { dto.ActivityId, dto.Name, dto.Description });
            }
        }

        public static void UpdateField(FieldDTO dto)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE Field SET Name = @Name, AllowFreeText = @AllowFreeText, IsRequired = @IsRequired, IsMultiSelect = @IsMultiSelect WHERE FieldId = @FieldId",
                    new { dto.FieldId, dto.Name, dto.AllowFreeText, dto.IsMultiSelect, dto.IsRequired });
            }
        }

        public static void UpdateFieldOption(FieldOptionDTO dto)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE FieldOption SET Text = @Text WHERE FieldOptionId = @FieldOptionId", new { dto.FieldOptionId, dto.Text });
            }
        }


        public static void UndeleteActivity(Guid activityId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE Activity SET Active = 1 WHERE ActivityId = @ActivityId", new { ActivityId = activityId });
            }
        }
        public static void UndeleteField(Guid fieldId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE Field SET Active = 1 WHERE FieldId = @FieldId", new { FieldId = fieldId });
            }
        }
        public static void UndeleteFieldOption(Guid fieldOptionId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE FieldOption SET Active = 1 WHERE FieldOptionId = @FieldOptionId", new { FieldOptionId = fieldOptionId });
            }
        }


        public static IEnumerable<FieldDTO> GetFields(Guid activityId, bool activeOnly = true)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var s = "SELECT * FROM Field WHERE ActivityId = @ActivityId ";
                if (activeOnly)
                    s += " AND Active = 1";
                s += " ORDER BY SortOrder";
                return conn.Query<FieldDTO>(s, new { ActivityId = activityId });
            }
        }

        public static IEnumerable<FieldForAppDTO> GetFieldsForUser(Guid userId, bool activeOnly = true)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var s = "SELECT fieldId, activityId, name, allowFreeText, sortOrder, isRequired FROM Field WHERE UserId = @UserId ";
                if (activeOnly)
                    s += " AND Active = 1";
                s += " ORDER BY SortOrder";
                return conn.Query<FieldForAppDTO>(s, new { UserId = userId });
            }
        }

        public static IEnumerable<FieldOptionForAppDTO> GetFieldOptionsForUser(Guid userId, bool activeOnly = true)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var s = "SELECT FieldOption.* FROM FieldOption JOIN Field ON Field.FieldId = FieldOption.FieldId WHERE UserId = @UserId ";
                if (activeOnly)
                    s += " AND FieldOption.Active = 1";
                s += " ORDER BY FieldOption.SortOrder";
                return conn.Query<FieldOptionForAppDTO>(s, new { UserId = userId });
            }
        }

        public static FieldDTO GetField(Guid fieldId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<FieldDTO>("SELECT TOP 1 * FROM Field WHERE FieldId = @FieldId", new { FieldId = fieldId }).SingleOrDefault();
            }
        }

        public static FieldOptionDTO GetFieldOption(Guid fieldOptionId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<FieldOptionDTO>("SELECT TOP 1 * FROM FieldOption WHERE FieldOptionId = @FieldOptionId", new { FieldOptionId = fieldOptionId }).SingleOrDefault();
            }
        }

        public static IEnumerable<FieldOptionDTO> GetFieldOptions(Guid fieldId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<FieldOptionDTO>("SELECT * FROM FieldOption WHERE FieldId = @FieldId", new { FieldId = fieldId });
            }
        }


        public static void AddField(Guid userId, Guid activityId, string name)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("INSERT INTO Field (UserId, ActivityId, Name) VALUES (@UserId, @ActivityId, @Name)", new { UserId = userId, ActivityId = activityId, Name = name });
            }
        }
        public static void AddFieldOption(Guid fieldId, string text)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("INSERT INTO FieldOption (FieldId, Text, SortOrder) VALUES (@FieldId, @Text, (SELECT ISNULL(MAX(SortOrder), 0) FROM FieldOption WHERE FieldId = @FieldId) + 1)", new { FieldId = fieldId, Text = text });
            } 
        }

        public static ActivityFieldOptionMapping[] GetFieldOptionMappings(Guid fieldId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<ActivityFieldOptionMapping>("SELECT * FROM ActivityFieldOptionMapping WHERE FieldId = @FieldId AND (OptionText IS NOT NULL OR AllowFreeText = 1) ORDER BY FieldOptionSortOrder", new {FieldId = fieldId}).ToArray();
            }
        }

        public static LogbookEntryFieldDTO[] GetFieldOptionMappings(Guid userId, Guid activityId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var fields = GetFields(activityId);
                var mappings = fields.Select(f => new LogbookEntryFieldDTO() {Name = f.Name, FieldId = f.FieldId, ActivityFieldOptionMappings = GetFieldOptionMappings(f.FieldId), AllowFreeText = f.AllowFreeText, IsMultiSelect = f.IsMultiSelect, IsRequired = f.IsRequired });
                return mappings.Where(m => m.ActivityFieldOptionMappings.Length > 0 || m.AllowFreeText).ToArray();
            }
        }

        public static SelectedFieldOption[] GetSelectedFields(Guid logbookEntryId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<SelectedFieldOption>("SELECT FieldId, FieldOptionId, Name AS FieldName, Text AS OptionText FROM SelectedFieldOption WHERE LogbookEntryId = @LogbookEntryId", new { LogbookEntryId = logbookEntryId }).ToArray();
            }
        }

        public static SelectedFieldOptionForAppDTO[] GetSelectedFieldsForApp(Guid logbookEntryId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<SelectedFieldOptionForAppDTO>("SELECT FieldId, FieldOptionId, Name AS FieldName, Text as fieldOptionText FROM SelectedFieldOption WHERE LogbookEntryId = @LogbookEntryId AND FieldOptionId IS NOT NULL", new { LogbookEntryId = logbookEntryId }).ToArray();
            }
        }

        public static CustomFieldValueForApp[] GetCustomFieldsForApp(Guid logbookEntryId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<CustomFieldValueForApp>("SELECT FieldId, Text AS CustomValue, Name AS FieldName FROM SelectedFieldOption WHERE LogbookEntryId = @LogbookEntryId AND FieldOptionId IS NULL", new { LogbookEntryId = logbookEntryId }).ToArray();
            }
        }

        public static AnnouncementPreview[] GetAnnouncementPreviews()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<AnnouncementPreview>("SELECT AnnouncementId, Title, CASE WHEN LEN(Body) < 500 THEN Body ELSE CONVERT(VARCHAR(500), Body) + '...' END AS Body, PublishDate FROM Announcement WHERE PublishDate <= @Date", new { Date = DateTime.Today }).ToArray();
            }
        }

        public static AnnouncementDTO GetAnnouncement(Guid announcementId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<AnnouncementDTO>("SELECT * FROM Announcement WHERE AnnouncementId = @AnnouncementId", new { AnnouncementId = announcementId }).SingleOrDefault();
            }
        }

        
    }
}
