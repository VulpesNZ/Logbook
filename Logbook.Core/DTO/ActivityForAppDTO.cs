using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class ActivityForAppDTO
    {
        public Guid ActivityId { get; set; }
        public string ActivityName { get; set; }

        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public int FieldSortOrder { get; set; }

        public Guid FieldOptionId { get; set; }
        public string FieldOptionText { get; set; }
        public int FieldOptionSortOrder { get; set; }
    }
}


