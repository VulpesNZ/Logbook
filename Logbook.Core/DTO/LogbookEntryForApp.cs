using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class LogbookEntryForAppDTO
    {
        public Guid LogbookEntryId { get; set; }
        public Guid entryId => LogbookEntryId;
        public Guid logbookId { get; set; }
        public Guid createdBy { get; set; }
        public Guid updatedBy { get; set; }
        public Guid activityId { get; set; }
        public string activityName { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
        public string formattedDate { get; set; }
        public string notes { get; set; }
        public LogbookEntryFieldDTO[] entryFields { get; set; }
        public SelectedFieldOptionForAppDTO[] selectedFieldOptions { get; set; }
        public CustomFieldValueForApp[] fieldCustomValues { get; set; }
        public bool synced { get; set; }
    }
}
