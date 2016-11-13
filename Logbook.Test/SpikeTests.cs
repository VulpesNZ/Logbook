using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using System.Configuration;
using System.Linq;
using Logbook.Core;

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
                var user = DataAccess.GetUser("vulpesnz@gmail.com");
            }
        }
    }
}
