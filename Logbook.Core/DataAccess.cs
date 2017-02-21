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
    }
}
