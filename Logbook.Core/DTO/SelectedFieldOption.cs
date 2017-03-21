using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class SelectedFieldOption
    {
        public Guid LogbookEntryId { get; set; }
        public string FieldName { get; set; }
        public string OptionText { get; set; }
    }
}
