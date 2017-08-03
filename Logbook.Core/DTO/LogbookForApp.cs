using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class LogbookForAppDTO
    {
        public Guid logbookId { get; set; }
        public DateTime updateDate { get; set; }
        public string name { get; set; }
        public Guid defaultActivityId { get; set; }
    }
}
