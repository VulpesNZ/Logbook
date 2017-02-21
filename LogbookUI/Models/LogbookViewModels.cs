using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Logbook.Core.DTO;

namespace LogbookUI.Models
{
    public class CreateLogbookViewModel
    {
        [Required]
        [Display(Name = "Logbook Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default Activity")]
        public Guid DefaultActivityId { get; set; }
        
        public List<ActivityDTO> Activities { get; set; }
    }

    public class LogbookViewModel
    {
        public Guid LogbookId { get; set; }

        [Display(Name = "Logbook Name")]
        public string Name { get; set; }
    }
}