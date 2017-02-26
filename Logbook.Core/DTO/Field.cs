using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class FieldDTO
    {
        public Guid FieldId { get; set; }
        public Guid ActivityId { get; set; }
        public string Name { get; set; }
        public string AllowFreeText { get; set; }
        public string AllowMultiSelect { get; set; }
        public bool Active { get; set; }
    }
}
