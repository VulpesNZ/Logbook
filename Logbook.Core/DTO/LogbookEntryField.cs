using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class LogbookEntryFieldDTO
    {
        public Guid LogbookId { get; set; }
        public Guid FieldId { get; set; }
        public Guid ActivityId { get; set; }
        public string Name { get; set; }
        public bool AllowFreeText { get; set; }
        public bool IsMultiSelect { get; set; }
        public bool IsRequired { get; set; }
        public bool Active { get; set; }
        public string CustomText { get; set; }
        public ActivityFieldOptionMapping[] ActivityFieldOptionMappings { get; set; }
    }
}
