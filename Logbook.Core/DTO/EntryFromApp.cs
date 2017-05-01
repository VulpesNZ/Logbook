using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class EntryFromAppJSON
    {
        public Guid userId { get; set; }
        public Guid entryId { get; set; }
        public Guid logbookId { get; set; }
        public Guid activityId { get; set; }
        public string activityName { get; set; }
        public string notes { get; set; }
        public DateTime date { get; set; }
        public SelectedFieldOptionFromAppJSON[] selectedFieldOptions { get; set; }
        public FieldCustomValueFromAppJSON[] fieldCustomValues { get; set; }
    }

    public class SelectedFieldOptionFromAppJSON
    {
        public Guid fieldId { get; set; }
        public FieldOptionDTO fieldOption { get; set; }
    }

    public class FieldCustomValueFromAppJSON
    {
        public Guid fieldId { get; set; }
        public string customValue { get; set; }

    }
}
