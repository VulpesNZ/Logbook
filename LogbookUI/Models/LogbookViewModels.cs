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
        
        public IEnumerable<ActivityDTO> Activities { get; set; }
    }

    public class LogbookViewModel
    {
        public Guid LogbookId { get; set; }

        [Display(Name = "Logbook Name")]
        public string Name { get; set; }
        
        [Display(Name = "Activity")]
        public string Activity { get; set; }
        
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }

        [Display(Name = "Entries")]
        public IEnumerable<LogbookEntryDTO> Entries { get; set; }
    }

    public class AddLogbookEntryViewModel
    {
        public Guid LogbookId { get; set; }

        [Display(Name = "Activity")]
        public Guid ActivityId { get; set; }

        public IEnumerable<ActivityDTO> Activities { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

    }

    public class MyLogbooksViewModel
    {
        public IEnumerable<LogbookDTO> Logbooks { get; set; }
    }
}