using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using System.Configuration;
using System.Linq;

namespace Logbook.Test
{
    [TestClass]
    public class SpikeTests
    {
        [TestMethod]
        public void Spike()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                conn.Open();
                Console.WriteLine(conn.Query<string>("SELECT TOP 1 Email FROM [User]").First());
            }
        }
    }
}
