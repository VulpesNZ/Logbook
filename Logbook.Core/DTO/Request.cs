using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class Request
    {
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Expires { get; set; }
        public string RequestType { get; set; }
    }
}
