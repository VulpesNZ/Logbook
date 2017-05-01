using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class SelectedFieldOptionForAppDTO
    {
        public Guid fieldId { get; set; }
        public Guid fieldOptionId { get; set; }
        public string fieldName { get; set; }
        public string fieldOptionText { get; set; }
    }
}
