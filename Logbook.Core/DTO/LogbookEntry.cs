using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class LogbookEntryDTO
    {
        public Guid LogbookEntryId { get; set; }
        public Guid LogbookId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public Guid ActivityId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Status { get; set; }
        public DateTime EntryDate { get; set; }
        public string Notes { get; set; }
    }
}
