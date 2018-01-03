using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core
{
    public static class Reporting
    {
        public static byte[] GenerateCSVReport(Guid activityId, DateTime startDate, DateTime endDate)
        {
            var data = DataAccess.GetReportData(activityId, startDate, endDate);
            data.Columns.Remove("ActivityId");
            data.Columns.Remove("LogbookEntryId");

            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = data.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in data.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => $"\"{field}\"");
                sb.AppendLine(string.Join(",", fields));
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
