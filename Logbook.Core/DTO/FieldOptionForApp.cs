using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class FieldOptionForAppDTO
    {
        public Guid fieldOptionId { get; set; }
        public Guid fieldId { get; set; }
        public string text { get; set; }
        public int sortOrder { get; set; }
    }
}
