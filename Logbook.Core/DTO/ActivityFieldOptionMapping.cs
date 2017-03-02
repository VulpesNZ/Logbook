using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class ActivityFieldOptionMapping
    {
        public string FieldName { get; set; }
        public string OptionText { get; set; }
        public Guid ActivityId { get; set; }
        public Guid FieldId { get; set; }
        public Guid FieldOptionId { get; set; }
        public int FieldSortOrder { get; set; }
        public int FieldOptionSortOrder { get; set; }
    }
}
