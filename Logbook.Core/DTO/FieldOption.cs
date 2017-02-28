using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class FieldOptionDTO
    {
        public Guid FieldOptionId { get; set; }
        public Guid FieldId { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
        public bool Active { get; set; }
    }
}
