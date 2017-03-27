using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class LogbookDTO
    {
        public Guid LogbookId { get; set; }
        public Guid UserId { get; set; }
        public Guid IndustryId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public Guid DefaultActivityId { get; set; }
    }
}
