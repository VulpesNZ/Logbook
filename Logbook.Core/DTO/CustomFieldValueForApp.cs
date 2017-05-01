using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class CustomFieldValueForApp
    { 
        public Guid fieldId { get; set; }
        public string customValue { get; set; }
        public string fieldName { get; set; }
    }
}
