using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        [Display(Name = "Usual Activity")]
        public string Activity { get; set; }
        
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }

        [Display(Name = "Entries")]
        public IEnumerable<LogbookEntryDTO> Entries { get; set; }
    }

    public class EditLogbookEntryViewModel
    {
        public Guid LogbookId { get; set; }
        public Guid LogbookEntryId { get; set; }

        [Display(Name = "Activity")]
        public Guid ActivityId { get; set; }

        public IEnumerable<ActivityDTO> Activities { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public LogbookEntryFieldDTO[] LogbookEntryFields { get; set; }
    }

    public class LogbookEntryViewModel
    {
        public Guid LogbookEntryId { get; set; }

        public Guid LogbookId { get; set; }

        [Display(Name = "Activity")]
        public Guid ActivityId { get; set; }
        
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public SelectedFieldOption[] SelectedFields { get; set; }

        [ReadOnly(true)]
        public LogbookDTO Logbook { get; set; }
    }

    public class MyLogbooksViewModel
    {
        public IEnumerable<LogbookDTO> Logbooks { get; set; }
    }
}
