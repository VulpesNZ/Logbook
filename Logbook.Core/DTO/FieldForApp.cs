using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class FieldForAppDTO
    {
        public Guid fieldId { get; set; }
        public Guid activityId { get; set; }
        public string name { get; set; }
        public bool allowFreeText { get; set; }
        public bool isRequired { get; set; }
    }
}
