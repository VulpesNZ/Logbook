﻿using System;
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

        public static bool CreateUser(UserDTO user)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var result =
                    conn.Execute(
                        "INSERT INTO [User] (Email, PasswordHash, PasswordSalt, Name, Location, Status) VALUES (@Email, @PasswordHash, @PasswordSalt, @Name, @Location, @Status)",
                        user);
                return result == 1;
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
                conn.Execute("INSERT INTO Logbook (LogbookId, Name, DefaultActivityId, CreatedBy, UpdatedBy, Status) " +
                                       "OUTPUT inserted.LogbookId " +
                                       "VALUES (@LogbookId, @Name, @DefaultActivityId, @CreatedBy, @UpdatedBy, @Status)",
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
                    foreach (var o in f.ActivityFieldOptionMappings)
                    {
                        conn.Execute("INSERT INTO LogbookEntryFieldOption (LogbookEntryId, FieldOptionId, Selected) " +
                                     "VALUES (@LogbookEntryId, @FieldOptionId, @Selected)",
                                     new { LogbookEntryId = entryId, FieldOptionId = o.FieldOptionId, Selected = o.Selected });
                    }
                }

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

        public static IEnumerable<LogbookDTO> GetLogbooks()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<LogbookDTO>("SELECT * FROM Logbook");
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
                conn.Execute("UPDATE Activity SET Name = @Name, Description = @Description WHERE ActivityId = @ActivityId", new { dto.ActivityId, dto.Name, dto.Description });
            }
        }

        public static void UpdateField(FieldDTO dto)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Execute("UPDATE Field SET Name = @Name WHERE FieldId = @FieldId", new { dto.FieldId, dto.Name });
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
                return conn.Query<ActivityFieldOptionMapping>("SELECT * FROM ActivityFieldOptionMapping WHERE FieldId = @FieldId AND OptionText IS NOT NULL ORDER BY FieldOptionSortOrder", new {FieldId = fieldId}).ToArray();
            }
        }

        public static LogbookEntryFieldDTO[] GetFieldOptionMappings(Guid userId, Guid activityId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                var fields = GetFields(activityId);
                var mappings = fields.Select(f => new LogbookEntryFieldDTO() {Name = f.Name, ActivityFieldOptionMappings = GetFieldOptionMappings(f.FieldId) });
                return mappings.Where(m => m.ActivityFieldOptionMappings.Length > 0).ToArray();
            }
        }

        public static SelectedFieldOption[] GetSelectedFields(Guid logbookEntryId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                return conn.Query<SelectedFieldOption>("SELECT Name AS FieldName, Text AS OptionText FROM SelectedFieldOption WHERE LogbookEntryId = @LogbookEntryId", new { LogbookEntryId = logbookEntryId }).ToArray();
            }
        }
    }
}
