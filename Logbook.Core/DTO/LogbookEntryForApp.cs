using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class LogbookEntryForAppDTO
    {
        public Guid logbookEntryId { get; set; }
        public Guid logbookId { get; set; }
        public Guid activityId { get; set; }
        public DateTime date { get; set; }
        public string notes { get; set; }
        public string syncStatus { get; set; }
        public SelectedFieldOptionForAppDTO[] selectedFieldOptions { get; set; }
        public CustomFieldValueForApp[] fieldCustomValues { get; set; }
    }
}
